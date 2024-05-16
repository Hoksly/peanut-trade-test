namespace CryptoAPI.Models
{
    public class EstimateRequest
    {
        public decimal InputAmount { get; set; }
        public string InputCurrency { get; set; }
        public string OutputCurrency { get; set; }
        
        public EstimateRequest(decimal inputAmount, string inputCurrency, string outputCurrency)
        {
            InputAmount = inputAmount;
            InputCurrency = inputCurrency;
            OutputCurrency = outputCurrency;
        }
    }
}
