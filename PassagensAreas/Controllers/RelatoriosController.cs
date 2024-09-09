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
