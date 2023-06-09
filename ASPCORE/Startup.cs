using ASPCORE.Models;
using ASPCORE.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPCORE.Infrastructure;
using ASPCORE.Repository;
using Microsoft.AspNetCore.Identity.UI.Services;
using ASPCORE.GenericRepo;
using Microsoft.AspNetCore.Http;

namespace ASPCORE
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
            services.AddDefaultIdentity<ApplicationUser>(/*options=>*///Password Settings changed here
            //{
            //    options.Password.RequiredLength = 8;
            //    options.Password.RequiredUniqueChars = 2;
            //}
            ).AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();//Service for adding new Identity Role
            services.AddControllersWithViews();//This method 
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<IEmployee, EmployeeRepo>();
            services.AddTransient(typeof(IRepo<>), typeof(Repository<>));
            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.Configure<IdentityOptions>(options =>//Here we can put our changed password settings
            {
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 2;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("CreateRolePolicy", policy => policy.RequireClaim("Create Role"));
            });

            services.AddMemoryCache();//For using caching in project use this service injection
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
                app.UseStatusCodePagesWithRedirects("/Error/{0}");
                app.UseExceptionHandler("/Home/Error"); //In Production Environment Global Exception Handler Handle by Calling this Error Page
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();//Here now how to Request Proccessing by Middlewares


            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("First Middleware");
            //    await next();
            //});
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMvc();
            //app.UseEndpoints(endpoints => //This is terminal middleware does,nt pass request to other middleware
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello Word!");
            //    });
            //});
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                //pattern: "{tr}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
