using L3AQTN_HFT_202231.Logic;
using L3AQTN_HFT_202231.Models;
using L3AQTN_HFT_202231.Repo;
using L3AQTN_HFT_202231.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace L3AQTN_HFT202231.Endpoint
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
            services.AddTransient<BusDbContext>();

            services.AddTransient<IRepository<Bus>, BusRepository>();
            services.AddTransient<IRepository<Brand>,BrandRepository>();
            services.AddTransient<IRepository<Owner>, OwnerRepository>();

            services.AddTransient<IBusLogic, BusLogic>();
            services.AddTransient<IBrandLogic, BrandLogic>();
            services.AddTransient<IOwnerLogic, OwnerLogic>();


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "L3AQTN_HFT202231.Endpoint", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "L3AQTN_HFT202231.Endpoint v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}