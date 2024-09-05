using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PassagensAreas.Domain.Models;
using PassagensAreas.Infraestrutura;

namespace PassagensAreas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BilhetesController : ControllerBase
    {
        private readonly BilheteContext _context;

        public BilhetesController(BilheteContext context)
        {
            _context = context;
        }

        // GET: api/Bilhetes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bilhete>>> GetBilheteSet()
        {
            return await _context.BilheteSet.ToListAsync();
        }

        // GET: api/Bilhetes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bilhete>> GetBilhete(int id)
        {
            var bilhete = await _context.BilheteSet.FindAsync(id);

            if (bilhete == null)
            {
                return NotFound();
            }

            return bilhete;
        }

        // PUT: api/Bilhetes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBilhete(int id, Bilhete bilhete)
        {
            if (id != bilhete.Id)
            {
                return BadRequest();
            }

            _context.Entry(bilhete).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BilheteExists(id))
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

        // POST: api/Bilhetes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Bilhete>> PostBilhete(Bilhete bilhete)
        {
            _context.BilheteSet.Add(bilhete);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBilhete", new { id = bilhete.Id }, bilhete);
        }

        // DELETE: api/Bilhetes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBilhete(int id)
        {
            var bilhete = await _context.BilheteSet.FindAsync(id);
            if (bilhete == null)
            {
                return NotFound();
            }

            _context.BilheteSet.Remove(bilhete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BilheteExists(int id)
        {
            return _context.BilheteSet.Any(e => e.Id == id);
        }
    }
}
