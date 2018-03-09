using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MonoSAR.Models;

namespace MonoSAR.Controllers
{
    public class HomeController : Controller
    {

        public HomeController(Microsoft.Extensions.Configuration.IConfiguration config)
        {
            var context = new Models.DB.monosarsqlContext(config);
        }

        public IActionResult Index()
        {

            if (!User.Identity.IsAuthenticated)
            {
                //throw new Exception("Not authenticated.");
            }


            return View();
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
