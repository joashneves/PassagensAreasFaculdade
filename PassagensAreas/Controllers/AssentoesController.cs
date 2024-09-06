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
        private bool AssentoExists(int id)
        {
            return _context.AssentoSet.Any(e => e.Id == id);
        }
    }
}
