using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using SportsPro.Models;
using SportsPro.Models.DataLayer;
using Microsoft.AspNetCore.Identity;
using System;

namespace SportsPro
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Session state for Tech ID
            services.AddMemoryCache();
            services.AddSession();

            //Unit of work and repository
            services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddTransient(typeof(IGRepository<>), typeof(GRepository<>));

            //connect to database
            services.AddDbContext<SportsProContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("SportsProContext")));

            //Identity user and roles 
            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<SportsProContext>();

            //Configure password options for user 
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
            });

            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
                options.AppendTrailingSlash = true;
            });

         }
        
        //THE FOLLOWING COMMENTED OUT WHEN PUBLISHING PROJECT AS A WEBSITE

        //The following is used to created admin and user roles using ASP.NET CORE identity templetes

        /*private async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            IdentityResult roleResult;
            //Adding Admin Role
            var roleCheck = await RoleManager.RoleExistsAsync("Admin");
            if (!roleCheck)
            {
                //create the roles and seed them to the database
                roleResult = await RoleManager.CreateAsync(new IdentityRole("Admin"));
            }


            //Assign Admin role to the main User here we have given our newly registered 
            //login id for Admin management
            IdentityUser user = await UserManager.FindByEmailAsync("admin@sportsprosoftware.com");
            var User = new IdentityUser();
            await UserManager.AddToRoleAsync(user, "Admin");
        }
        private async Task CreateNewRoles(IServiceProvider serviceProvider)
             {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            IdentityResult roleResult;
            var roleCheck = await RoleManager.RoleExistsAsync("Tech");
                if (!roleCheck)
                {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole("Tech"));
                }
                IdentityUser user = await UserManager.FindByNameAsync("tech@sportsprosoftware.com");
                var User = new IdentityUser();
                await UserManager.AddToRoleAsync(user, "Tech");
        }*/


       
        // Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services)
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
            
            app.UseAuthentication();//Require for login roles/users. Authentication must come before Authorization
            app.UseAuthorization();

            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            //FOLLOWING 2 LINES COMMENTED OUT TO PUBLISH PROJECT AS A WEBSITE
             
            //CreateUserRoles(services).Wait();
            //CreateNewRoles(services).Wait();


        }
    }
}