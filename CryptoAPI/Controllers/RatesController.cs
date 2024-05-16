using Microsoft.AspNetCore.Mvc;
using CryptoAPI.Models;
using CryptoAPI.Models;
using System.Threading.Tasks;
using CryptoAPI.Services;

namespace CryptoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RatesController : ControllerBase
    {
        private readonly IRatesService _ratesService;

        public RatesController(IRatesService ratesService)
        {
            _ratesService = ratesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEstimate([FromQuery] string baseCurrency, [FromQuery] string quoteCurrency)
        {
            
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
