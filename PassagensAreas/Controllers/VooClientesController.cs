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
        private readonly CheckInContext _contextCheckIn;
        private readonly ClienteContext _contextCliente;

        public VooClientesController(VooContext context, ReservaDePassagemContext contextReserva, CheckInContext contextChekin, ClienteContext contextCliente)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _contextReserva = contextReserva ?? throw new ArgumentNullException(nameof(contextReserva));
            _contextCheckIn = contextChekin ?? throw new ArgumentNullException(nameof(contextChekin));
            _contextCliente = contextCliente ?? throw new ArgumentNullException(nameof(contextCliente));
        }
        [HttpGet("consulta")]
        public async Task<ActionResult<IEnumerable<VooCliente>>> ConsultarVoos(
            [FromQuery] string origem,
            [FromQuery] string destino,
            [FromQuery] DateTime? ida,
            [FromQuery] DateTime? volta)
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
                DataReserva = (DateTime.Now),
                NumeroVoo = voo.NumeroVoo,
                AssentosReservados = quantidadePassageiros
            };
            Console.WriteLine(novaReserva.ToString());

            // Adiciona a reserva ao banco de dados
            _contextReserva.ReservaDePassagemSet.Add(novaReserva);

            // Atualiza o voo com a nova quantidade de passageiros
            _context.Entry(voo).State = EntityState.Modified;

            // Salva as mudanças
            await _context.SaveChangesAsync();
            await _contextReserva.SaveChangesAsync();

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

        [HttpPost("checkin/{idReserva}")]
        public async Task<IActionResult> FazerCheckIn(int idReserva, [FromBody] string assentoEscolhido)
        {
            // Busca a reserva
            var reserva = await _contextReserva.ReservaDePassagemSet.FindAsync(idReserva);

            if (reserva == null)
            {
                return NotFound("Reserva não encontrada.");
            }

            // Verifica se o check-in está dentro do período permitido
            var voo = await _context.VooSet.FindAsync(reserva.Id_voo);
            if (voo == null)
            {
                return NotFound("Voo não encontrado.");
            }

            var horasParaDecolagem = voo.Ida.Subtract(DateTime.Now).TotalHours;
            if (horasParaDecolagem > 24 || horasParaDecolagem < 1)
            {
                return BadRequest("O check-in só pode ser realizado entre 24 horas e 1 hora antes da decolagem.");
            }

            // Confirmação dos dados cadastrais do cliente
            var cliente = await _contextCliente.ClienteSet.FindAsync(reserva.CPFCliente);
            if (cliente == null)
            {
                return NotFound("Cliente não encontrado.");
            }

            // Verifica a disponibilidade do assento
            if (!AssentoDisponivel(voo, assentoEscolhido))
            {
                return BadRequest("O assento escolhido não está disponível.");
            }

            // Cria o registro de check-in
            var checkIn = new CheckIn
            {
                IdReserva = idReserva,
                DataCheckIn = (DateTime.Now),
                AssentoEscolhido = assentoEscolhido,
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
            _contextReserva.Add(ocupacao);

            // Salva as mudanças
            await _contextReserva.SaveChangesAsync();
            await _context.SaveChangesAsync();
            await _contextCheckIn.SaveChangesAsync();

            return Ok("Check-in realizado com sucesso.");
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
            var checkIn = await _contextCheckIn.CheckInSet.FirstOrDefaultAsync(c => c.IdReserva == idReserva);
            if (checkIn == null || !checkIn.CheckInRealizado)
            {
                return BadRequest("Check-in não realizado. O bilhete só pode ser emitido após o check-in.");
            }

            // Busca as informações do cliente e voo
            var cliente = await _contextCliente.ClienteSet.FindAsync(reserva.CPFCliente);
            var voo = await _context.VooSet.FindAsync(reserva.Id_voo);

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
            // Aqui você implementaria o envio real de e-mail.
            // Pode usar serviços como SendGrid, Amazon SES, ou SMTP.
            // Exemplo simplificado:
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
            var voo = await _context.VooSet.FindAsync(reserva.Id_voo);

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

        private bool VooClienteExists(int id)
        {
            return _context.VooSet.Any(e => e.Id == id);
        }
    }
}
