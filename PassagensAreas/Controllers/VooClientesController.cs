using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infraestrutura;
using PassagensAreas.Domain.Models;
using PassagensAreas.Infraestrutura;

namespace PassagensAreas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VooClientesController : ControllerBase
    {
        private readonly VooContext _context;
        private readonly ReservaDePassagemContext _contextReserva;

        public VooClientesController(VooContext context)
        {
            _context = context;
        }
        [HttpGet("consulta")]
        public async Task<ActionResult<IEnumerable<VooCliente>>> ConsultarVoos(
            [FromQuery] string origem,
            [FromQuery] string destino,
            [FromQuery] DateOnly? ida,
            [FromQuery] DateOnly? volta)
        {
            // Valida se origem e destino foram informados
            if (string.IsNullOrEmpty(origem) || string.IsNullOrEmpty(destino))
            {
                return BadRequest("Origem e destino são obrigatórios.");
            }

            // Consulta os voos disponíveis com base nos parâmetros
            var voos = await _context.VooSet
                .Where(v => v.Origem == origem && v.Destino == destino)
                .Where(v => ida == null || v.Ida == ida)    // Se a data de ida for informada, filtra pela data
                .Where(v => volta == null || v.Volta == volta)  // Se a data de volta for informada, filtra pela data
                .ToListAsync();

            if (!voos.Any())
            {
                return NotFound("Nenhum voo encontrado para os critérios informados.");
            }

            return Ok(voos);
        }

        [HttpPost("reservar")]
        public async Task<IActionResult> ReservarPassagem(
        [FromBody] ReservaDePassagem reserva,
        [FromQuery] int quantidadePassageiros)
        {
            // Verifica se o voo existe
            var voo = await _context.VooSet.FirstOrDefaultAsync(v => v.Id == reserva.Id_voo);

            if (voo == null)
            {
                return NotFound("Voo não encontrado.");
            }

            // Verifica se há assentos disponíveis
            int assentosDisponiveis = voo.QuantidadeMaximaPassageiros - voo.QuantidadePassageiros;
            if (assentosDisponiveis < quantidadePassageiros)
            {
                return BadRequest("Não há assentos disponíveis para a quantidade de passageiros informada.");
            }

            // Atualiza a quantidade de passageiros no voo
            voo.QuantidadePassageiros += quantidadePassageiros;

            // Cria a reserva de passagem
            var novaReserva = new ReservaDePassagem
            {
                Id_voo = voo.Id,
                CPFCliente = reserva.CPFCliente,  // Assume-se que o CPF foi passado no body
                DataReserva = DateOnly.FromDateTime(DateTime.Now),
                NumeroVoo = voo.NumeroVoo,
                AssentosReservados = quantidadePassageiros
            };

            // Adiciona a reserva ao banco de dados
            _contextReserva.ReservaDePassagemSet.Add(novaReserva);

            // Atualiza o voo com a nova quantidade de passageiros
            _context.Entry(voo).State = EntityState.Modified;

            // Salva as mudanças
            await _context.SaveChangesAsync();

            return Ok("Reserva realizada com sucesso.");
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
