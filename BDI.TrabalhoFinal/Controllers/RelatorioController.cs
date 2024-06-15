using Microsoft.AspNetCore.Mvc;

using BDI.TrabalhoFinal.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

            return View("RelatorioViagem", viagens);
        }
        public IActionResult Faturamento()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Faturamento(int ano, int mes)
        {
            var faturamentos = _context.Viagens
                .Where(f => f.DataHoraInicio.Year == ano && f.DataHoraInicio.Month == mes)
                .OrderByDescending(f => f.ValorPagar)
                .Take(20)
                .Select(f => new
                {
                    Data = f.DataHoraInicio,
                    Valor = f.ValorPagar,
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
            var faturamentos = _context.Viagens
                .Where(v => v.DataHoraInicio.Year == ano && v.DataHoraInicio.Month == mes)
                .Join(
                    _context.Veiculos,
                    v => v.VeiculoId,
                    veiculo => veiculo.Id,
                    (v, veiculo) => new { Viagem = v, Veiculo = veiculo }
                )
                .Join(
                    _context.Proprietarios,
                    veiculoViagem => veiculoViagem.Veiculo.ProprietarioId,
                    proprietario => proprietario.Id,
                    (veiculoViagem, proprietario) => new { veiculoViagem.Viagem, veiculoViagem.Veiculo, Proprietario = proprietario }
                )
                .AsEnumerable()
                .GroupBy(v => new {
                    v.Proprietario,
                    v.Veiculo.Placa,
                    v.Viagem.FormaPagamento
                })
                .Select(g => new FaturamentoPorVeiculoViewModel
                {
                    Proprietario = g.Key.Proprietario,
                    Placa = g.Key.Placa,
                    FormaPagamento = g.Key.FormaPagamento,
                    Viagens = g.Select(v => new ViagemInfo
                    {
                        Viagem = v.Viagem,
                        Veiculo = v.Veiculo,
                        Proprietario = v.Proprietario
                    }).ToList(),
                    ValorTotalFaturado = (decimal)g.Sum(v => v.Viagem.ValorPagar),
                    ValorMedioFaturamento = (decimal)(g.Sum(v => v.Viagem.ValorPagar) / g.Count())
                })
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

    public class FaturamentoPorVeiculoViewModel
    {
        public Proprietario Proprietario { get; set; }
        public string Placa { get; set; }
        public string FormaPagamento { get; set; }
        public List<ViagemInfo> Viagens { get; set; }
        public decimal ValorTotalFaturado { get; set; }
        public decimal ValorMedioFaturamento { get; set; }
    }

    public class ViagemInfo
    {
        public Viagem Viagem { get; set; }
        public Veiculo Veiculo { get; set; }
        public Proprietario Proprietario { get; set; }
    }

}
