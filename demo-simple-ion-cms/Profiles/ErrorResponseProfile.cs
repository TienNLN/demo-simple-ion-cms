using AutoMapper;
using demo_simple_ion_cms.Models.Common;

namespace demo_simple_ion_cms.Profiles
{
    public class ErrorResponseProfile : Profile
    {
        public ErrorResponseProfile()
        {
            CreateMap(typeof(GenericResult<>), typeof(ErrorResponse)).ReverseMap();
            CreateMap<GenericResult, ErrorResponse>().ReverseMap();
        }
    }
}