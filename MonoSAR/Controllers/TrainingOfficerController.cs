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


            var query = (from tm in _context.TrainingMember
                         orderby tm.Created descending, tm.TrainingDate descending
                         select tm).Take(100);

            //Explicit loading because EF Core isn't lazy
            _context.TrainingMember.Include(x => x.Member).Load();
            _context.TrainingMember.Include(x => x.Training).Load();

            //converting from data models to the view model (dto), the conversions happen in the view model / dto's constructor
            Models.Training.TrainingSummary model = new Models.Training.TrainingSummary(query);


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
        [Authorize(Roles = "Admin,Training")]
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
        [Authorize(Roles = "Admin,Training")]
        public ActionResult Edit(int id)
        {
            var query = (from tm in _context.TrainingMember
                         where tm.TrainingMemberId == id
                         select tm).FirstOrDefault();

            //Explicit loading because EF Core isn't lazy
            _context.TrainingMember.Include(x => x.Member).Load();
            _context.TrainingMember.Include(x => x.Training).Load();

            //converting from data models to the view model (dto), the conversions happen in the view model / dto's constructor
            Models.Training.TrainingSummaryItem model = new Models.Training.TrainingSummaryItem(query);


            return View(model);

        }

        // POST: TrainingOfficer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Training")]
        public ActionResult Edit(Models.Training.TrainingSummaryItem viewModel)
        {
            // only updating hours and date at the moment

            try {

                var query = (from tm in _context.TrainingMember
                             where tm.TrainingMemberId == viewModel.TrainingMemberID
                             select tm).FirstOrDefault();

                query.TrainingHours = viewModel.Hours;
                query.TrainingDate = viewModel.When;

                _context.SaveChanges();

                return RedirectToAction(nameof(Index));

            }
            catch (Exception exc)
            {
                throw exc;
            }

        }

        // POST: TrainingOfficer/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Training")]
        public ActionResult Delete(Models.Training.TrainingSummaryItem viewModel)
        {
            try
            {
                var del = new Models.DB.TrainingMember { TrainingMemberId = viewModel.TrainingMemberID };
                _context.TrainingMember.Attach(del);
                _context.Remove(del);

                _context.SaveChanges();

                return RedirectToAction(nameof(Index));

            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
    }
}