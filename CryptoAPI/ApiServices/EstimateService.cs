using CryptoAPI.Exchanges;
using CryptoAPI.Models;

namespace CryptoAPI.Services
{
    public class EstimateService : IEstimateService
    {
        private readonly IRatesService _ratesService;
        public EstimateService(IRatesService ratesService)
        {
           _ratesService = ratesService;
       
        }
        
        
        public async Task<EstimateResponseModel> EstimateExchangeAsync(EstimateRequestModel request)
        {
    
            var rates = await _ratesService.GetRatesAsync(new RateRequestModel
            {
                BaseCurrency = request.InputCurrency,
                QuoteCurrency = request.OutputCurrency
            });

            rates = rates.OrderBy(x => x.Rate).ToArray();
            
            var minRate = rates.First();
            
            return new EstimateResponseModel
            {
                ExchangeName = minRate.ExchangeName,
                
                OutputAmount = minRate.Rate * request.InputAmount
            };
        }
    }
}
