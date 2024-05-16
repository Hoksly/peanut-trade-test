namespace CryptoExchangeApi.Services
{
    public interface IRequestValidationService
    {
        bool CurrencyValid(string inputCurrency);
    }
}
