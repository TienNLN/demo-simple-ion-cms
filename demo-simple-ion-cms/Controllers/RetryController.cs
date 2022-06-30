using System;
using System.Threading.Tasks;
using demo_simple_ion_cms.IServices;
using demo_simple_ion_cms.Utils;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace demo_simple_ion_cms.Controllers
{
    [Route("api/v{version:apiVersion}/retries")]
    [ApiController]
    [ApiVersion("2")]
    public class RetryController : ControllerBase
    {
        private readonly IRetryService _retryService;

        public RetryController(IRetryService retryService)
        {
            _retryService = retryService;
        }
        
        [HttpGet]
        [MapToApiVersion("2")]
        public async Task<IActionResult> DemoRetry()
        {
            Retry.Do(() => _retryService.DemoRetry(), TimeSpan.FromSeconds(2), 5);

            return Ok();
        }
    }
}