using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MonoSAR.Models.DB;

namespace MonoSAR.Controllers
{
    [Produces("application/json")]
    [Route("api/Members")]
    public class MembersController : Controller
    {
        private readonly monosarsqlContext _context;

        public MembersController(IConfiguration config)
        {

            this._context = new monosarsqlContext(config);
        }

        // GET: api/Members
        [HttpGet]
        [Authorize]
        public IEnumerable<Member> GetMember()
        {

            var query = from m in _context.Member
                        where m.Capacity.CapacityName.ToLower() != "inactive"
                        orderby m.LastName ascending
                        select m;

            return query.ToList();
        }

        private bool MemberExists(int id)
        {
            return _context.Member.Any(e => e.MemberId == id);
        }
    }
}