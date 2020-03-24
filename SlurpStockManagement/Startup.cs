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
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", builder => builder.WithOrigins("http://localhost:3000", "http://react-web-stock-management.azurewebsites.net").AllowAnyHeader().AllowAnyMethod().AllowCredentials());
            });

            services.AddMvc();
            services.AddSignalR();

            services.Configure<CoffeeDatabaseSettings>(Configuration.GetSection(nameof(CoffeeDatabaseSettings)));
            services.Configure<BoxDatabaseSettings>(Configuration.GetSection(nameof(BoxDatabaseSettings)));
            services.Configure<OrderListDatabaseSettings>(Configuration.GetSection(nameof(OrderListDatabaseSettings)));
            services.Configure<CoffeeBagSizeSettings>(Configuration.GetSection(nameof(CoffeeBagSizeSettings)));
            services.Configure<BoxSizeSettings>(Configuration.GetSection(nameof(BoxSizeSettings)));

            services.AddSingleton<ICoffeeDatabaseSettings>(sp => sp.GetRequiredService<IOptions<CoffeeDatabaseSettings>>().Value);
            services.AddSingleton<IBoxDatabaseSettings>(sp => sp.GetRequiredService<IOptions<BoxDatabaseSettings>>().Value);
            services.AddSingleton<IOrderListDatabaseSettings>(sp => sp.GetRequiredService<IOptions<OrderListDatabaseSettings>>().Value);
            services.AddSingleton<ICoffeeBagSettings>(sp => sp.GetRequiredService<IOptions<CoffeeBagSizeSettings>>().Value);
            services.AddSingleton<IBoxSizeSettings>(sp => sp.GetRequiredService<IOptions<BoxSizeSettings>>().Value);

            services.AddTransient<IReserveCoffeeService, ReserveCoffeeService>();
            services.AddTransient<IReserveBoxService, ReserveBoxService>();
            services.AddTransient<IOrderService, OrderService>();

            services.AddTransient<ICoffeeRepository, CoffeeRepository>();
            services.AddTransient<IBoxRepository, BoxRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();

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

            app.UseCors(options => options.WithOrigins("http://localhost:3000", "http://react-web-stock-management.azurewebsites.net").AllowAnyHeader().AllowAnyMethod().AllowCredentials());

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
                endpoints.MapHub<RealTimeUpdate>("/realTimeUpdate");
                endpoints.MapControllers();
            });


        }
    }
}
