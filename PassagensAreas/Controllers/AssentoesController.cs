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
    public class AssentoesController : ControllerBase
    {
        private readonly AssentoContext _context;

        public AssentoesController(AssentoContext context)
        {
            _context = context;
        }

        // GET: api/Assentoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Assento>>> GetAssentoSet()
        {
            return await _context.AssentoSet.ToListAsync();
        }

        // GET: api/Assentoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Assento>> GetAssento(int id)
        {
            var assento = await _context.AssentoSet.FindAsync(id);

            if (assento == null)
            {
                return NotFound();
            }

            return assento;
        }

        // PUT: api/Assentoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAssento(int id, Assento assento)
        {
            if (id != assento.Id)
            {
                return BadRequest();
            }

            _context.Entry(assento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssentoExists(id))
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

        // POST: api/Assentoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Assento>> PostAssento(Assento assento)
        {
            _context.AssentoSet.Add(assento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAssento", new { id = assento.Id }, assento);
        }

        // DELETE: api/Assentoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssento(int id)
        {
            var assento = await _context.AssentoSet.FindAsync(id);
            if (assento == null)
            {
                return NotFound();
            }

            _context.AssentoSet.Remove(assento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AssentoExists(int id)
        {
            return _context.AssentoSet.Any(e => e.Id == id);
        }
    }
}
