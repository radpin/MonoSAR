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
    [Route("api/Trainings")]
    public class TrainingsController : Controller
    {
        private readonly monosarsqlContext _context;
        
        


        public TrainingsController(IConfiguration config)
        {

            //if (!User.Identity.IsAuthenticated)
            //{ Response.Redirect("/"); }

            this._context = new monosarsqlContext(config);
        }


        // GET: api/Trainings
        [HttpGet]
        public IEnumerable<Training> GetTraining()
        {

            return _context.Training;
        }

        // GET: api/Trainings/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTraining([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var training = await _context.Training.SingleOrDefaultAsync(m => m.TrainingId == id);

            if (training == null)
            {
                return NotFound();
            }

            return Ok(training);
        }

        // PUT: api/Trainings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTraining([FromRoute] int id, [FromBody] Training training)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != training.TrainingId)
            {
                return BadRequest();
            }

            _context.Entry(training).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainingExists(id))
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

        // POST: api/Trainings
        [HttpPost]
        public async Task<IActionResult> PostTraining([FromBody] Training training)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Training.Add(training);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTraining", new { id = training.TrainingId }, training);
        }

        // DELETE: api/Trainings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTraining([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var training = await _context.Training.SingleOrDefaultAsync(m => m.TrainingId == id);
            if (training == null)
            {
                return NotFound();
            }

            _context.Training.Remove(training);
            await _context.SaveChangesAsync();

            return Ok(training);
        }

        private bool TrainingExists(int id)
        {
            return _context.Training.Any(e => e.TrainingId == id);
        }
    }
}