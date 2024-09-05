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
    public class ReservaDePassagemsController : ControllerBase
    {
        private readonly ReservaDePassagemContext _context;

        public ReservaDePassagemsController(ReservaDePassagemContext context)
        {
            _context = context;
        }

        // GET: api/ReservaDePassagems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservaDePassagem>>> GetReservaDePassagemSet()
        {
            return await _context.ReservaDePassagemSet.ToListAsync();
        }

        // GET: api/ReservaDePassagems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservaDePassagem>> GetReservaDePassagem(int id)
        {
            var reservaDePassagem = await _context.ReservaDePassagemSet.FindAsync(id);

            if (reservaDePassagem == null)
            {
                return NotFound();
            }

            return reservaDePassagem;
        }

        // PUT: api/ReservaDePassagems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservaDePassagem(int id, ReservaDePassagem reservaDePassagem)
        {
            if (id != reservaDePassagem.Id)
            {
                return BadRequest();
            }

            _context.Entry(reservaDePassagem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservaDePassagemExists(id))
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

        // POST: api/ReservaDePassagems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ReservaDePassagem>> PostReservaDePassagem(ReservaDePassagem reservaDePassagem)
        {
            _context.ReservaDePassagemSet.Add(reservaDePassagem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReservaDePassagem", new { id = reservaDePassagem.Id }, reservaDePassagem);
        }

        // DELETE: api/ReservaDePassagems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservaDePassagem(int id)
        {
            var reservaDePassagem = await _context.ReservaDePassagemSet.FindAsync(id);
            if (reservaDePassagem == null)
            {
                return NotFound();
            }

            _context.ReservaDePassagemSet.Remove(reservaDePassagem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReservaDePassagemExists(int id)
        {
            return _context.ReservaDePassagemSet.Any(e => e.Id == id);
        }
    }
}
