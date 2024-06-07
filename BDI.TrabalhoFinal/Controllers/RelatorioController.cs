using Microsoft.AspNetCore.Mvc;

using System;
using System.Linq;
using BDI.TrabalhoFinal.Data;
using BDI.TrabalhoFinal.Models;

namespace BDI.TrabalhoFinal.Controllers
{

    public class RelatorioController : Controller
    {
        private readonly BancoDeDados _context;

        public RelatorioController(BancoDeDados context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string marca, DateTime dataInicial, DateTime dataFinal, TimeSpan horaInicial, TimeSpan horaFinal)
        {
            var viagens = _context.Veiculos
                .Where(v => v.Marca == marca)
                .SelectMany(v => v.Viagens)
                .Where(v => v.DataHoraInicio >= dataInicial.Date + horaInicial
                         && v.DataHoraFim <= dataFinal.Date + horaFinal)
                .Select(v => new
                {
                    Marca = v.Veiculo.Marca,
                    Placa = v.Veiculo.Placa,
                    LocalOrigem = v.LocalOrigem,
                    LocalDestino = v.LocalDestino,
                    NomeMotorista = v.Motorista.Nome,
                    NomePassageiro = v.Passageiro.Nome
                }).ToList();

            return View("Relatorio", viagens);
        }
        public IActionResult Faturamento()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Faturamento(int ano, int mes)
        {
            var faturamentos = _context.Faturamentos
                .Where(f => f.Data.Year == ano && f.Data.Month == mes)
                .OrderByDescending(f => f.Valor)
                .Take(20)
                .Select(f => new
                {
                    Data = f.Data,
                    Valor = f.Valor,
                    Marca = f.Veiculo.Marca,
                    Placa = f.Veiculo.Placa
                }).ToList();

            return View("RelatorioFaturamento", faturamentos);
        }

        public IActionResult FaturamentoPorVeiculo()
        {
            return View();
        }

        [HttpPost]
        public IActionResult FaturamentoPorVeiculo(int ano, int mes)
        {
            var faturamentos = _context.Faturamentos
                .Where(f => f.Data.Year == ano && f.Data.Month == mes)
                .GroupBy(f => new { f.Veiculo.Proprietario, f.Veiculo.Placa, f.TipoPagamento })
                .Select(g => new
                {
                    NomeProprietario = g.Key.Proprietario,
                    PlacaVeiculo = g.Key.Placa,
                    TipoPagamento = g.Key.TipoPagamento,
                    ValorTotalFaturado = g.Sum(f => f.Valor),
                    ValorMedioFaturamento = g.Average(f => f.Valor)
                })
                .OrderBy(f => f.NomeProprietario)
                .ThenBy(f => f.TipoPagamento)
                .ThenBy(f => f.PlacaVeiculo)
                .ToList();

            return View("RelatorioFaturamentoPorVeiculo", faturamentos);
        }

        public IActionResult MediaMensalViagensPorSexo()
        {
            return View();
        }

        [HttpPost]
        public IActionResult MediaMensalViagensPorSexo(int ano, int mes)
        {
            var viagensPorSexo = _context.Viagens
                .GroupBy(v => new { v.DataHoraInicio.Year, v.DataHoraInicio.Month, v.Passageiro.Sexo })
                .Select(g => new
                {
                    Ano = g.Key.Year,
                    Mes = g.Key.Month,
                    Sexo = g.Key.Sexo,
                    MediaViagens = g.Count() / (decimal)g.Select(v => v.DataHoraInicio.Month).Distinct().Count()
                })
                .OrderBy(v => v.Ano)
                .ThenBy(v => v.Mes)
                .ThenBy(v => v.Sexo)
                .ToList();

            return View("RelatorioMediaMensalViagensPorSexo", viagensPorSexo);
        }
    }
}
