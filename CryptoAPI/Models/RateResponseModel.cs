namespace CryptoAPI.Models
{
    public class RateResponseModel
    {
        public string ExchangeName { get; set; }
        public decimal Rate { get; set; }
        
        public RateResponseModel(string exchangeName, decimal rate)
        {
            ExchangeName = exchangeName;
            Rate = rate;
        }
    }
}
