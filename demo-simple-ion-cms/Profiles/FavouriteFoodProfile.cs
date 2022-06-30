using AutoMapper;
using demo_simple_ion_cms.Entities;
using demo_simple_ion_cms.Models.Responses.FavouriteFoods;

namespace demo_simple_ion_cms.Profiles
{
    public class FavouriteFoodProfile : Profile
    {
        public FavouriteFoodProfile()
        {
            CreateMap<FavouriteFood, FavouriteFoodDTO>().ReverseMap();
        }
    }
}