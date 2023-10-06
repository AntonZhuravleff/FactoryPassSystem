using FactoryPassSystem.WebAPI.Data;
using FactoryPassSystem.WebAPI.Data.Repositories;
using FactoryPassSystem.WebAPI.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using FactoryPassSystem.WebAPI.Mapper;
using FactoryPassSystem.WebAPI.Services;

namespace FactoryPassSystem.WebAPI
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
            services.AddControllers();
    
            services.AddDbContext<FactoryPassDbContext>();

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IPositionRepository, PositionRepository>();
            services.AddScoped<IShiftRepository, ShiftRepository>();

            services.AddScoped<ICheckpointService, CheckpointService>();

            services.AddAutoMapper(typeof(AspNetWebApiProfile));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FactoryPassSystem.WebAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // Seeding database
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var scopedProvider = scope.ServiceProvider;
                    var factoryPassDbContext = scopedProvider.GetRequiredService<FactoryPassDbContext>();


                    factoryPassDbContext.Database.EnsureCreated();

                    FactoryPassDbContextSeed.SeedAsync(factoryPassDbContext).GetAwaiter().GetResult();
                }

                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FactoryPassSystem.WebAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
