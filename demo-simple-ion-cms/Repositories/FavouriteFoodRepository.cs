using demo_simple_ion_cms.DbContexts;
using demo_simple_ion_cms.Entities;
using demo_simple_ion_cms.IRepositories;

namespace demo_simple_ion_cms.Repositories
{
    public class FavouriteFoodRepository : BaseRepository<FavouriteFood>, IFavouriteFoodRepository
    {
        public FavouriteFoodRepository(DemoDbContext context) : base(context)
        {
        }
    }
}