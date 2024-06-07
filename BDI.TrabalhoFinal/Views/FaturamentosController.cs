using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BDI.TrabalhoFinal.Data;
using BDI.TrabalhoFinal.Models;

namespace BDI.TrabalhoFinal.Views
{
    public class FaturamentosController : Controller
    {
        private readonly BancoDeDados _context;

        public FaturamentosController(BancoDeDados context)
        {
            _context = context;
        }

        // GET: Faturamentos
        public async Task<IActionResult> Index()
        {
            var bancoDeDados = _context.Faturamentos.Include(f => f.Veiculo);
            return View(await bancoDeDados.ToListAsync());
        }

        // GET: Faturamentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faturamento = await _context.Faturamentos
                .Include(f => f.Veiculo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (faturamento == null)
            {
                return NotFound();
            }

            return View(faturamento);
        }

        // GET: Faturamentos/Create
        public IActionResult Create()
        {
            ViewData["VeiculoId"] = new SelectList(_context.Veiculos, "Id", "Cor");
            return View();
        }

        // POST: Faturamentos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Data,Valor,VeiculoId")] Faturamento faturamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(faturamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VeiculoId"] = new SelectList(_context.Veiculos, "Id", "Cor", faturamento.VeiculoId);
            return View(faturamento);
        }

        // GET: Faturamentos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faturamento = await _context.Faturamentos.FindAsync(id);
            if (faturamento == null)
            {
                return NotFound();
            }
            ViewData["VeiculoId"] = new SelectList(_context.Veiculos, "Id", "Cor", faturamento.VeiculoId);
            return View(faturamento);
        }

        // POST: Faturamentos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Data,Valor,VeiculoId")] Faturamento faturamento)
        {
            if (id != faturamento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(faturamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FaturamentoExists(faturamento.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["VeiculoId"] = new SelectList(_context.Veiculos, "Id", "Cor", faturamento.VeiculoId);
            return View(faturamento);
        }

        // GET: Faturamentos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faturamento = await _context.Faturamentos
                .Include(f => f.Veiculo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (faturamento == null)
            {
                return NotFound();
            }

            return View(faturamento);
        }

        // POST: Faturamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var faturamento = await _context.Faturamentos.FindAsync(id);
            if (faturamento != null)
            {
                _context.Faturamentos.Remove(faturamento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FaturamentoExists(int id)
        {
            return _context.Faturamentos.Any(e => e.Id == id);
        }
    }
}
