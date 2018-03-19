using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MonoSAR.Models.DB;
using Microsoft.EntityFrameworkCore;

namespace MonoSAR.Controllers
{
    public class TrainingOfficerController : Controller
    {
        private Models.DB.monosarsqlContext _context; 

        public TrainingOfficerController(IConfiguration config)
        {

            this._context = new monosarsqlContext(config);
        }

        // GET: TrainingOfficer
        [Authorize(Roles ="Admin,Training")]
        public ActionResult Index()
        {
            Models.Training.TrainingSummary model = new Models.Training.TrainingSummary();

            var query = (from tm in _context.TrainingMember
                         orderby tm.Created descending, tm.TrainingDate descending
                         select tm).Take(100);

            //Explicit loading because EF Core isn't lazy
            _context.TrainingMember.Include(x => x.Member).Load();
            _context.TrainingMember.Include(x => x.Training).Load();

            foreach (var item in query)
            {
                Models.Training.TrainingSummaryItem tsi = new Models.Training.TrainingSummaryItem();
                tsi.Created = item.Created;
                tsi.Hours = item.TrainingHours;
                tsi.MemberName = item.Member.LastName + ", " + item.Member.FirstName;
                tsi.TrainingTitle = item.Training.TrainingTitle;
                tsi.When = item.TrainingDate;
                tsi.TrainingMemberID = item.TrainingMemberId;

                model.Add(tsi);
            }

            return View(model); 
        }

        // GET: TrainingOfficer
        [Authorize(Roles = "Admin,Training")]
        public ActionResult RecordTrainingOccurrence()
        {

            return View();
        }


        [HttpPost]
        public ViewResult CreateOccurrence(Models.Training.TrainingOccurrenceInsert toi)
        {
            List<Models.Training.TrainingOccurrenceParticipationInsert> topiList = new List<Models.Training.TrainingOccurrenceParticipationInsert>();
            var x = HttpContext.Request.Form;
            var thanks = new Models.Training.TrainingOccurrenceInsertConfirmation();


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
                        topi.Hours = Decimal.Parse(maybeHours);

                        //keeping a counter so (above) we know how to find the hours by index. ie: for the third member, we look for the third set of hours.
                        topiList.Add(topi);
                        i = i + 1;
                    }

                }
            }


            //build ef objects to store
            foreach (var item in topiList)
            {
                Models.DB.TrainingMember trainingMember = new TrainingMember();
                trainingMember.Created = DateTime.UtcNow;
                trainingMember.MemberId = item.MemberID;
                trainingMember.TrainingDate = toi.TrainingDate;
                trainingMember.TrainingHours = item.Hours;
                trainingMember.TrainingId = toi.TrainingID;

                _context.TrainingMember.Add(trainingMember);
            }

            _context.SaveChanges();


            var hours = topiList.Sum(item => item.Hours).ToString();
            var people = topiList.Count.ToString() ;


            thanks.Message = "Great work, your team has added " + hours + " hours of training across " + people + " members."   ;


            return View("TrainingOccurrenceInsertConfirmation", thanks);
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