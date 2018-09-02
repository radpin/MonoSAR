using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MonoSAR.Models.DB;
using MonoSAR.Models.Training;

namespace MonoSAR.Controllers
{
    [Produces("application/json")]
    [Route("api/TrainingClassStudent")]
    public class TrainingClassStudentController : Controller
    {
        private readonly monosarsqlContext _context;

        public TrainingClassStudentController(IConfiguration config)
        {
            this._context = new monosarsqlContext(config);
        }

        // GET: api/TrainingClassStudent
        [HttpGet]
        public IEnumerable<TrainingClassStudent> GetTrainingClassStudent()
        {
            return _context.TrainingClassStudent.OrderBy(x => x.TrainingClassStudentId);
        }

        // GET: api/TrainingClassStudent/5
        [HttpGet("{trainingClassStudentId}")]
        public async Task<IActionResult> GetTrainingClassStudent([FromRoute] int trainingClassStudentId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trainingClassStudent = await _context.TrainingClassStudent.SingleOrDefaultAsync(m => m.TrainingClassStudentId == trainingClassStudentId);

            if (trainingClassStudent == null)
            {
                return NotFound();
            }

            return Ok(trainingClassStudent);
        }

        // PUT: api/TrainingClassStudent/5
        [HttpPut("{trainingClassId}")]
        public async Task<IActionResult> PutTrainingClassStudent([FromRoute] int trainingClassId, [FromBody] TrainingClassParticipant trainingClassParticipant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TrainingClassStudentExists(trainingClassId, trainingClassParticipant.MemberID))
            {
                var trainingClassStudent = new TrainingClassStudent()
                {
                    TrainingClassId = trainingClassId,
                    TrainingClassStudentMemberId = trainingClassParticipant.MemberID,
                    TrainingClassStudentHours = trainingClassParticipant.Hours,
                    Created = DateTime.UtcNow
                };

                try
                {
                    _context.TrainingClassStudent.Add(trainingClassStudent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainingClassStudentExists(trainingClassId, trainingClassParticipant.MemberID))
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

        private bool TrainingClassStudentExists(int trainingClassId, int memberId)
        {
            return _context.TrainingClassStudent.Any(e => e.TrainingClassId == trainingClassId && e.TrainingClassStudentMemberId == memberId);
        }

        // DELETE: api/TrainingClassStudent/5
        [HttpDelete("{trainingClassStudentId}")]
        public async Task<IActionResult> DeleteTrainingClassStudent([FromRoute] int trainingClassStudentId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trainingClassStudent = await _context.TrainingClassStudent.SingleOrDefaultAsync(m => m.TrainingClassStudentId == trainingClassStudentId);
            if (trainingClassStudent == null)
            {
                return NotFound();
            }

            _context.TrainingClassStudent.Remove(trainingClassStudent);
            await _context.SaveChangesAsync();

            return Ok(trainingClassStudent);
        }

        // PATCH: api/TrainingClassStudent/5
        [HttpPatch("{trainingClassStudentId}")]
        public async Task<IActionResult> PatchTrainingClassStudent([FromRoute] int trainingClassStudentId, [FromBody] JsonPatchDocument<TrainingClassStudent> patchDocument)
        {
            // https://janaks.com.np/implementing-json-patch-in-asp-net-core/

            /*
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            */

            if (patchDocument == null)
            {
                return BadRequest();
            }

            try
            {
                var trainingClassStudent = await _context.TrainingClassStudent.SingleOrDefaultAsync(m => m.TrainingClassStudentId == trainingClassStudentId);

                if (trainingClassStudent == null)
                {
                    return NotFound();
                }

                patchDocument.ApplyTo(trainingClassStudent);
                _context.SaveChanges();

                return Ok(trainingClassStudent);
            }
            catch (Exception exc)
            {
                throw exc;
            }

        }

    }
}