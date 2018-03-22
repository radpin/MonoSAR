using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MonoSAR.Models;

namespace MonoSAR.Controllers
{
    public class MembershipOfficerController : Controller
    {
        private Models.DB.monosarsqlContext _context;
        private IConfiguration _config;
        private Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> _userManager;
        private IOptions<ApplicationSettings> _applicationOptions;


        public MembershipOfficerController(IConfiguration config, Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> usermanager, IOptions<ApplicationSettings> options)
        {
            this._context = new Models.DB.monosarsqlContext(config);
            this._config = config;
            this._userManager = usermanager;
            this._applicationOptions = options;
        }


        // GET: MembershipOfficer
        public ActionResult Index()
        {
            return View();
        }

        // GET: MembershipOfficer/Details/5
        [Authorize(Roles = "Admin,Membership")]
        [HttpGet]
        public ActionResult CreateMember()
        {

            Models.Membership.MemberInsert model = new Models.Membership.MemberInsert();

            var caps = (from c in _context.Capacity
                        select c).ToList();

            model.Joined = DateTime.Now;
            model.CapacityStubs = new Models.Membership.CapacityStubs(caps);

            return View(model);
        }

        // GET: MembershipOfficer/Details/5
        [Authorize(Roles = "Admin,Membership")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMember(Models.Membership.MemberInsert model)
        {
            return View();
        }



        // POST: MembershipOfficer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MembershipOfficer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MembershipOfficer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MembershipOfficer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MembershipOfficer/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}