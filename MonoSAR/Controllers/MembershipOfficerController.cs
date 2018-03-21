using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MonoSAR.Controllers
{
    public class MembershipOfficerController : Controller
    {
        // GET: MembershipOfficer
        public ActionResult Index()
        {
            return View();
        }

        // GET: MembershipOfficer/Details/5
        public ActionResult Details(int id)
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