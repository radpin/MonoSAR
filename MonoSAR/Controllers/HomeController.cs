using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MonoSAR.Models;

namespace MonoSAR.Controllers
{
    public class HomeController : Controller
    {

        private Models.ApplicationSettings _applicationSettings;
        private Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> _userManager;


        public HomeController(Microsoft.Extensions.Configuration.IConfiguration config, Microsoft.Extensions.Options.IOptions<ApplicationSettings> settings, Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> usermanager)
        {
            //var context = new Models.DB.monosarsqlContext(config);
            _applicationSettings = settings.Value;
            _userManager = usermanager;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            Models.HomeScreen homeScreen = new HomeScreen();
            homeScreen.LogoPath = _applicationSettings.HomeImagePath;

            var x = User;
            
            return View(homeScreen);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
