using demo_simple_ion_cms.DbContexts;
using demo_simple_ion_cms.Entities;
using demo_simple_ion_cms.IRepositories;

namespace demo_simple_ion_cms.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(DemoDbContext context) : base(context)
        {
        }
    }
}