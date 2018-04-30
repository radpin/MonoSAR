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
    [Route("api/CPR")]
    public class CPRController : Controller
    {
        private readonly monosarsqlContext _context;

        public CPRController(IConfiguration config)
        {
            this._context = new monosarsqlContext(config);
        }
                
        // GET: api/CPR
        [HttpGet]
        public IEnumerable<Cpr> GetCpr()
        {
            return _context.Cpr;
        }

        // GET: api/CPR/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCpr([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cpr = await _context.Cpr.SingleOrDefaultAsync(m => m.Cprid == id);

            if (cpr == null)
            {
                return NotFound();
            }

            return Ok(cpr);
        }

        // PUT: api/CPR/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCpr([FromRoute] int id, [FromBody] Cpr cpr)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cpr.Cprid)
            {
                return BadRequest();
            }

            _context.Entry(cpr).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CprExists(id))
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

        // POST: api/CPR
        [HttpPost]
        public async Task<IActionResult> PostCpr([FromBody] Cpr cpr)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Cpr.Add(cpr);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCpr", new { id = cpr.Cprid }, cpr);
        }

        // DELETE: api/CPR/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCpr([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cpr = await _context.Cpr.SingleOrDefaultAsync(m => m.Cprid == id);
            if (cpr == null)
            {
                return NotFound();
            }

            _context.Cpr.Remove(cpr);
            await _context.SaveChangesAsync();

            return Ok(cpr);
        }

        private bool CprExists(int id)
        {
            return _context.Cpr.Any(e => e.Cprid == id);
        }
    }
}