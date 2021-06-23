using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SportsStore.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

//The Login for the admin useages is UserName:Admin ; email:Admin@cop3855.com password:Password1!

namespace SportsStore
{
    public class Startup
    {
        IConfigurationRoot Configuration;
        public Startup(IWebHostEnvironment env)
        { 
            Configuration = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json").Build();
        }



        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))); 
            services.AddTransient<IProductRepository, EFProductRepository>();
            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IOrderRepository, EFOrderRepository>();
            services.AddIdentity<AppUser, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.AddMvc(options => options.EnableEndpointRouting = false)
                .AddNewtonsoftJson();
            services.AddMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseDeveloperExceptionPage(); 
            app.UseStatusCodePages(); 
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(name: null, template: "{category}/Page{page:int}", defaults: new { controller = "Product", action = "List" });
                routes.MapRoute(name: null, template: "Page{page:int}", defaults: new { controller = "Product", action = "List", page = 1 });
                routes.MapRoute(name: null, template: "{category}", defaults: new { controller = "Product", action = "List", page = 1 });
                routes.MapRoute(name: null, template: "", defaults: new { controller = "Product", action = "Home", page = 1 }); //controller = "Product", action = "List",
                routes.MapRoute(name: "default", template: "{controller}/{action}/{id?}");
            });
        }
    }
}

//Complete Now converting the example to the project
//THOUGHTS:
//Also this is fun learning about new concepts and how they all interact with each-other.
//Order & Checkout screen is added, i can convert this code into my project easly by changing the database to fit my business requirements.
//ERROR The Ship button on the Order/List page doesn't update the shipped entry in the "Orders" table. FIXED: Changed the name in intput to match the controller and added a value in button set to being "submit"