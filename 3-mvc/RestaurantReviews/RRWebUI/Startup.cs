using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RRDL;
using Microsoft.EntityFrameworkCore;
using RRBL;
namespace RRWebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // Services are the things that your applciation is dependent on
        // Services have different lifetimes depending on when instances are created for that particular
        // dep:
        // 1. Transient = a new object is created per service call, lots of overhead 
        // 2. Scoped = a new instance is created per request 
        // 3. Singleton = an instance is shared for every request, leads to other requests waiting
        // used for dep injection
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<RestaurantDBContext>(options => options.UseNpgsql(Configuration.GetConnectionString("RestaurantDB")));
            services.AddScoped<IRepository, RepoDB>();
            services.AddScoped<IRestaurantBL, RestaurantBL>();
            services.AddScoped<IReviewBL, ReviewBL>();

        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
