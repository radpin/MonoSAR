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
    [Route("api/Operations")]
    public class OperationsController : Controller
    {
        private readonly monosarsqlContext _context;

        public OperationsController(IConfiguration config)
        {
            this._context = new monosarsqlContext(config);
        }

        // GET: api/Operations
        [HttpGet]
        public IEnumerable<Operation> GetOperation()
        {
            return _context.Operation.OrderBy(x => x.OperationId);
        }

        // GET: api/Operations/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOperation([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var operation = await _context.Operation.SingleOrDefaultAsync(m => m.OperationId == id);

            if (operation == null)
            {
                return NotFound();
            }

            return Ok(operation);
        }

        // PUT: api/Operations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOperation([FromRoute] int id, [FromBody] Operation operation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != operation.OperationId)
            {
                return BadRequest();
            }

            _context.Entry(operation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OperationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Operations
        [HttpPost]
        public async Task<IActionResult> PostOperation([FromBody] Operation operation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Operation.Add(operation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOperation", new { id = operation.OperationId }, operation);
        }

        // DELETE: api/Operations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOperation([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var operation = await _context.Operation.SingleOrDefaultAsync(m => m.OperationId == id);
            if (operation == null)
            {
                return NotFound();
            }

            _context.Operation.Remove(operation);
            await _context.SaveChangesAsync();

            return Ok(operation);
        }

        private bool OperationExists(int id)
        {
            return _context.Operation.Any(e => e.OperationId == id);
        }
    }
}