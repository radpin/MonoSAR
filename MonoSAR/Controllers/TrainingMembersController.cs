using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonoSAR.Models.DB;

namespace MonoSAR.Controllers
{
    [Produces("application/json")]
    [Route("api/TrainingMembers")]
    public class TrainingMembersController : Controller
    {
        private readonly monosarsqlContext _context;

        public TrainingMembersController(monosarsqlContext context)
        {
            _context = context;
        }

        // GET: api/TrainingMembers
        [HttpGet]
        public IEnumerable<TrainingMember> GetTrainingMember()
        {
            return _context.TrainingMember;
        }

        // GET: api/TrainingMembers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrainingMember([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trainingMember = await _context.TrainingMember.SingleOrDefaultAsync(m => m.TrainingMemberId == id);

            if (trainingMember == null)
            {
                return NotFound();
            }

            return Ok(trainingMember);
        }

        // PUT: api/TrainingMembers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrainingMember([FromRoute] int id, [FromBody] TrainingMember trainingMember)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != trainingMember.TrainingMemberId)
            {
                return BadRequest();
            }

            _context.Entry(trainingMember).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainingMemberExists(id))
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

        // POST: api/TrainingMembers
        [HttpPost]
        public async Task<IActionResult> PostTrainingMember([FromBody] TrainingMember trainingMember)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.TrainingMember.Add(trainingMember);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrainingMember", new { id = trainingMember.TrainingMemberId }, trainingMember);
        }

        // DELETE: api/TrainingMembers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainingMember([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trainingMember = await _context.TrainingMember.SingleOrDefaultAsync(m => m.TrainingMemberId == id);
            if (trainingMember == null)
            {
                return NotFound();
            }

            _context.TrainingMember.Remove(trainingMember);
            await _context.SaveChangesAsync();

            return Ok(trainingMember);
        }

        private bool TrainingMemberExists(int id)
        {
            return _context.TrainingMember.Any(e => e.TrainingMemberId == id);
        }
    }
}