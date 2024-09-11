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
using PassagensAreas.Domain.DTOs;

namespace PassagensAreas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VooClientesController : ControllerBase
    {
        private readonly VooContext _context;
        private readonly ReservaDePassagemContext _contextReserva;
        private readonly CheckInContext _contextCheckIn;
        private readonly ClienteContext _contextCliente;
        private readonly RelatorioOcupacaoContext _contextOcupacao;

        public VooClientesController(VooContext context, ReservaDePassagemContext contextReserva, CheckInContext contextChekin, ClienteContext contextCliente, RelatorioOcupacaoContext contextOcupacao)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _contextReserva = contextReserva ?? throw new ArgumentNullException(nameof(contextReserva));
            _contextCheckIn = contextChekin ?? throw new ArgumentNullException(nameof(contextChekin));
            _contextCliente = contextCliente ?? throw new ArgumentNullException(nameof(contextCliente));
            _contextOcupacao = contextOcupacao ?? throw new ArgumentNullException(nameof(contextOcupacao));
        }
        [HttpGet("consulta")]
        public async Task<ActionResult<IEnumerable<VooClienteDTO>>> ConsultarVoos(
            [FromQuery] string origem,
            [FromQuery] string destino,
            [FromQuery] DateTime? ida,
            [FromQuery] DateTime? volta)
        {
            try
            {
                // Valida se origem e destino foram informados
                if (string.IsNullOrEmpty(origem) || string.IsNullOrEmpty(destino))
                {
                    return BadRequest("Origem e destino são obrigatórios.");
                }

                // Consulta os voos disponíveis com base nos parâmetros
                var voos = await _context.VooSet
                    .Where(v => v.Origem == origem && v.Destino == destino)
                    .Where(v => ida == null || v.Ida == ida)
                    .Where(v => volta == null || v.Volta == volta)
                    .ToListAsync();

                if (!voos.Any())
                {
                    return NotFound("Nenhum voo encontrado para os critérios informados.");
                }

                // Mapeia os dados de Voo para VooClienteDTO
                var voosDTO = voos.Select(v => new VooClienteDTO
                {
                    Origem = v.Origem,
                    Destino = v.Destino,
                    Ida = v.Ida,
                    Volta = v.Volta,
                    Preco = v.Preco,
                    Companhias = v.Companhias,
                    QuantidadeMaximaPassageiros = v.QuantidadeMaximaPassageiros,
                    QuantidadePassageiros = v.QuantidadePassageiros,
                    NumeroVoo = v.NumeroVoo
                }).ToList();

                return Ok(voosDTO);
            }
            catch (Exception ex)
            {
                return BadRequest("Ocorreu um erro");
            }
        }


        [HttpPost("reservar")]
        public async Task<IActionResult> ReservarPassagem(
    [FromBody] ReservaDePassagemDTO reservaDTO,
    [FromQuery] int quantidadePassageiros,
    [FromQuery] string formaPagamento)
        {
            try
            {
                // Verifica se o voo existe pelo número do voo
                var voo = await _context.VooSet.FirstOrDefaultAsync(v => v.NumeroVoo == reservaDTO.NumeroVoo);

                if (voo == null)
                {
                    return NotFound("Voo não encontrado.");
                }

                // Verifica se o CPF informado já existe no banco de dados
                var clienteExistente = await _contextCliente.ClienteSet
                    .FirstOrDefaultAsync(c => c.CPF == reservaDTO.CPFCliente);

                if (clienteExistente == null)
                {
                    return NotFound("CPF não encontrado. Por favor, registre-se antes de realizar a reserva.");
                }

                // Verifica se há assentos disponíveis
                int assentosDisponiveis = voo.QuantidadeMaximaPassageiros - voo.QuantidadePassageiros;
                if (assentosDisponiveis < quantidadePassageiros)
                {
                    return BadRequest("Não há assentos disponíveis para a quantidade de passageiros informada.");
                }

                // Atualiza a quantidade de passageiros no voo
                voo.QuantidadePassageiros += quantidadePassageiros;

                // Define o valor fixo por passagem (exemplo: R$100,00)
                decimal valorPorPassagem = 100.00m;
                decimal totalArrecadado = valorPorPassagem * quantidadePassageiros;

                var novaReserva = new ReservaDePassagem
                {
                    Id_Voo = voo.Id,
                    CPFCliente = reservaDTO.CPFCliente,
                    DataReserva = DateTime.Now,
                    NumeroVoo = reservaDTO.NumeroVoo,
                    AssentosReservados = quantidadePassageiros,
                    FormaPagamento = formaPagamento,
                    ValorTotal = totalArrecadado
                };

                // Adiciona a reserva ao banco de dados
                _contextReserva.ReservaDePassagemSet.Add(novaReserva);

                // Atualiza o voo com a nova quantidade de passageiros
                _context.Entry(voo).State = EntityState.Modified;

                // Salva as mudanças
                await _context.SaveChangesAsync();
                await _contextReserva.SaveChangesAsync();

                // Retorna o DTO da reserva criada
                var reservaCriada = new ReservaDePassagemDTO
                {
                    CPFCliente = novaReserva.CPFCliente,
                    DataReserva = novaReserva.DataReserva,
                    NumeroVoo = novaReserva.NumeroVoo
                };

                return Ok(new { mensagem = "Reserva realizada com sucesso.", reserva = reservaCriada });
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro: {ex.Message}");
            }
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VooClienteDTO>>> GetVooSet()
        {
            var voos = await _context.VooSet
                .Select(v => new VooClienteDTO
                {
                    Origem = v.Origem,
                    Destino = v.Destino,
                    Ida = v.Ida,
                    Volta = v.Volta,
                    Preco = v.Preco,
                    Companhias = v.Companhias,
                    QuantidadeMaximaPassageiros = v.QuantidadeMaximaPassageiros,
                    QuantidadePassageiros = v.QuantidadePassageiros,
                    NumeroVoo = v.NumeroVoo
                })
                .ToListAsync();

            return Ok(voos);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<VooClienteDTO>> GetVooCliente(int id)
        {
            var voo = await _context.VooSet
                .Where(v => v.Id == id)
                .Select(v => new VooClienteDTO
                {
                    Origem = v.Origem,
                    Destino = v.Destino,
                    Ida = v.Ida,
                    Volta = v.Volta,
                    Preco = v.Preco,
                    Companhias = v.Companhias,
                    QuantidadeMaximaPassageiros = v.QuantidadeMaximaPassageiros,
                    QuantidadePassageiros = v.QuantidadePassageiros,
                    NumeroVoo = v.NumeroVoo
                })
                .FirstOrDefaultAsync();

            if (voo == null)
            {
                return NotFound();
            }

            return Ok(voo);
        }
        [HttpPost("checkin/{numeroVoo}")]
        public async Task<IActionResult> FazerCheckIn(int numeroVoo, [FromBody] CheckInDTO checkInDTO)
        {
            try
            {
                // Busca a reserva usando o numeroVoo
                var reserva = await _contextReserva.ReservaDePassagemSet
                    .FirstOrDefaultAsync(r => r.NumeroVoo == numeroVoo);

                if (reserva == null)
                {
                    return NotFound("Reserva não encontrada.");
                }

                // Verifica se o check-in está dentro do período permitido
                var voo = await _context.VooSet.FindAsync(reserva.Id_Voo);
                if (voo == null)
                {
                    return NotFound("Voo não encontrado.");
                }


                // Confirmação dos dados cadastrais do cliente
                    var cliente = await _contextCliente.ClienteSet
                .FirstOrDefaultAsync(c => c.CPF == reserva.CPFCliente);
                if (cliente == null)
                {
                    return NotFound("Cliente não encontrado.");
                }

                // Verifica a disponibilidade do assento
                if (!AssentoDisponivel(voo, checkInDTO.AssentoEscolhido))
                {
                    return BadRequest("O assento escolhido não está disponível.");
                }

                // Cria o registro de check-in
                var checkIn = new CheckIn
                {
                    Id_ReservaDePassagem = reserva.Id,
                    DataCheckIn = DateTime.Now,
                    AssentoEscolhido = checkInDTO.AssentoEscolhido,
                    CheckInRealizado = true
                };

                // Adiciona o check-in ao banco de dados
                _contextCheckIn.CheckInSet.Add(checkIn);

                // Registra mudança de ocupação
                var ocupacao = new RelatorioOcupacao
                {
                    VooId = voo.Id,
                    DataRelatorio = DateTime.Now,
                    PercentualOcupacao = (double)voo.QuantidadePassageiros / voo.QuantidadeMaximaPassageiros * 100
                };
                _contextOcupacao.Add(ocupacao);

                // Salva as mudanças
                await _contextReserva.SaveChangesAsync();
                await _context.SaveChangesAsync();
                await _contextCheckIn.SaveChangesAsync();

                return Ok("Check-in realizado com sucesso.");
            }
            catch (Exception ex)
            {
                // Retorna o corpo da exceção em caso de erro
                return StatusCode(500, new { mensagem = "Ocorreu um erro interno.", detalhes = ex.Message });
            }
        }


        // Função auxiliar para verificar se o assento está disponível
        private bool AssentoDisponivel(VooCliente voo, string assentoEscolhido)
        {
            return true;
        }

        [HttpPost("emitir-bilhete/{idReserva}")]
        public async Task<IActionResult> EmitirBilhete(int idReserva)
        {
            // Busca a reserva
            var reserva = await _contextReserva.ReservaDePassagemSet.FindAsync(idReserva);

            if (reserva == null)
            {
                return NotFound("Reserva não encontrada.");
            }

            // Verifica se o check-in foi realizado
            var checkIn = await _contextCheckIn.CheckInSet.FirstOrDefaultAsync(c => c.Id_ReservaDePassagem == idReserva);
            if (checkIn == null || !checkIn.CheckInRealizado)
            {
                return BadRequest("Check-in não realizado. O bilhete só pode ser emitido após o check-in.");
            }

            // Busca as informações do cliente e voo
            var cliente = await _contextCliente.ClienteSet.FindAsync(reserva.CPFCliente);
            var voo = await _context.VooSet.FindAsync(reserva.Id_Voo);

            if (cliente == null || voo == null)
            {
                return NotFound("Cliente ou voo não encontrado.");
            }

            // Cria o bilhete
            var bilhete = new Bilhete
            {
                Id_ReservaDePassagem = idReserva,
                NomeCliente = cliente.Nome,
                NumeroVoo = voo.NumeroVoo.ToString(),
                Origem = voo.Origem,
                Destino = voo.Destino,
                DataVoo = voo.Ida,
                Assento = checkIn.AssentoEscolhido,
                DataEmissao = (DateTime.Now)
            };

            // Simulação de envio de bilhete por e-mail (com lógica de envio por e-mail)
            await EnviarBilhetePorEmail(cliente.Email, bilhete);

            // Retorna o bilhete gerado como resposta
            return Ok(bilhete);
        }

        // Função auxiliar para enviar o bilhete por e-mail
        private async Task EnviarBilhetePorEmail(string email, Bilhete bilhete)
        {
            var mensagem = $@"
        Bilhete Eletrônico
        -----------------
        Nome: {bilhete.NomeCliente}
        Número do Voo: {bilhete.NumeroVoo}
        Origem: {bilhete.Origem}
        Destino: {bilhete.Destino}
        Data do Voo: {bilhete.DataVoo}
        Assento: {bilhete.Assento}
        Data de Emissão: {bilhete.DataEmissao}
    ";

            Console.WriteLine($"Enviando bilhete para {email}:\n{mensagem}");
        }


        [HttpDelete("cancelar/{idReserva}")]
        public async Task<IActionResult> CancelarReserva(int idReserva)
        {
            // Busca a reserva
            var reserva = await _contextReserva.ReservaDePassagemSet.FindAsync(idReserva);

            if (reserva == null)
            {
                return NotFound("Reserva não encontrada.");
            }

            // Busca o voo relacionado à reserva
            var voo = await _context.VooSet.FindAsync(reserva.Id_Voo);

            if (voo == null)
            {
                return NotFound("Voo não encontrado.");
            }

            // Libera os assentos reservados
            voo.QuantidadePassageiros -= reserva.AssentosReservados;

            // Remove a reserva
            _contextReserva.ReservaDePassagemSet.Remove(reserva);

            // Atualiza o voo no banco de dados
            _context.Entry(voo).State = EntityState.Modified;

            // Registra mudança de ocupação
            var ocupacao = new RelatorioOcupacao
            {
                VooId = voo.Id,
                DataRelatorio = DateTime.Now,
                PercentualOcupacao = (double)voo.QuantidadePassageiros / voo.QuantidadeMaximaPassageiros * 100
            };
            _contextReserva.Add(ocupacao);

            // Salva as mudanças
            await _contextReserva.SaveChangesAsync();
            await _context.SaveChangesAsync();
            await _contextReserva.SaveChangesAsync();

            return Ok("Reserva cancelada com sucesso.");
        }
    }
}
