using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BDI.TrabalhoFinal.Data;
using BDI.TrabalhoFinal.Models;

namespace BDI.TrabalhoFinal.Controllers
{
    public class VeiculoController : Controller
    {
        private readonly BancoDeDados _context;

        public VeiculoController(BancoDeDados context)
        {
            _context = context;
        }

        // GET: Veiculoes
        public async Task<IActionResult> Index()
        {
            var bancoDeDados = _context.Veiculos.Include(v => v.Motorista).Include(v => v.Proprietario);
            return View(await bancoDeDados.ToListAsync());
        }

        // GET: Veiculoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veiculo = await _context.Veiculos
                .Include(v => v.Motorista)
                .Include(v => v.Proprietario)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (veiculo == null)
            {
                return NotFound();
            }

            return View(veiculo);
        }

        // GET: Veiculoes/Create
        public IActionResult Create()
        {
            ViewData["MotoristaId"] = new SelectList(_context.Motoristas, "Id", "Id");
            ViewData["ProprietarioId"] = new SelectList(_context.Proprietarios, "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Veiculo veiculo)
        {
            var motoristaVeiculo = new MotoristaVeiculo
            {
                MotoristaId = veiculo.MotoristaId.Value,
                VeiculoId = veiculo.Id
            };

            _context.MotoristaVeiculos.Add(motoristaVeiculo);
            _context.Add(veiculo);
            
            await _context.SaveChangesAsync();

            ViewData["MotoristaId"] = new SelectList(_context.Motoristas, "Id", "Id");
            ViewData["ProprietarioId"] = new SelectList(_context.Proprietarios, "Id", "Id");

            return RedirectToAction(nameof(Index));
        }

        // GET: Veiculoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veiculo = await _context.Veiculos.FindAsync(id);
            if (veiculo == null)
            {
                return NotFound();
            }

            ViewData["MotoristaId"] = new SelectList(_context.Motoristas, "Id", "Id");
            ViewData["ProprietarioId"] = new SelectList(_context.Proprietarios, "Id", "Id");
            
            return View(veiculo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Veiculo veiculo)
        {
            if (id != veiculo.Id)
            {
                return NotFound();
            }
                try
                {
                    _context.Update(veiculo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VeiculoExists(veiculo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

            ViewData["MotoristaId"] = new SelectList(_context.Motoristas, "Id", "Id");
            ViewData["ProprietarioId"] = new SelectList(_context.Proprietarios, "Id", "Id");

            return RedirectToAction(nameof(Index));
        }

        // GET: Veiculoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veiculo = await _context.Veiculos
                .Include(v => v.Motorista)
                .Include(v => v.Proprietario)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (veiculo == null)
            {
                return NotFound();
            }

            return View(veiculo);
        }

        // POST: Veiculoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var veiculo = await _context.Veiculos.FindAsync(id);
            var motoristaVeiculo = await _context.MotoristaVeiculos.FirstOrDefaultAsync(x => x.VeiculoId == id);

            if (veiculo != null)
            {
                _context.Veiculos.Remove(veiculo);

                if (motoristaVeiculo != null)
                {
                    _context.MotoristaVeiculos.Remove(motoristaVeiculo);
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VeiculoExists(int id)
        {
            return _context.Veiculos.Any(e => e.Id == id);
        }
    }
}
