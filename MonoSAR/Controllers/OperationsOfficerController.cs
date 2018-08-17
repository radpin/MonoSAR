using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MonoSAR.Models;

namespace MonoSAR.Controllers
{
    public class OperationsOfficerController : Controller
    {
        private Models.DB.monosarsqlContext _context;
        private IConfiguration _config;
        private Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> _userManager;
        private IOptions<ApplicationSettings> _applicationOptions;

        public OperationsOfficerController(IConfiguration config, Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> usermanager, IOptions<ApplicationSettings> options)
        {
            this._context = new Models.DB.monosarsqlContext(config);
            this._config = config;
            this._userManager = usermanager;
            this._applicationOptions = options;
        }

        // GET: OperationsOfficer
        [Authorize]
        [System.Web.Mvc.HttpGet]
        public ActionResult Index()
        {
            var operationSummaryItems = _context.Operation
                .OrderByDescending(o => o.OperationStart)
                .Select(o => new Models.Operations.OperationSummaryItem(o))
                .ToList();

            return View(operationSummaryItems);
        }

        // GET: OperationsOfficer/ViewOperation/5
        [Authorize]
        public ActionResult ViewOperation(int id)
        {
            var operationSummaryItem = _context.Operation
                .Select(o => new Models.Operations.OperationSummaryItem(o))
                .Where(o => o.ID == id)
                .FirstOrDefault();

            if (operationSummaryItem == null)
            { throw new Exception("Invalid operation ID."); }

            return View(operationSummaryItem);
        }

        // GET: OperationOfficer/CreateOperation
        [Authorize(Roles = "Admin,Operations")]
        [HttpGet]
        public ActionResult CreateOperation()
        {
            Models.Operations.OperationInsert model = new Models.Operations.OperationInsert();
            model.Start = DateTime.Now;
            model.End = DateTime.Now;

            return View(model);
        }

        // GET: OperationsOfficer/OperationInsert
        [Authorize(Roles = "Admin,Operations")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOperation(Models.Operations.OperationInsert model)
        {

            Models.DB.Operation dbOperation = new Models.DB.Operation();
            Int32 newID;

            dbOperation.OperationNumber = model.OperationNumber;
            dbOperation.SequenceNumber = model.SequenceNumber;
            dbOperation.OperationStart = model.Start;
            dbOperation.OperationEnd = model.End;
            dbOperation.Title = model.Title;
            dbOperation.Notes = model.Notes;
            dbOperation.Created = DateTime.Now;

            try
            {
                _context.Operation.Add(dbOperation);
                _context.SaveChanges();
                newID = dbOperation.OperationId;
            }
            catch (Exception exc)
            {
                throw exc;
            }

            return View("Thanks", newID);
        }

        // GET: OperationsOfficer/Edit/5
        [Authorize(Roles = "Admin,Operations")]
        public ActionResult Edit(int id)
        {
            var operationSummaryItem = _context.Operation
                .Select(o => new Models.Operations.OperationUpdate(o))
                .Where(o => o.ID == id)
                .FirstOrDefault();

            if (operationSummaryItem == null)
            { throw new Exception("Invalid operation ID."); }

            return View(operationSummaryItem);

        }


        // GET: OperationsOfficer/Edit
        [Authorize(Roles = "Admin,Operations")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Models.Operations.OperationUpdate viewModel)
        {
            try
            {
                var operation = _context.Operation
                    .Where(o => o.OperationId == viewModel.ID)
                    .FirstOrDefault();

                operation.OperationNumber = viewModel.OperationNumber;
                operation.SequenceNumber = viewModel.SequenceNumber;
                operation.OperationStart = viewModel.Start;
                operation.OperationEnd = viewModel.End;
                operation.Title = viewModel.Title;
                operation.Notes = viewModel.Notes;

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