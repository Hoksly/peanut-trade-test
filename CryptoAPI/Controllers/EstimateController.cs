using Microsoft.AspNetCore.Mvc;

using CryptoAPI.Models;
using System.Threading.Tasks;
using CryptoAPI.Services;

namespace CryptoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstimateController : ControllerBase
    {
        private readonly IEstimateService _estimateService;

        public EstimateController(IEstimateService estimateService)
        {
            _estimateService = estimateService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEstimate([FromQuery] decimal inputAmount, [FromQuery] string inputCurrency, [FromQuery] string outputCurrency)
        {
            var request = new EstimateRequestModel
            {
                InputAmount = inputAmount,
                InputCurrency = inputCurrency,
                OutputCurrency = outputCurrency
            };


            try
            {
                var result = await _estimateService.EstimateExchangeAsync(request);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
