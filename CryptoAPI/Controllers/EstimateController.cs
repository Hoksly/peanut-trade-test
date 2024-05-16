using Microsoft.AspNetCore.Mvc;

using CryptoAPI.Models;
using System.Threading.Tasks;
using CryptoAPI.Services;
using CryptoExchangeApi.Services;

namespace CryptoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstimateController : ControllerBase
    {
        private readonly IEstimateService _estimateService;
        private readonly IRequestValidationService _requestValidationService;

        public EstimateController(IEstimateService estimateService, IRequestValidationService requestValidationService)
        {
            _estimateService = estimateService;
            _requestValidationService = requestValidationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEstimate([FromQuery] decimal inputAmount, [FromQuery] string inputCurrency, [FromQuery] string outputCurrency)
        {
            if (!_requestValidationService.CurrencyValid(inputCurrency))
            {
                return BadRequest("No such currency available " + inputCurrency);
            }
            
            if(_requestValidationService.CurrencyValid(outputCurrency))
            {
                return BadRequest("No such currency available " + outputCurrency);
            }
            
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
