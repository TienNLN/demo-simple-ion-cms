using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using demo_simple_ion_cms.Utils;
using Microsoft.AspNetCore.Mvc;

namespace demo_simple_ion_cms.Controllers
{
    [Route("api/v{version:apiVersion}/firebases")]
    [ApiController]
    [ApiVersion("2")]
    public class FirebaseController : ControllerBase
    {
        public FirebaseController()
        {
        }

        [HttpGet]
        [MapToApiVersion("2")]
        public async Task<IActionResult> DemoGet(string child)
        {
            var response = await FirebaseUtil.Get<List<string>>(child);

            return Ok(response);
        }
        
        [HttpPost]
        [MapToApiVersion("2")]
        public async Task<IActionResult> DemoUpdate([FromQuery]string child, [FromBody]string data)
        {
            var response = await FirebaseUtil.Update(child, data);

            return Ok(response);
        }
    }
}