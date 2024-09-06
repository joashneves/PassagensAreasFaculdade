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

    }
}
