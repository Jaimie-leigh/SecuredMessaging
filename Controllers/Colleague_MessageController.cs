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
    [Route("api/Colleague_Message")]
    [ApiController]
    public class Colleague_MessageController : ControllerBase
    {
        private readonly SMDbContext _context;

        public Colleague_MessageController(SMDbContext context)
        {
            _context = context;
        }

        // GET: api/Colleague_Message
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Colleague_Message>>> GetColleague_Message()
        {
            return await _context.Colleague_Message.ToListAsync();
        }

        // GET: api/Colleague_Message/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Colleague_Message>> GetColleague_Message(int id)
        {
            var colleague_Message = await _context.Colleague_Message.FindAsync(id);

            if (colleague_Message == null)
            {
                return NotFound();
            }

            return colleague_Message;
        }

        // PUT: api/Colleague_Message/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutColleague_Message(int id, Colleague_Message colleague_Message)
        {
            if (id != colleague_Message.ColleagueId)
            {
                return BadRequest();
            }

            _context.Entry(colleague_Message).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Colleague_MessageExists(id))
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

        // POST: api/Colleague_Message
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Colleague_Message>> PostColleague_Message(Colleague_Message colleague_Message)
        {
            _context.Colleague_Message.Add(colleague_Message);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (Colleague_MessageExists(colleague_Message.ColleagueId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetColleague_Message", new { id = colleague_Message.ColleagueId }, colleague_Message);
        }

        // DELETE: api/Colleague_Message/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Colleague_Message>> DeleteColleague_Message(int id)
        {
            var colleague_Message = await _context.Colleague_Message.FindAsync(id);
            if (colleague_Message == null)
            {
                return NotFound();
            }

            _context.Colleague_Message.Remove(colleague_Message);
            await _context.SaveChangesAsync();

            return colleague_Message;
        }

        private bool Colleague_MessageExists(int id)
        {
            return _context.Colleague_Message.Any(e => e.ColleagueId == id);
        }
    }
}
