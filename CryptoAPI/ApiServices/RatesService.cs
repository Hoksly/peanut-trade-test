using CryptoAPI.Exchanges;
using CryptoAPI.Models;

namespace CryptoAPI.Services
{
    public class RatesService: IRatesService
    {
        private readonly IEnumerable<IExchange> _exchanges;
        
        public RatesService(IEnumerable<IExchange> exchanges)
        {
            _exchanges = exchanges;
        }
        public Task<RateResponseModel[]> GetRatesAsync(RateRequestModel request)
        {
            var tasks = _exchanges.Select(x => x.GetExchangeRateAsync(request));

            
            return Task.WhenAll(tasks).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    foreach (var ex in task.Exception.InnerExceptions)
                    {
                        if (ex is ArgumentException)
                            throw ex;
                    } 
                    throw new Exception("An error occurred while fetching rates");
                }

                return task.Result;
            });

        }
    }
}
