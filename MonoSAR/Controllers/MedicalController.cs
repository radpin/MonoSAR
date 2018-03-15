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
    [Route("api/Medical")]
    public class MedicalController : Controller
    {
        private readonly monosarsqlContext _context;

        public MedicalController(IConfiguration config)
        {
            this._context = new monosarsqlContext(config);
        }

        // GET: api/Medical
        [HttpGet]
        public IEnumerable<Medical> GetMedical()
        {
            return _context.Medical;
        }

        // GET: api/Medicals/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMedical([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var medical = await _context.Medical.SingleOrDefaultAsync(m => m.MedicalId == id);

            if (medical == null)
            {
                return NotFound();
            }

            return Ok(medical);
        }

        // PUT: api/Medicals/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedical([FromRoute] int id, [FromBody] Medical medical)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != medical.MedicalId)
            {
                return BadRequest();
            }

            _context.Entry(medical).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicalExists(id))
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

        // POST: api/Medicals
        [HttpPost]
        public async Task<IActionResult> PostMedical([FromBody] Medical medical)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Medical.Add(medical);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMedical", new { id = medical.MedicalId }, medical);
        }

        // DELETE: api/Medicals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedical([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var medical = await _context.Medical.SingleOrDefaultAsync(m => m.MedicalId == id);
            if (medical == null)
            {
                return NotFound();
            }

            _context.Medical.Remove(medical);
            await _context.SaveChangesAsync();

            return Ok(medical);
        }

        private bool MedicalExists(int id)
        {
            return _context.Medical.Any(e => e.MedicalId == id);
        }
    }
}