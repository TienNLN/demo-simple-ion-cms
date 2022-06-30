using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using demo_simple_ion_cms.IRepositories;
using demo_simple_ion_cms.IServices;
using demo_simple_ion_cms.Models.Common;
using demo_simple_ion_cms.Models.Datas;
using demo_simple_ion_cms.Models.Datas.Customers;
using demo_simple_ion_cms.Models.Payloads.Customers;
using demo_simple_ion_cms.Models.Responses;
using demo_simple_ion_cms.Models.Responses.Customers;
using Microsoft.AspNetCore.Mvc;

namespace demo_simple_ion_cms.Controllers
{
    [Route("api/v{version:apiVersion}/customers")]
    [ApiController]
    [ApiVersion("1")]
    public class CustomerController : ControllerBase
    {
        private readonly IMapper _mapper;
        
        private readonly ICustomerService _customerService;

        public CustomerController(IMapper mapper,
            ICustomerService customerService)
        {
            _mapper = mapper;

            _customerService = customerService;
        }

        [HttpGet]
        [MapToApiVersion("1")]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var result = await _customerService.GetAll();

            // Return with statusCode=200 and data if success
            if (result.IsSuccess)
                return Ok(new MultiObjectResponse<CustomerDTO>(result.Data));

            // Add error response data informations
            Response.StatusCode = result.StatusCode;

            var response = _mapper.Map<ErrorResponse>(result);

            return StatusCode(result.StatusCode, response);
        }

        [HttpGet("{id:int}")]
        [MapToApiVersion("1")]
        public async Task<IActionResult> GetById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var result = await _customerService.GetById(id);

            // Return with statusCode=200 and data if success
            if (result.IsSuccess)
                return Ok(new SingleObjectResponse<CustomerDTO>(result.Data));

            // Add error response data informations
            Response.StatusCode = result.StatusCode;

            var response = _mapper.Map<ErrorResponse>(result);

            return StatusCode(result.StatusCode, response);
        }

        [HttpPost]
        [MapToApiVersion("1")]
        public async Task<IActionResult> CreateNew(CustomerCreatePayload payload)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var result = await _customerService.CreateCustomer(payload);

            // Return with statusCode=201 and data if success
            if (result.IsSuccess)
                return StatusCode((int)HttpStatusCode.Created, 
                    new SingleObjectResponse<CustomerDTO>(result.Data));

            // Add error response data informations
            Response.StatusCode = result.StatusCode;

            var response = _mapper.Map<ErrorResponse>(result);

            return StatusCode(result.StatusCode, response);
        }
        
        [HttpPut]
        [MapToApiVersion("1")]
        public async Task<IActionResult> Update([FromQuery]int id, [FromBody]CustomerUpdatePayload payload)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dataModel = _mapper.Map<CustomerUpdateDataModel>(payload);
            dataModel.Id = id;
            
            var result = await _customerService.Update(dataModel);

            // Return with statusCode=200 and data if success
            if (result.IsSuccess)
                return Ok(new SingleObjectResponse<CustomerDTO>(result.Data));

            // Add error response data informations
            Response.StatusCode = result.StatusCode;

            var response = _mapper.Map<ErrorResponse>(result);

            return StatusCode(result.StatusCode, response);
        }
    }
}