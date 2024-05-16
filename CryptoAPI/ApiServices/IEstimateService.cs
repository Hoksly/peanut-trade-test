using CryptoAPI.Models;

namespace CryptoAPI.Services
{
    public interface IEstimateService
    {
        Task<EstimateResponseModel> EstimateExchangeAsync(EstimateRequestModel request);
    }
}
