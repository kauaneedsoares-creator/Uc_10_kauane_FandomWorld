using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Uc_10_kauane_FandomWorld.Data;
using Uc_10_kauane_FandomWorld.Models;

namespace Uc_10_kauane_FandomWorld.Controllers
{
    public class PlataformasController : Controller
    {
        private readonly Uc_10_kauane_FandomWorldContext _context;

        public PlataformasController(Uc_10_kauane_FandomWorldContext context)
        {
            _context = context;
        }

        // GET: Plataformas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Plataforma.ToListAsync());
        }

        // GET: Plataformas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plataforma = await _context.Plataforma
                .FirstOrDefaultAsync(m => m.PlataformaId == id);
            if (plataforma == null)
            {
                return NotFound();
            }

            return View(plataforma);
        }

        // GET: Plataformas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Plataformas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlataformaId,Nome")] Plataforma plataforma)
        {
            if (ModelState.IsValid)
            {
                _context.Add(plataforma);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(plataforma);
        }

        // GET: Plataformas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plataforma = await _context.Plataforma.FindAsync(id);
            if (plataforma == null)
            {
                return NotFound();
            }
            return View(plataforma);
        }

        // POST: Plataformas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlataformaId,Nome")] Plataforma plataforma)
        {
            if (id != plataforma.PlataformaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plataforma);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlataformaExists(plataforma.PlataformaId))
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
            return View(plataforma);
        }

        // GET: Plataformas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plataforma = await _context.Plataforma
                .FirstOrDefaultAsync(m => m.PlataformaId == id);
            if (plataforma == null)
            {
                return NotFound();
            }

            return View(plataforma);
        }

        // POST: Plataformas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var plataforma = await _context.Plataforma.FindAsync(id);
            if (plataforma != null)
            {
                _context.Plataforma.Remove(plataforma);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlataformaExists(int id)
        {
            return _context.Plataforma.Any(e => e.PlataformaId == id);
        }


        //Pesquisa
        [HttpGet]
        public async Task<IActionResult> Index(string searchString)
        {
            IQueryable<Plataforma> plataformas = _context.Plataforma;

            if (!string.IsNullOrEmpty(searchString))
            {
                plataformas = plataformas.Where(p => p.Nome.Contains(searchString));
            }
            return View(await plataformas.ToListAsync());
        }
    }
}
