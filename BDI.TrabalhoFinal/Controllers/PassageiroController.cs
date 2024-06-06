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
    public class PassageiroController : Controller
    {
        private readonly BancoDeDados _context;

        public PassageiroController(BancoDeDados context)
        {
            _context = context;
        }

        // GET: Passageiroes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Passageiros.ToListAsync());
        }

        // GET: Passageiroes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var passageiro = await _context.Passageiros
                .FirstOrDefaultAsync(m => m.Id == id);
            if (passageiro == null)
            {
                return NotFound();
            }

            return View(passageiro);
        }

        // GET: Passageiroes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Passageiroes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Passageiro passageiro)
        {
            //if (ModelState.IsValid)
            //{
                _context.Add(passageiro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            return View(passageiro);
        }

        // GET: Passageiroes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var passageiro = await _context.Passageiros.FindAsync(id);
            if (passageiro == null)
            {
                return NotFound();
            }
            return View(passageiro);
        }

        // POST: Passageiroes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Passageiro passageiro)
        {
            if (id != passageiro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(passageiro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PassageiroExists(passageiro.Id))
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
            return View(passageiro);
        }

        // GET: Passageiroes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var passageiro = await _context.Passageiros
                .FirstOrDefaultAsync(m => m.Id == id);
            if (passageiro == null)
            {
                return NotFound();
            }

            return View(passageiro);
        }

        // POST: Passageiroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var passageiro = await _context.Passageiros.FindAsync(id);
            if (passageiro != null)
            {
                _context.Passageiros.Remove(passageiro);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PassageiroExists(int id)
        {
            return _context.Passageiros.Any(e => e.Id == id);
        }
    }
}
