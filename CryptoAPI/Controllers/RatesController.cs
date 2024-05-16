using Microsoft.AspNetCore.Mvc;
using CryptoAPI.Models;
using CryptoAPI.Models;
using System.Threading.Tasks;
using CryptoAPI.Services;
using CryptoExchangeApi.Services;

namespace CryptoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RatesController : ControllerBase
    {
        private readonly IRatesService _ratesService;
        private IRequestValidationService _requestValidationService;

        public RatesController(IRatesService ratesService, IRequestValidationService requestValidationService)
        {
            _ratesService = ratesService;
            _requestValidationService = requestValidationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEstimate([FromQuery] string baseCurrency, [FromQuery] string quoteCurrency)
        {
            if (!_requestValidationService.CurrencyValid(baseCurrency))
            {
                return BadRequest("No such currency available " + baseCurrency);
            }
            
            if(_requestValidationService.CurrencyValid(quoteCurrency))
            {
                return BadRequest("No such currency available " + quoteCurrency);
            }
            
            var request = new RateRequestModel
            {
                BaseCurrency = baseCurrency,
                QuoteCurrency = quoteCurrency
            };
            try
            {
                var result = await _ratesService.GetRatesAsync(request);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
