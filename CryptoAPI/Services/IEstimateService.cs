using CryptoAPI.Models;

namespace CryptoAPI.Services
{
    public interface IEstimateService
    {
        Task<EstimateResponse> EstimateExchangeAsync(EstimateRequest request);
    }
}
