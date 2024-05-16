using CryptoAPI.Models;
using Newtonsoft.Json.Linq;

namespace CryptoAPI.Exchanges
{
    public class KucoinExchange : IExchange
    {
        public string Name => "kucoin";

        public async Task<RateResponseModel> GetExchangeRateAsync(RateRequestModel requestModel)
        {
            using var httpClient = new HttpClient { BaseAddress = new Uri("https://api.kucoin.com") };
            var response = await httpClient.GetAsync($"/api/v1/market/orderbook/level2_20?symbol={requestModel.BaseCurrency}-{requestModel.QuoteCurrency}");
            JObject? orderBook;
            if (response.IsSuccessStatusCode)
            {
                orderBook = JObject.Parse(await response.Content.ReadAsStringAsync());
                if (!string.IsNullOrEmpty(orderBook["data"]?["asks"]?.ToString()))
                {
                    var lastTenAsks = orderBook["data"]?["asks"]?.TakeLast(10).Select(a => a[0]?.Value<decimal>() ?? 0).ToList();

                    if (lastTenAsks?.Count > 0)
                    {
                        return new() { ExchangeName = Name, Rate = lastTenAsks.Average() };
                    }
                }
            }
            
            // If the first request fails, try the reverse pair

            response = await httpClient.GetAsync($"/api/v1/market/orderbook/level2_20?symbol={requestModel.QuoteCurrency}-{requestModel.BaseCurrency}");
            var content = await response.Content.ReadAsStringAsync();
            if (content == null)
            {
                throw new ArgumentException("Invalid pair");
            }

            orderBook = JObject.Parse(content);
            if (string.IsNullOrEmpty(orderBook["data"]?["asks"]?.ToString()))
            {
                throw new ArgumentException("Invalid pair");
            }
            
            var lastTenAsksReverse = orderBook["data"]?["asks"]?.TakeLast(10).Select(a => a[0]?.Value<decimal>() ?? 0).ToList();
            
            if (lastTenAsksReverse?.Count > 0)
            {
                return new() { ExchangeName = Name, Rate = Decimal.One / lastTenAsksReverse.Average() };
            }

            throw new ArgumentException("Invalid pair");
        }
    }
}
