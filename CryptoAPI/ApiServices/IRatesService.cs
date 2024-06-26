using CryptoAPI.Models;

namespace CryptoAPI.Services
{
    public interface IRatesService
    {
        Task<RateResponseModel[]> GetRatesAsync(RateRequestModel request);
    }
}
