using CryptoAPI.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CryptoAPI.Exchanges
{
    public class BinanceExchange : IExchange
    {
        public string Name => "binance";
        private const string BaseApiUrl = "https://api.binance.com";

        public async Task<RateResponseModel> GetExchangeRateAsync(RateRequestModel requestModel)
        {
            using var httpClient = new HttpClient { BaseAddress = new Uri(BaseApiUrl) };
            
            var response = await httpClient.GetAsync($"/api/v3/depth?symbol={requestModel.BaseCurrency}{requestModel.QuoteCurrency}&limit=1");
            if (response.IsSuccessStatusCode)
            { 
                var sellOrderBook = JObject.Parse(await response.Content.ReadAsStringAsync());
                
                var lastTenAsks = sellOrderBook["asks"]?.TakeLast(10).Select(a => a[0]?.Value<decimal>() ?? 0).ToList();
                if (lastTenAsks?.Count > 0)
                {
                    return new RateResponseModel
                    {
                        ExchangeName = Name,
                        Rate = lastTenAsks.Average()
                    };
                }
               
            }
            
            // If the first request fails, try the reverse pair
            
            response = await httpClient.GetAsync($"/api/v3/depth?symbol={requestModel.QuoteCurrency}{requestModel.BaseCurrency}&limit=1");
            if (!response.IsSuccessStatusCode)
            {
                throw new ArgumentException("Invalid pair");
            }

            var buyOrderBook = JObject.Parse(await response.Content.ReadAsStringAsync());

            var lastTenAsksReverse = buyOrderBook["asks"]?.TakeLast(10).Select(a => a[0]?.Value<decimal>() ?? 0).ToList();
            if (lastTenAsksReverse?.Count > 0)
            {
                return new RateResponseModel
                {
                    ExchangeName = Name,
                    Rate = Decimal.One / lastTenAsksReverse.Average()
                };
            }
            
            throw new ArgumentException("Invalid pair");
        }
    }
}
