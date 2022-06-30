using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using demo_simple_ion_cms.Utils;
using Microsoft.AspNetCore.Mvc;

namespace demo_simple_ion_cms.Controllers
{
    [Route("api/v{version:apiVersion}/customers")]
    [ApiController]
    [ApiVersion("1")]
    public class FirebaseController : ControllerBase
    {
        public FirebaseController()
        {
        }

        [HttpGet]
        [MapToApiVersion("1")]
        public async Task<IActionResult> DemoGet(string child)
        {
            var response = await FirebaseUtil.Get<List<string>>(child);

            return Ok(response);
        }
    }
}