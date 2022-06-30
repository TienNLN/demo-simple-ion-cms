using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using demo_simple_ion_cms.DbContexts;
using demo_simple_ion_cms.DI;
using demo_simple_ion_cms.Extensions;
using demo_simple_ion_cms.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;

namespace demo_simple_ion_cms
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigServiceDI();

            // Add dbcontext
            var connectionString = Configuration.GetConnectionString("default");

            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            
            // if (env != null && env.Equals("Production"))
            // {
            //     var HOST = Configuration["HOST"];
            //     var DATABASE = Configuration["DATABASE"];
            //     var USER_ID = Configuration["USER_ID"];
            //     var DATABASE_PORT = Configuration["DATABASE_PORT"];
            //     var PASSWORD = Configuration["PASSWORD"];
            //     connectionString = $"Server={HOST};Port={DATABASE_PORT};Database={DATABASE};User Id={USER_ID};Password={PASSWORD};SSL Mode=Prefer;Trust Server Certificate=true;";
            // }

            services.AddDbContext<DemoDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            // Compression
            services.AddResponseCompression();

            // AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddControllers();

            // Versioning
            services.AddSwaggerGenNewtonsoftSupport();
            services.ConfigureSwagger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            app.UseInternalExceptionMiddleware();

            app.UseSwagger();
                
            app.UseSerilogRequestLogging();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseResponseCompression();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Versioning
            app.ConfigureSwagger(provider);
        }
    }
}