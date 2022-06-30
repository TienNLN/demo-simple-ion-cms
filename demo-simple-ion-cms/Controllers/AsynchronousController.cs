using System.Threading.Tasks;
using demo_simple_ion_cms.Utils;
using Microsoft.AspNetCore.Mvc;

namespace demo_simple_ion_cms.Controllers
{
    [Route("api/v{version:apiVersion}/asynchronous")]
    [ApiController]
    [ApiVersion("2")]
    public class AsynchronousController : ControllerBase
    {
        [HttpGet]
        [MapToApiVersion("2")]
        public async Task<IActionResult> DemoAsynchronousFlow()
        {
            Task.Run(AsynchronousUtil.Execute);

            return Ok();
        }
    }
}