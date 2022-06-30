using System.Collections.Generic;
using System.Threading.Tasks;
using demo_simple_ion_cms.Models.Common;
using demo_simple_ion_cms.Models.Datas;
using demo_simple_ion_cms.Models.Payloads;
using demo_simple_ion_cms.Models.Responses;

namespace demo_simple_ion_cms.IServices
{
    public interface ICustomerService
    {
        Task<GenericResult<CustomerDTO>> CreateCustomer(CustomerCreatePayload payload);
        Task<GenericResult<List<CustomerDTO>>> GetAll();
        Task<GenericResult<CustomerDTO>> GetById(int id);
        Task<GenericResult<CustomerDTO>> Update(CustomerUpdateDataModel dataModel);
    }
}