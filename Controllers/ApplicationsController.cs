using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using SMWebAPI.Models;

namespace SMWebAPI.Controllers
{
    [Route("api/Application")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        private readonly SMDbContext _context;

        public ApplicationsController(SMDbContext context)
        {
            _context = context;
        }

        // GET: api/Applications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Application>>> GetApplication()
        {
            return await _context.Application.ToListAsync();
        }

        // GET: api/Applications/5
        [HttpGet("{rollNumber}")]
        public async Task<ActionResult<Application>> GetApplication(int rollNumber)
        {
            var application = await _context.Application.FindAsync(rollNumber);
            var messSubject = await _context.Message_Subject.Where(a => a.RollNumber == rollNumber).ToListAsync();


            if (application == null)
            {
                return NotFound();
            }

            Application allAppAndMessage = application;
            allAppAndMessage.Message_Subject = messSubject;
            
            return allAppAndMessage;
        }

        // PUT: api/Applications/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplication(int id, Application application)
        {
            if (id != application.RollNumber)
            {
                return BadRequest();
            }

            _context.Entry(application).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationExists(id))
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

        // POST: api/Applications
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Application>> PostApplication(Application application)
        {
            _context.Application.Add(application);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ApplicationExists(application.RollNumber))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetApplication", new { id = application.RollNumber }, application);
        }

        // DELETE: api/Applications/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Application>> DeleteApplication(int id)
        {
            var application = await _context.Application.FindAsync(id);
            if (application == null)
            {
                return NotFound();
            }

            _context.Application.Remove(application);
            await _context.SaveChangesAsync();

            return application;
        }

        private bool ApplicationExists(int id)
        {
            return _context.Application.Any(e => e.RollNumber == id);
        }
    }
}
