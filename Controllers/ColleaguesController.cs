using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMWebAPI.Models;

namespace SMWebAPI.Controllers
{
    [Route("api/Colleague")]
    [ApiController]
    public class ColleaguesController : ControllerBase
    {
        private readonly SMDbContext _context;

        public ColleaguesController(SMDbContext context)
        {
            _context = context;
        }

        // GET: api/Colleagues
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Colleague>>> GetColleague()
        {
            return await _context.Colleague.ToListAsync();
        }

        // GET: api/Colleagues/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Colleague>> GetColleague(int id)
        {
            var colleague = await _context.Colleague.FindAsync(id);

            if (colleague == null)
            {
                return NotFound();
            }

            return colleague;
        }

        // PUT: api/Colleagues/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutColleague(int id, Colleague colleague)
        {
            if (id != colleague.ColleagueId)
            {
                return BadRequest();
            }

            _context.Entry(colleague).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ColleagueExists(id))
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

        // POST: api/Colleagues
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Colleague>> PostColleague(Colleague colleague)
        {
            _context.Colleague.Add(colleague);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ColleagueExists(colleague.ColleagueId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetColleague", new { id = colleague.ColleagueId }, colleague);
        }

        // DELETE: api/Colleagues/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Colleague>> DeleteColleague(int id)
        {
            var colleague = await _context.Colleague.FindAsync(id);
            if (colleague == null)
            {
                return NotFound();
            }

            _context.Colleague.Remove(colleague);
            await _context.SaveChangesAsync();

            return colleague;
        }

        private bool ColleagueExists(int id)
        {
            return _context.Colleague.Any(e => e.ColleagueId == id);
        }
    }
}
