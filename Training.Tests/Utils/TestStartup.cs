using Training.Database.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Training.API;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Training.Repository.Interfaces;
using Training.Repository;

namespace Training.Tests.Utils
{
    public class TestStartup
    {
        //public TestStartup(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        //public IConfiguration Configuration { get; }

        //// This method gets called by the runtime. Use this method to add services to the container.
        //public void ConfigureServices(IServiceCollection services)
        //{
        //    services.AddMvc();            
        //    services.AddDbContext<TrainingapiContext>(options => options.UseInMemoryDatabase("test"));


        //}

        //public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        //{
        //    if (env.IsDevelopment())
        //    {
        //        app.UseDeveloperExceptionPage();
        //    }

        //    app.UseMvc();
        //}


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TrainingapiContext>(optionsBuilder => optionsBuilder.UseInMemoryDatabase("InMemoryDb"));
            services.AddMvc();

            services.AddScoped<IProductSessionRepository, ProductSessionRepository>();
        }

        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory)
        {
            var repository = app.ApplicationServices.GetService<IProductSessionRepository>();
            InitializeDatabaseAsync(repository);

            app.UseStaticFiles();

            app.UseMvcWithDefaultRoute();
        }

        public void InitializeDatabaseAsync(IProductSessionRepository repo)
        {
            var sessionList = repo.GetProductList();
            if (!sessionList.Any())
            {
                repo.AddProduct(GetTestSession());
            }
        }

        public static Product GetTestSession()
        {
            var session = new  Product {
                ProductId = 1,
                ProductName = "product 1",
                Package = "box"
            };
            
            return session;
        }
    }
}
