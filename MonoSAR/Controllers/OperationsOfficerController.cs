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
            var operationListItems = _context.Operation
                .OrderByDescending(o => o.OperationStart)
                .Select(o => new Models.Operations.OperationListItem() {
                    ID = o.OperationId,
                    OperationNumber = o.OperationNumber,
                    SequenceNumber = o.SequenceNumber,
                    Start = o.OperationStart,
                    End = o.OperationEnd,
                    Title = o.Title,
                    NumParticipants = o.OperationMember.Where(p => p.OperationId == o.OperationId).Count()
                })
                .ToList();

            return View(operationListItems);
        }

        // GET: OperationsOfficer/ViewOperation/5
        [Authorize]
        public ActionResult ViewOperation(int id)
        {
            var query = _context.Operation
                .Where(o => o.OperationId == id)
                .FirstOrDefault();

            if (query == null)
            { throw new Exception("Invalid operation ID."); }

            //Explicit loading because EF Core isn't lazy
            _context.Operation.Include(x => x.OperationMember).ThenInclude(y => y.Member).Load();

            Models.Operations.OperationSummaryItem model = new Models.Operations.OperationSummaryItem(query);

            return View(model);
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

        // POST: OperationsOfficer/CreateOperation
        [Authorize(Roles = "Admin,Operations")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOperation(Models.Operations.OperationInsert model)
        {

            Models.DB.Operation dbOperation = new Models.DB.Operation();
            Int32 operationId;

            dbOperation.OperationNumber = model.OperationNumber;
            dbOperation.SequenceNumber = model.SequenceNumber;
            dbOperation.OperationStart = model.Start;
            dbOperation.OperationEnd = model.End;
            dbOperation.Title = model.Title;
            dbOperation.Notes = model.Notes;
            dbOperation.Created = DateTime.UtcNow;

            try
            {
                _context.Operation.Add(dbOperation);
                _context.SaveChanges();
                operationId = dbOperation.OperationId;
            }
            catch (Exception exc)
            {
                throw exc;
            }

            return RedirectToAction("Edit", new { id = operationId });
        }

        // GET: OperationsOfficer/Edit/5
        [Authorize(Roles = "Admin,Operations")]
        public ActionResult Edit(int id)
        {
            var query = _context.Operation
                .Where(o => o.OperationId == id)
                .FirstOrDefault();

            if (query == null)
            { throw new Exception("Invalid operation ID."); }

            //Explicit loading because EF Core isn't lazy
            _context.Operation.Include(x => x.OperationMember).ThenInclude(y => y.Member).Load();

            Models.Operations.OperationSummaryItem model = new Models.Operations.OperationSummaryItem(query);

            return View(model);
        }

        // POST: OperationsOfficer/Edit
        [Authorize(Roles = "Admin,Operations")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Models.Operations.OperationSummaryItem viewModel)
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