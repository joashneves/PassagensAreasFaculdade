using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infraestrutura;
using PassagensAreas.Domain.Models;
using PassagensAreas.Domain.DTOs;

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
        public async Task<ActionResult<IEnumerable<ClienteDTO>>> GetClienteSet()
        {
            var clientes = await _context.ClienteSet
        .Select(cliente => new ClienteDTO
        {
            Nome = cliente.Nome,
            CPF = cliente.CPF,
            Endereco = cliente.Endereco,
            Numero_telefone = cliente.Numero_telefone,
            Numero_telefone_fixo = cliente.Numero_telefone_fixo,
            Email = cliente.Email
        })
        .ToListAsync();

            return Ok(clientes);
        }
        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteDTO>> GetCliente(int id)
        {
            var cliente = await _context.ClienteSet.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            // Mapeando a entidade Cliente para o DTO
            var clienteDTO = new ClienteDTO
            {
                Nome = cliente.Nome,
                CPF = cliente.CPF,
                Endereco = cliente.Endereco,
                Numero_telefone = cliente.Numero_telefone,
                Numero_telefone_fixo = cliente.Numero_telefone_fixo,
                Email = cliente.Email
            };

            return Ok(clienteDTO);
        }
        [HttpPut("{cpf}")]
        public async Task<IActionResult> PutCliente(string cpf, ClienteDTO clienteDTO)
        {
            // Verifica se o CPF informado é o mesmo do DTO
            if (cpf != clienteDTO.CPF)
            {
                return BadRequest("O CPF informado não corresponde ao cliente.");
            }

            // Busca o cliente no banco de dados
            var clienteExistente = await _context.ClienteSet.FirstOrDefaultAsync(c => c.CPF == cpf);

            // Se o cliente já existir, atualiza os dados
            if (clienteExistente != null)
            {
                // Atualiza os dados do cliente existente com os dados do DTO
                clienteExistente.Nome = clienteDTO.Nome;
                clienteExistente.Endereco = clienteDTO.Endereco;
                clienteExistente.Numero_telefone = clienteDTO.Numero_telefone;
                clienteExistente.Numero_telefone_fixo = clienteDTO.Numero_telefone_fixo;
                clienteExistente.Email = clienteDTO.Email;

                _context.Entry(clienteExistente).State = EntityState.Modified;
            }
            else
            {
                // Se o cliente não existir, cadastra um novo usando os dados do DTO
                var novoCliente = new Cliente
                {
                    Nome = clienteDTO.Nome,
                    CPF = clienteDTO.CPF,
                    Endereco = clienteDTO.Endereco,
                    Numero_telefone = clienteDTO.Numero_telefone,
                    Numero_telefone_fixo = clienteDTO.Numero_telefone_fixo,
                    Email = clienteDTO.Email
                };

                await _context.ClienteSet.AddAsync(novoCliente);
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


    }
}
