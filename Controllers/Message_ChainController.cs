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
    [Route("api/Message_Chain")]
    [ApiController]
    public class Message_ChainController : ControllerBase
    {
        private readonly SMDbContext _context;

        public Message_ChainController(SMDbContext context)
        {
            _context = context;
        }

        // GET: api/Message_Chain
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Message_Chain>>> GetMessage_Chain()
        {
            return await _context.Message_Chain.ToListAsync();
        }

        // GET: api/Message_Chain/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Message_Chain>> GetMessage_Chain(int id)
        {
            var message_Chain = await _context.Message_Chain.FindAsync(id);

            if (message_Chain == null)
            {
                return NotFound();
            }

            return message_Chain;
        }

        // PUT: api/Message_Chain/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMessage_Chain(int id, Message_Chain message_Chain)
        {
            if (id != message_Chain.MessageChainId)
            {
                return BadRequest();
            }

            _context.Entry(message_Chain).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Message_ChainExists(id))
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

        // POST: api/Message_Chain
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Message_Chain>> PostMessage_Chain(Message_Chain message_Chain)
        {
            _context.Message_Chain.Add(message_Chain);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                if (Message_ChainExists(message_Chain.MessageChainId))
                {
                    return Conflict();
                }
                else
                {
                    throw e;
                }
            }

            //return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
            //  return CreatedAtAction("GetMessage_Chain", new { id = message_Chain.MessageChainId }, message_Chain);
            return CreatedAtAction(nameof(GetMessage_Chain), new { id = message_Chain.MessageChainId }, message_Chain);
        }

        // DELETE: api/Message_Chain/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Message_Chain>> DeleteMessage_Chain(int id)
        {
            var message_Chain = await _context.Message_Chain.FindAsync(id);
            if (message_Chain == null)
            {
                return NotFound();
            }

            _context.Message_Chain.Remove(message_Chain);
            await _context.SaveChangesAsync();

            return message_Chain;
        }

        private bool Message_ChainExists(int id)
        {
            return _context.Message_Chain.Any(e => e.MessageChainId == id);
        }
    }
}
