using AutoMapper;
using DbUp;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using SPSP.Models.SearchObjects;
using SPSP.Services.Category;
using SPSP.Services.Configurations;
using SPSP.Services.Database;
using SPSP.Services.QRTable;
using SPSP.Services.Menu;
using SPSP.Services.MenuItem;
using SPSP.Services.Order;
using SPSP.Services.Reservation;

using SPSP.Services.Base;

namespace SPSP
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

            services.AddDbContext<DataDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("LocalDb")));

            //var mappingConfig = new MapperConfiguration(mc =>
            //{
            //    mc.AddProfile(new MappingProfile());
            //});

            //var mapper = new Mapper(mappingConfig);
            //services.AddSingleton(mapper);

            services.AddSingleton<IMapper>(sp => new Mapper(new MapperConfiguration(cfg => {
                cfg.AddProfile<MappingProfile>(); 
            }), sp.GetService));


            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IMenuItemService, MenuItemService>();
            services.AddTransient
                <IService<Models.Business, BaseSearchObject>, 
                BaseService<Models.Business, Business, BaseSearchObject>>();
            services.AddTransient<IMenuService, MenuService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IQRTableService, QRTableService>();
            services.AddTransient<IReservationService, ReservationService>();


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SPSP", Version = "v1" });
            });



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SPSP v1"));

                var connectionString = Configuration.GetConnectionString("LocalDb");
                var scriptsFolderPath = Configuration["ScriptsPath"];

                EnsureDatabase.For.SqlDatabase(connectionString);

                var upgrader = DeployChanges.To
                    .SqlDatabase(connectionString)
                    .WithScriptsFromFileSystem(scriptsFolderPath)
                    .LogToConsole()
                    .Build();

                var result = upgrader.PerformUpgrade();

                if (!result.Successful)
                {
                    throw new Exception("Database migration failed!", result.Error);
                }
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
