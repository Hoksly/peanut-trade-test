namespace CryptoAPI.Models
{
    public class ReateRequestModel
    {
        public string baseCurrency { get; set; }
        public string quoteCurrency { get; set; }
        
        public ReateRequestModel(string baseCurrency, string quoteCurrency)
        {
            this.baseCurrency = baseCurrency;
            this.quoteCurrency = quoteCurrency;
        }
    }
}
