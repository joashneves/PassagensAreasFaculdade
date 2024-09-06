using Infraestrutura;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PassagensAreas.Domain.Models;
using PassagensAreas.Infraestrutura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassagensAreas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelatoriosController : ControllerBase
    {
        private readonly VooContext _context;
        private readonly ReservaDePassagemContext _contextReserva;
        private readonly VendaContext _contextVenda;

        public RelatoriosController(VooContext context, ReservaDePassagemContext contextReserva, VendaContext contextVenda)
        {
            _context = context;
            _contextReserva = contextReserva;
            _contextVenda = contextVenda;
        }

        // Relatório de Ocupação Semanal
        [HttpGet("ocupacao-semanal")]
        public async Task<ActionResult<IEnumerable<RelatorioOcupacao>>> GerarRelatorioOcupacaoSemanal()
        {
            var dataInicial = DateTime.Now.AddDays(-7);
            var dataFinal = DateTime.Now;

            var voos = await _context.VooSet
                .Where(v => v.Ida >= dataInicial && v.Ida <= dataFinal)
                .ToListAsync();

            if (voos.Count == 0)
            {
                return NotFound("Nenhum voo encontrado na última semana.");
            }

            var relatorios = new List<RelatorioOcupacao>();

            foreach (var voo in voos)
            {
                var reservas = await _contextReserva.ReservaDePassagemSet
                    .Where(r => r.Id_voo == voo.Id)
                    .ToListAsync();

                int assentosOcupados = reservas.Sum(r => r.AssentosReservados);
                int totalAssentos = voo.QuantidadeMaximaPassageiros;

                float percentualOcupacao = (totalAssentos > 0) ? (float)assentosOcupados / totalAssentos * 100 : 0;

                var relatorio = new RelatorioOcupacao
                {
                    NumeroVoo = voo.NumeroVoo,
                    Origem = voo.Origem,
                    Destino = voo.Destino,
                    TotalAssentos = totalAssentos,
                    AssentosOcupados = assentosOcupados,
                    PercentualOcupacao = percentualOcupacao,
                    DataVoo = voo.Ida
                };

                relatorios.Add(relatorio);
            }

            return Ok(relatorios);
        }

        [HttpGet("vendas-mensal")]
        public async Task<ActionResult<IEnumerable<RelatorioVendas>>> GerarRelatorioVendasMensal()
        {
            var mesAtual = DateTime.Now.Month;
            var anoAtual = DateTime.Now.Year;

            var vendasMesAtual = await _contextVenda.VendaSet
                .Where(v => v.DataVenda.Month == mesAtual && v.DataVenda.Year == anoAtual)
                .ToListAsync();

            var vendasMesAnterior = await _contextVenda.VendaSet
                .Where(v => v.DataVenda.Month == mesAtual - 1 && v.DataVenda.Year == anoAtual)
                .ToListAsync();

            if (vendasMesAtual.Count == 0)
            {
                return NotFound("Nenhuma venda encontrada no mês atual.");
            }

            var relatorios = new List<RelatorioVendas>();

            return Ok(relatorios);
        }



    }
}
