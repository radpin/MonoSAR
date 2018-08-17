using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MonoSAR.Models.DB;

namespace MonoSAR.Controllers
{
    [Produces("application/json")]
    [Route("api/OperationMember")]
    public class OperationMemberController : Controller
    {
        private readonly monosarsqlContext _context;

        public OperationMemberController(IConfiguration config)
        {
            this._context = new monosarsqlContext(config);
        }

        // GET: api/OperationMember
        [HttpGet]
        public IEnumerable<OperationMember> GetOperationMember()
        {
            return _context.OperationMember.OrderBy(x => x.OperationMemberId);
        }

        // GET: api/OperationMember/5/5
        [HttpGet("{operationId}/{memberId}")]
        public async Task<IActionResult> GetOperationMember([FromRoute] int operationId, [FromRoute] int memberId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var operationMember = await _context.OperationMember.SingleOrDefaultAsync(m => m.OperationId == operationId && m.MemberId == memberId);

            if (operationMember == null)
            {
                return NotFound();
            }

            return Ok(operationMember);
        }

        // PUT: api/OperationMember/5/5
        [HttpPut("{operationId}/{memberId}")]
        public async Task<IActionResult> PutOperationMember([FromRoute] int operationId, [FromRoute] int memberId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!OperationMemberExists(operationId, memberId))
            {
                var operationMember = new OperationMember()
                {
                    OperationId = operationId,
                    MemberId = memberId,
                    Created = DateTime.UtcNow
                };

                try
                {
                    _context.OperationMember.Add(operationMember);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OperationMemberExists(operationId, memberId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return NoContent();
        }

        private bool OperationMemberExists(int operationId, int memberId)
        {
            return _context.OperationMember.Any(e => e.OperationId == operationId && e.MemberId == memberId);
        }

        // DELETE: api/Operations/5
        [HttpDelete("{operationId}/{memberId}")]
        public async Task<IActionResult> DeleteOperationMember([FromRoute] int operationId, [FromRoute] int memberId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var operationMember = await _context.OperationMember.SingleOrDefaultAsync(m => m.OperationId == operationId && m.MemberId == memberId);
            if (operationMember == null)
            {
                return NotFound();
            }

            _context.OperationMember.Remove(operationMember);
            await _context.SaveChangesAsync();

            return Ok(operationMember);
        }

        private bool OperationExists(int id)
        {
            return _context.Operation.Any(e => e.OperationId == id);
        }
    }
}