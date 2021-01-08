using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMWebAPI.Models;

namespace SMWebAPI.Controllers
{
    [Route("api/Broker")]
    [ApiController]
    public class BrokersController : ControllerBase
    {
        private readonly SMDbContext _context;

        public BrokersController(SMDbContext context)
        {
            _context = context;
        }

        // GET: api/Brokers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Broker>>> GetBroker()
        {
            return await _context.Broker.ToListAsync();
        }

        // GET: api/Brokers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Broker>> GetBroker(int id)
        {
            var broker = await _context.Broker.FindAsync(id);

            var apps = await _context.Application.Where(a => a.BrokerId == id).ToListAsync();

            Broker fullbroker = broker;
            fullbroker.Application = apps;
            if (broker == null)
            {
                return NotFound();
            }

            return broker;
        }

        // PUT: api/Brokers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBroker(int id, Broker broker)
        {
            if (id != broker.BrokerId)
            {
                return BadRequest();
            }

            _context.Entry(broker).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrokerExists(id))
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

        // POST: api/Brokers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Broker>> PostBroker(Broker broker)
        {
            _context.Broker.Add(broker);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BrokerExists(broker.BrokerId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBroker", new { id = broker.BrokerId }, broker);
        }

        // DELETE: api/Brokers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Broker>> DeleteBroker(int id)
        {
            var broker = await _context.Broker.FindAsync(id);
            if (broker == null)
            {
                return NotFound();
            }

            _context.Broker.Remove(broker);
            await _context.SaveChangesAsync();

            return broker;
        }

        private bool BrokerExists(int id)
        {
            return _context.Broker.Any(e => e.BrokerId == id);
        }
    }
}
