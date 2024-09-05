using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infraestrutura;
using PassagensAreas.Domain.Models;

namespace PassagensAreas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VooClientesController : ControllerBase
    {
        private readonly VooContext _context;

        public VooClientesController(VooContext context)
        {
            _context = context;
        }

        // GET: api/VooClientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VooCliente>>> GetVooSet()
        {
            return await _context.VooSet.ToListAsync();
        }

        // GET: api/VooClientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VooCliente>> GetVooCliente(int id)
        {
            var vooCliente = await _context.VooSet.FindAsync(id);

            if (vooCliente == null)
            {
                return NotFound();
            }

            return vooCliente;
        }

        // PUT: api/VooClientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVooCliente(int id, VooCliente vooCliente)
        {
            if (id != vooCliente.Id)
            {
                return BadRequest();
            }

            _context.Entry(vooCliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VooClienteExists(id))
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

        // POST: api/VooClientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VooCliente>> PostVooCliente(VooCliente vooCliente)
        {
            _context.VooSet.Add(vooCliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVooCliente", new { id = vooCliente.Id }, vooCliente);
        }

        // DELETE: api/VooClientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVooCliente(int id)
        {
            var vooCliente = await _context.VooSet.FindAsync(id);
            if (vooCliente == null)
            {
                return NotFound();
            }

            _context.VooSet.Remove(vooCliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VooClienteExists(int id)
        {
            return _context.VooSet.Any(e => e.Id == id);
        }
    }
}
