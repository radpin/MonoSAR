using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MonoSAR.Data;
using MonoSAR.Models;
using MonoSAR.Services;

namespace MonoSAR
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
            //get sql connection string
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration["sqlconnectionstring"]));

            //built-in individual user accounts
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            //load up appsettings.json into ApplicationSettings
            services.Configure<Models.ApplicationSettings>(Configuration.GetSection("AppSettings"));


            services.AddMvc();



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider provider)
        {

             CreateRoles(provider).Wait();


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });



        }



        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            //initializing custom roles 
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            string[] roleNames = { "Admin", "Training", "Operations", "Membership", "Individual" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                // ensure that the role does not exist
                if (!roleExist)
                {
                    //create the roles and seed them to the database: 
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName)); 
                }
            }

            //// find the user with the admin email 

            var _barry = await UserManager.FindByEmailAsync("bearbnz@yahoo.com");
            var _eric = await UserManager.FindByEmailAsync("radpin@gmail.com");

            //await UserManager.AddToRoleAsync(_barry, "Training");
            //await UserManager.AddToRoleAsync(_eric, "Admin");

            //UserManager.AddToRoleAsync(_barry, "Training").Wait();
            //UserManager.AddToRoleAsync(_eric, "Admin").Wait();

            // check if the user exists
            if (_barry != null)
            {
                await UserManager.AddToRoleAsync(_barry, "Training");

            }



            // check if the user exists
            if (_eric != null)
            {
                await UserManager.AddToRoleAsync(_eric, "Admin");

            }


            ////Here you could create the super admin who will maintain the web app
            //var poweruser = new ApplicationUser
            //{
            //    UserName = "Admin",
            //    Email = "admin@email.com",
            //};
            //string adminPassword = "p@$$w0rd";

            //var createPowerUser = await UserManager.CreateAsync(poweruser, adminPassword);
            //if (createPowerUser.Succeeded)
            //{
            //    //here we tie the new user to the role
            //    await UserManager.AddToRoleAsync(poweruser, "Admin");

            //}

        }




    }
}
