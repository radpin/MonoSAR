﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MonoSAR.Controllers
{
    public class TrainingOfficerController : Controller
    {
        // GET: TrainingOfficer
        public ActionResult Index()

        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateOccurrence(Models.Training.TrainingOccurrenceInsert toi)
        {
            List<Models.Training.TrainingOccurrenceParticipationInsert> topiList = new List<Models.Training.TrainingOccurrenceParticipationInsert>();

            var x = HttpContext.Request.Form;

            Int32 i = 0;

            foreach (var item in x)
            {
                

                if (item.Key == "member")
                {
                    foreach (var y in item.Value)
                    {
                        //gets the memberid
                        Models.Training.TrainingOccurrenceParticipationInsert topi = new Models.Training.TrainingOccurrenceParticipationInsert();
                        String maybememberid = y.ToString();
                        topi.MemberID = Int32.Parse(y.ToString());
                        
                        //get the hours
                        string maybeHours = x["hours"][i];
                        topi.Hours = Int32.Parse(maybeHours);

                        //keeping a counter so (above) we know how to find the hours by index. ie: for the third member, we look for the third set of hours.
                        topiList.Add(topi);
                        i = i + 1;
                    }

                }
            }
            



            Index();
            return null;
        }

        // GET: TrainingOfficer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        

        // GET: TrainingOfficer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TrainingOfficer/Create
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

        // GET: TrainingOfficer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TrainingOfficer/Edit/5
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

        // GET: TrainingOfficer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TrainingOfficer/Delete/5
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