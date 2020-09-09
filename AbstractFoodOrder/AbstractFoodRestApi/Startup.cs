using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbstractFoodDatabaseImplement.Implements;
using AbstractFoodOrderBusinessLogic.BusinessLogics;
using AbstractFoodOrderBusinessLogic.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AbstractFoodRestApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IClientLogic, ClientLogic>();
            services.AddTransient<IOrderLogic, OrderLogic>();
            services.AddTransient<IKitLogic, KitLogic>();
            services.AddTransient<IMessageInfoLogic, MessageInfoLogic>();
            services.AddTransient<MainLogic>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
