using demo_simple_ion_cms.IRepositories;
using demo_simple_ion_cms.IServices;
using demo_simple_ion_cms.Repositories;
using demo_simple_ion_cms.Services;
using Microsoft.Extensions.DependencyInjection;

namespace demo_simple_ion_cms.DI
{
    public static class ServiceDI
    {
        public static void ConfigServiceDI(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ICustomerService, CustomerService>();

            services.AddScoped<IFavouriteFoodRepository, FavouriteFoodRepository>();
            services.AddScoped<IFavouriteFoodService, FavouriteFoodService>();

            services.AddScoped<IRetryService, RetryService>();
        }
    }
}