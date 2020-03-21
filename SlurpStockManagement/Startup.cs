using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SlurpStockManagement.Models;
using SlurpStockManagement.Interfaces;
using SlurpStockManagement.Services;
using SlurpStockManagement.Repositories;

namespace SlurpStockManagement
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
            services.Configure<CoffeeDatabaseSettings>(Configuration.GetSection(nameof(CoffeeDatabaseSettings)));
            services.Configure<BoxDatabaseSettings>(Configuration.GetSection(nameof(BoxDatabaseSettings)));

            services.AddSingleton<ICoffeeDatabaseSettings>(sp => sp.GetRequiredService<IOptions<CoffeeDatabaseSettings>>().Value);
            services.AddSingleton<IBoxDatabaseSettings>(sp => sp.GetRequiredService<IOptions<BoxDatabaseSettings>>().Value);

            services.AddTransient<IReserveCoffeeService, ReserveCoffeeService>();
            services.AddTransient<IReserveBoxServices, ReserveBoxService>();

            services.AddTransient<ICoffeeRepository, CoffeeRepository>();
            services.AddTransient<IBoxRepository, BoxRepository>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Slurp Stock Management", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Slurp Stock Management v1");
            });

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
