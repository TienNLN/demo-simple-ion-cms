using System.Collections.Generic;
using System.Threading.Tasks;
using demo_simple_ion_cms.Models.Common;
using demo_simple_ion_cms.Models.Datas.FavouriteFoods;
using demo_simple_ion_cms.Models.Responses.FavouriteFoods;

namespace demo_simple_ion_cms.IServices
{
    public interface IFavouriteFoodService
    {
        Task<GenericResult<FavouriteFoodDTO>> GetById(int id);
        Task<GenericResult<List<FavouriteFoodDTO>>> CreateNew(FavouriteFoodCreateRangeDataModel dataModel);
        Task<GenericResult<FavouriteFoodDTO>> CreateNew(FavouriteFoodCreateDataModel dataModel);
        Task<GenericResult<List<FavouriteFoodDTO>>> GetAll();
        Task<GenericResult<List<FavouriteFoodDTO>>> GetByCustomerId(int customerId);
    }
}