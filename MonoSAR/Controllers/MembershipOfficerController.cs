using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        [Authorize(Roles = "Admin,Membership,Training")]
        [HttpGet]
        public ActionResult MemberReport()
        {

            var memberList = this.memberSummaryItems();


            return View(memberList);
        }


        [Authorize(Roles = "Admin,Membership,Training")]
        [HttpGet]
        public ActionResult CandidateReport()
        {
            //so in order to re-use code a bit I get a full (no where clause) list from this.memberSummaryItems()
            //then we filter using linq
            
            var memberList = this.memberSummaryItems();


            var candidates = (from m in memberList
                              where m.Capacity.ToLower() == "candidate"
                              select m).ToList();

            Models.Membership.MemberSummary ms = new Models.Membership.MemberSummary();
            ms.AddRange(candidates);


            return View(ms);
        }

        [Authorize(Roles = "Admin,Membership,Training")]
        [HttpGet]
        public ActionResult CandidateReportCurrentYear()
        {
            //so in order to re-use code a bit I get a full (no where clause) list from this.memberSummaryItems()
            //then we filter using linq

            var memberList = this.memberSummaryItems();


            var candidates = (from m in memberList
                              where m.Capacity.ToLower() == "candidate" && m.Joined.Year == DateTime.UtcNow.Year
                              select m).ToList();


            Models.Membership.MemberSummary ms = new Models.Membership.MemberSummary();
            ms.AddRange(candidates);


            return View(ms);
        }

        [Authorize(Roles = "Admin,Membership,Training")]
        [HttpGet]
        public ActionResult ExpiredMedicalReport()
        {
            //so in order to re-use code a bit I get a full (no where clause) list from this.memberSummaryItems()
            //then we filter using linq

            var memberList = this.memberSummaryItems();


            var candidates = (from m in memberList
                              where m.IsMedicalExpired == true
                              select m).ToList();

            Models.Membership.MemberSummary ms = new Models.Membership.MemberSummary();
            ms.AddRange(candidates);


            return View(ms);
        }


        [Authorize(Roles = "Admin,Membership,Training")]
        [HttpGet]
        public ActionResult ExpiredCPRReport()
        {
            //so in order to re-use code a bit I get a full (no where clause) list from this.memberSummaryItems()
            //then we filter using linq

            var memberList = this.memberSummaryItems();


            var candidates = (from m in memberList
                              where m.IsCPRExpired == true
                              select m).ToList();

            Models.Membership.MemberSummary ms = new Models.Membership.MemberSummary();
            ms.AddRange(candidates);


            return View(ms);
        }

        [Authorize(Roles = "Admin,Membership,Training")]
        [HttpGet]
        public ActionResult RescueReport()
        {
            //so in order to re-use code a bit I get a full (no where clause) list from this.memberSummaryItems()
            //then we filter using linq

            var memberList = this.memberSummaryItems();


            var candidates = (from m in memberList
                              where m.Capacity.ToLower() == "rescue"
                              select m).ToList();

            Models.Membership.MemberSummary ms = new Models.Membership.MemberSummary();
            ms.AddRange(candidates);


            return View(ms);
        }

        [Authorize(Roles = "Admin,Membership,Training")]
        [HttpGet]
        public ActionResult WinterReadyReport()
        {
            //so in order to re-use code a bit I get a full (no where clause) list from this.memberSummaryItems()
            //then we filter using linq

            var memberList = this.memberSummaryItems();


            var candidates = (from m in memberList
                              where m.IsWinterFieldReady == true
                              select m).ToList();

            Models.Membership.MemberSummary ms = new Models.Membership.MemberSummary();
            ms.AddRange(candidates);


            return View(ms);
        }

        [Authorize(Roles = "Admin,Membership,Training")]
        [HttpGet]
        public ActionResult SummerReadyReport()
        {
            //so in order to re-use code a bit I get a full (no where clause) list from this.memberSummaryItems()
            //then we filter using linq

            var memberList = this.memberSummaryItems();


            var candidates = (from m in memberList
                              where m.IsSummerFieldReady == true
                              select m).ToList();


            Models.Membership.MemberSummary ms = new Models.Membership.MemberSummary();
            ms.AddRange(candidates);


            return View(ms);
        }

        // GET: MembershipOfficer/Details/5
        [Authorize(Roles = "Admin,Membership")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMember(Models.Membership.MemberInsert model)
        {

            Models.DB.Member dbMember = new Models.DB.Member();
            Int32 newID;

            dbMember.Address = model.Address;
            dbMember.CapacityId = model.CapacityID;
            dbMember.City = model.City;
            dbMember.Email = model.Email;
            dbMember.FirstName = model.FirstName;
            dbMember.Ham = model.LastName;
            dbMember.Joined = model.Joined;
            dbMember.LastName = model.LastName;
            dbMember.PhoneCell = model.PhoneCell;
            dbMember.PhoneHome = model.PhoneHome;
            dbMember.PhoneWork = model.PhoneWork;
            dbMember.State = model.State;
            dbMember.Status = String.Empty; //need to remove that from the db at some point
            dbMember.Zipcode = model.Zip;
            
            if (dbMember.PhoneCell == null)
            { dbMember.PhoneCell = String.Empty; }
            if (dbMember.PhoneHome == null)
            { dbMember.PhoneHome = String.Empty; }
            if (dbMember.PhoneWork == null)
            { dbMember.PhoneWork = String.Empty; }

            if (dbMember.Ham == null)
            { dbMember.Ham = String.Empty; }

            try
            {
                _context.Member.Add(dbMember);
                _context.SaveChanges();
                newID = dbMember.MemberId;
            }
            catch (Exception exc)
            {
                throw exc;
            }



            return View("Thanks",newID);
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
        [Authorize(Roles = "Admin,Membership")]
        public ActionResult ViewMember(int id)
        {
            var query = (from m in _context.Member
                         where m.MemberId == id
                         select m).FirstOrDefault();

            if (query == null)
            { throw new Exception("Unable to locate member."); }

            _context.Member.Include(x => x.MemberCertification).ThenInclude(y => y.Certification).Load();
            _context.Member.Include(x => x.MemberCpr).ThenInclude(y => y.Cpr).Load();
            _context.Member.Include(x => x.MemberMedical).ThenInclude(y => y.Medical).Load();
            _context.Member.Include(x => x.Capacity).Load();
            _context.Member.Include(x => x.TrainingClassStudent).ThenInclude(y => y.TrainingClass).ThenInclude(z=>z.Training).Load();

            var model = new Models.Membership.MemberSummaryItem(query, _applicationOptions, _config);

            return View(model);
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

        private Models.Membership.MemberSummary memberSummaryItems()
        {
            Models.Membership.MemberSummary memberList = new Models.Membership.MemberSummary();

            var dbmems = (from m in _context.Member
                          orderby m.LastName, m.FirstName
                          select m).ToList();

            _context.Member.Include(x => x.MemberCertification).ThenInclude(y => y.Certification).Load();
            _context.Member.Include(x => x.MemberCpr).ThenInclude(y => y.Cpr).Load();
            _context.Member.Include(x => x.MemberMedical).ThenInclude(y => y.Medical).Load();
            _context.Member.Include(x => x.Capacity).Load();
            _context.Member.Include(x => x.TrainingClassStudent).ThenInclude(y => y.TrainingClass).ThenInclude(z => z.Training).Load();
            
            foreach (var x in dbmems)
            {
                memberList.Add(new Models.Membership.MemberSummaryItem(x, _applicationOptions, _config));
            }

            return memberList;
        }
    }
}