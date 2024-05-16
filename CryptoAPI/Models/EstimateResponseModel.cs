namespace CryptoAPI.Models
{
    public class EstimateResponse
    {
        public string ExchangeName { get; set; }
        public decimal OutputAmount { get; set; }
        
        public EstimateResponse(string exchangeName, decimal outputAmount)
        {
            ExchangeName = exchangeName;
            OutputAmount = outputAmount;
        }
    }
}
