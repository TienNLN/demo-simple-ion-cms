using AutoMapper;
using demo_simple_ion_cms.Entities;
using demo_simple_ion_cms.Models.Datas;
using demo_simple_ion_cms.Models.Payloads;
using demo_simple_ion_cms.Models.Responses;

namespace demo_simple_ion_cms.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDTO>().ReverseMap();

            CreateMap<Customer, CustomerCreatePayload>().ReverseMap();

            CreateMap<Customer, CustomerUpdatePayload>().ReverseMap();
            CreateMap<Customer, CustomerUpdateDataModel>().ReverseMap()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<CustomerUpdatePayload, CustomerUpdateDataModel>().ReverseMap();
        }
    }
}