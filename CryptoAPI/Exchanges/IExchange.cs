using System.Threading.Tasks;
using CryptoAPI.Models;

namespace CryptoAPI.Exchanges
{
    public interface IExchange
    {
        string Name { get; }
        Task<RateResponseModel> GetExchangeRateAsync(RateRequestModel requestModel);
    }
}
