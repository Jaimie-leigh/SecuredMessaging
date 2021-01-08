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
    [Route("api/Message_Subject")]
    [ApiController]
    public class Message_SubjectController : ControllerBase
    {
        private readonly SMDbContext _context;

        public Message_SubjectController(SMDbContext context)
        {
            _context = context;
        }

        // GET: api/Message_Subject
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Message_Subject>>> GetMessage_Subject()
        {
            return await _context.Message_Subject.ToListAsync();
        }

        // GET: api/Message_Subject/5
        //[HttpGet("{rollNumber}")]
        //public async Task<ActionResult<Message_Subject>> GetMessage_Subject(int rollNumber)
        //{
        //    var message_Subject = await _context.Message_Subject.Where(a => a.RollNumber == rollNumber).ToListAsync();
        //    var subjectIds {
        //        foreach (Message_Subject a in message_Subject)
        //        {
        //            subjectIds.Add(a.MessageSubjectId);
        //        }
        //    }

        //    var noDupesSubjectIds = subjectIds.Distinct().ToList();

        //    foreach (int id in noDupesSubjectIds)
        //    {
        //        var chains = await _context.Message_Chain.Where(a => a.MessageSubjectId == id).ToListAsync();
        //    }

        //    if (message_Subject == null)
        //    {
        //        return NotFound();
        //    }

        //    Message_Subject allappMessages = message_Subject;
        //    allAppAndMessage.Message_Subject = messSubject;

        //    return message_Subject;
        //}

        // PUT: api/Message_Subject/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMessage_Subject(int id, Message_Subject message_Subject)
        {
            if (id != message_Subject.MessageSubjectId)
            {
                return BadRequest();
            }

            _context.Entry(message_Subject).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Message_SubjectExists(id))
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

        // POST: api/Message_Subject
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Message_Subject>> PostMessage_Subject(Message_Subject message_Subject)
        {
            _context.Message_Subject.Add(message_Subject);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (Message_SubjectExists(message_Subject.MessageSubjectId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMessage_Subject", new { id = message_Subject.MessageSubjectId }, message_Subject);
        }

        // DELETE: api/Message_Subject/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Message_Subject>> DeleteMessage_Subject(int id)
        {
            var message_Subject = await _context.Message_Subject.FindAsync(id);
            if (message_Subject == null)
            {
                return NotFound();
            }

            _context.Message_Subject.Remove(message_Subject);
            await _context.SaveChangesAsync();

            return message_Subject;
        }

        private bool Message_SubjectExists(int id)
        {
            return _context.Message_Subject.Any(e => e.MessageSubjectId == id);
        }
    }
}
