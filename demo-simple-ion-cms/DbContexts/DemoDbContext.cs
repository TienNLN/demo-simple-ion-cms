using demo_simple_ion_cms.Entities;
using Microsoft.EntityFrameworkCore;

namespace demo_simple_ion_cms.DbContexts
{
    public class DemoDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<FavouriteFood> FavouriteFoods { get; set; }

        public DemoDbContext(DbContextOptions<DemoDbContext> options) : base(options)
        {
        }

        static DemoDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().Property(tempCustomer => tempCustomer.Id).UseMySqlIdentityColumn();

            modelBuilder.Entity<FavouriteFood>().Property(tempFavFood => tempFavFood.Id).UseMySqlIdentityColumn();
        }
    }
}