using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MonoSAR.Models.DB;
using MonoSAR.Models.Training;

namespace MonoSAR.Controllers
{
    [Produces("application/json")]
    [Route("api/TrainingClassInstructor")]
    public class TrainingClassInstructorController : Controller
    {
        private readonly monosarsqlContext _context;

        public TrainingClassInstructorController(IConfiguration config)
        {
            this._context = new monosarsqlContext(config);
        }

        // GET: api/TrainingClassInstructor
        [HttpGet]
        public IEnumerable<TrainingClassInstructor> GetTrainingClassInstructor()
        {
            return _context.TrainingClassInstructor.OrderBy(x => x.TrainingClassInstructorId);
        }

        // GET: api/TrainingClassInstructor/5
        [HttpGet("{trainingClassInstructorId}")]
        public async Task<IActionResult> GetTrainingClassInstructor([FromRoute] int trainingClassInstructorId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trainingClassInstructor = await _context.TrainingClassInstructor.SingleOrDefaultAsync(m => m.TrainingClassInstructorId == trainingClassInstructorId);

            if (trainingClassInstructor == null)
            {
                return NotFound();
            }

            return Ok(trainingClassInstructor);
        }

        // PUT: api/TrainingClassInstructor/5
        [HttpPut("{trainingClassId}")]
        public async Task<IActionResult> PutTrainingClassInstructor([FromRoute] int trainingClassId, [FromBody] TrainingClassParticipant trainingClassParticipant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TrainingClassInstructorExists(trainingClassId, trainingClassParticipant.MemberID))
            {
                var trainingClassInstructor = new TrainingClassInstructor()
                {
                    TrainingClassId = trainingClassId,
                    TrainingClassInstructorMemberId = trainingClassParticipant.MemberID,
                    TrainingClassStudentHours = trainingClassParticipant.Hours,
                    Created = DateTime.UtcNow
                };

                try
                {
                    _context.TrainingClassInstructor.Add(trainingClassInstructor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainingClassInstructorExists(trainingClassId, trainingClassParticipant.MemberID))
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

        private bool TrainingClassInstructorExists(int trainingClassId, int memberId)
        {
            return _context.TrainingClassInstructor.Any(e => e.TrainingClassId == trainingClassId && e.TrainingClassInstructorMemberId == memberId);
        }

        // DELETE: api/TrainingClassInstructor/5
        [HttpDelete("{trainingClassInstructorId}")]
        public async Task<IActionResult> DeleteTrainingClassInstructor([FromRoute] int trainingClassInstructorId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trainingClassInstructor = await _context.TrainingClassInstructor.SingleOrDefaultAsync(m => m.TrainingClassInstructorId == trainingClassInstructorId);
            if (trainingClassInstructor == null)
            {
                return NotFound();
            }

            _context.TrainingClassInstructor.Remove(trainingClassInstructor);
            await _context.SaveChangesAsync();

            return Ok(trainingClassInstructor);
        }
    }
}