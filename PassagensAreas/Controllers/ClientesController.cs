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
    public class ClientesController : ControllerBase
    {
        private readonly ClienteContext _context;

        public ClientesController(ClienteContext context)
        {
            _context = context;
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClienteSet()
        {
            return await _context.ClienteSet.ToListAsync();
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(long id)
        {
            var cliente = await _context.ClienteSet.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        // PUT: api/Clientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(long id, Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return BadRequest();
            }

            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
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

        [HttpPut("{cpf}")]
        public async Task<IActionResult> PutCliente(string cpf, Cliente cliente)
        {
            // Verifica se o cliente informado é o mesmo do CPF passado na URL
            if (cpf != cliente.CPF)
            {
                return BadRequest("O CPF informado não corresponde ao cliente.");
            }

            // Busca o cliente no banco de dados
            var clienteExistente = await _context.ClienteSet.FirstOrDefaultAsync(c => c.CPF == cpf);

            // Se o cliente já existir, atualiza os dados
            if (clienteExistente != null)
            {
                // Atualiza os dados do cliente existente
                clienteExistente.Nome = cliente.Nome;
                clienteExistente.Endereco = cliente.Endereco;
                clienteExistente.Numero_telefone = cliente.Numero_telefone;
                clienteExistente.Numero_telefone_fixo = cliente.Numero_telefone_fixo;
                clienteExistente.Email = cliente.Email;

                _context.Entry(clienteExistente).State = EntityState.Modified;
            }
            else
            {
                // Se o cliente não existir, cadastra um novo
                await _context.ClienteSet.AddAsync(cliente);
            }

            try
            {
                // Salva as mudanças no banco de dados
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(cpf))
                {
                    return NotFound("Cliente não encontrado.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // Método para verificar se o cliente existe
        private bool ClienteExists(string cpf)
        {
            return _context.ClienteSet.Any(e => e.CPF == cpf);
        }

        // POST: api/Clientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            _context.ClienteSet.Add(cliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCliente", new { id = cliente.Id }, cliente);
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(long id)
        {
            var cliente = await _context.ClienteSet.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.ClienteSet.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClienteExists(long id)
        {
            return _context.ClienteSet.Any(e => e.Id == id);
        }
    }
}
