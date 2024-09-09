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

        [HttpGet("relatorio-vendas")]
        public async Task<ActionResult<IEnumerable<RelatorioVendas>>> GerarRelatorioVendas()
        {
            var vendas = await _contextVenda.VendaSet
                .GroupBy(v => new { v.NumeroVoo, v.FormaPagamento })
                .Select(g => new RelatorioVendas
                {
                    NumeroVoo = g.Key.NumeroVoo,
                    FormaPagamento = g.Key.FormaPagamento,
                    ValorTotal = g.Sum(v => v.ValorTotal)
                })
                .ToListAsync();

            return Ok(vendas);
        }

        // Endpoint para gerar relatório semanal de ocupação dos voos
        [HttpGet("ocupacao-semanal")]
        public async Task<ActionResult<IEnumerable<RelatorioOcupacao>>> GerarRelatorioOcupacaoSemanal()
        {
            var inicioDaSemana = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek + (int)DayOfWeek.Monday);
            var fimDaSemana = inicioDaSemana.AddDays(7);

            // Obtém todas as reservas na semana atual e agrupa por voo
            var voos = await _context.VooSet.ToListAsync();
            var reservas = await _contextReserva.ReservaDePassagemSet
                .Where(r => r.DataReserva >= inicioDaSemana && r.DataReserva < fimDaSemana)
                .GroupBy(r => r.Id_Voo)
                .Select(g => new
                {
                    VooId = g.Key,
                    TotalReservado = g.Sum(r => r.AssentosReservados)
                })
                .ToListAsync();
            // Cria o relatório de ocupação com base nas reservas e na quantidade máxima de passageiros
            var relatorios = voos.Select(v => new RelatorioOcupacao
            {
                VooId = v.Id,
                DataRelatorio = DateTime.Now,
                PercentualOcupacao = reservas.FirstOrDefault(r => r.VooId == v.Id)?.TotalReservado / (double)v.QuantidadeMaximaPassageiros * 100 ?? 0
            }).ToList();

            if (relatorios.Count == 0)
            {
                return NotFound("Nenhuma ocupação encontrada para a semana atual.");
            }

            return Ok(relatorios);
        }

    }
}
