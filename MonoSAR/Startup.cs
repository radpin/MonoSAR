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

            services.AddIdentity<ApplicationUser, IdentityRole>(config =>
            {
                config.SignIn.RequireConfirmedEmail = true;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();


            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            //load up appsettings.json into ApplicationSettings
            services.Configure<Models.ApplicationSettings>(Configuration.GetSection("AppSettings"));


            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = false;
            });



            //for email confirmation https://docs.microsoft.com/en-us/aspnet/core/security/authentication/accconfirm?view=aspnetcore-2.1&tabs=aspnetcore2x#enable-account-confirmation-and-password-recovery
            services.AddSingleton<IEmailSender, EmailSender>();
            services.Configure<AuthMessageSenderOptions>(Configuration);


            services.AddMvc();



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider provider)
        {

            CreateRoles(provider).Wait();


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseBrowserLink();
                //app.UseDatabaseErrorPage();
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

            //hard-coding the email addresses is a little dorky, better would be to have a json file of email/role names which is not in source control,
            //and then read/loop/process it.... somehow. - eric


            //initializing custom roles 
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            string[] roleNames = { "Admin", "Training", "Operations", "Membership", "Individual" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExists = await RoleManager.RoleExistsAsync(roleName);
                // ensure that the role does not exist
                if (!roleExists)
                {
                    //create the roles and seed them to the database: 
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            //// find the user with the admin email 

            var _barry = await UserManager.FindByEmailAsync("bearbnz@yahoo.com");
            var _eric = await UserManager.FindByEmailAsync("radpin@gmail.com");
            var _lisa = await UserManager.FindByEmailAsync("lisagitel@hotmail.com");
            var _jim = await UserManager.FindByEmailAsync("jgnjneer@gmail.com");
            var _kr = await UserManager.FindByEmailAsync("khansen@monosheriff.org");
            var _chris = await UserManager.FindByEmailAsync("chris.j.thompson@cox.net");

            if (_jim != null)
            {
                await UserManager.AddToRoleAsync(_jim, "Training");
                await UserManager.AddToRoleAsync(_jim, "Membership");
                await UserManager.AddToRoleAsync(_jim, "Operations");
            }

            // check if the user exists
            if (_barry != null)
            {
                await UserManager.AddToRoleAsync(_barry, "Training");
                await UserManager.AddToRoleAsync(_barry, "Membership");
                await UserManager.AddToRoleAsync(_barry, "Operations");
            }

            if (_kr != null)
            {
                await UserManager.AddToRoleAsync(_kr, "Training");
                await UserManager.AddToRoleAsync(_kr, "Membership");
            }

            // check if the user exists
            if (_lisa != null)
            {
                await UserManager.AddToRoleAsync(_lisa, "Admin");
            }


            // check if the user exists
            if (_eric != null)
            {
                await UserManager.AddToRoleAsync(_eric, "Admin");
            }


            // check if the user exists
            if (_chris != null)
            {
                await UserManager.AddToRoleAsync(_chris, "Membership");
            }

        }
    }
}
