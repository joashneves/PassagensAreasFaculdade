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
    }
}
