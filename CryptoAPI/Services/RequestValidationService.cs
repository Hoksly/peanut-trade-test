namespace CryptoExchangeApi.Services
{
    public class RequestValidationService : IRequestValidationService
    {
        private readonly IEnumerable<string> _validCurrencies;
        
        public RequestValidationService(IEnumerable<string> validCurrencies)
        {
            _validCurrencies = validCurrencies;
        }
        public bool CurrencyValid(string inputCurrency)
        {
            if (_validCurrencies.Contains(inputCurrency))
                return true; 
            return false;
        }
    }
}
