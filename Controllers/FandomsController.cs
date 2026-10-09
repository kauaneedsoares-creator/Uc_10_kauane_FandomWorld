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
    public class FandomsController : Controller
    {
        private readonly Uc_10_kauane_FandomWorldContext _context;

        public FandomsController(Uc_10_kauane_FandomWorldContext context)
        {
            _context = context;
        }

        // GET: Fandoms
        public async Task<IActionResult> Index()
        {
            return View(await _context.Fandom.ToListAsync());
        }

        // GET: Fandoms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fandom = await _context.Fandom
                .FirstOrDefaultAsync(m => m.FandomId == id);
            if (fandom == null)
            {
                return NotFound();
            }

            return View(fandom);
        }

        // GET: Fandoms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fandoms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FandomId,NomeFandom,Descricao,Categoria")] Fandom fandom)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fandom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fandom);
        }

        // GET: Fandoms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fandom = await _context.Fandom.FindAsync(id);
            if (fandom == null)
            {
                return NotFound();
            }
            return View(fandom);
        }

        // POST: Fandoms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FandomId,NomeFandom,Descricao,Categoria")] Fandom fandom)
        {
            if (id != fandom.FandomId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fandom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FandomExists(fandom.FandomId))
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
            return View(fandom);
        }

        // GET: Fandoms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fandom = await _context.Fandom
                .FirstOrDefaultAsync(m => m.FandomId == id);
            if (fandom == null)
            {
                return NotFound();
            }

            return View(fandom);
        }

        // POST: Fandoms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fandom = await _context.Fandom.FindAsync(id);
            if (fandom != null)
            {
                _context.Fandom.Remove(fandom);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FandomExists(int id)
        {
            return _context.Fandom.Any(e => e.FandomId == id);
        }

        public IActionResult Fandom()
        {
            return View();
        }

        //Pesquisa
        [HttpGet]
        public async Task<IActionResult> Index(string searchString)
        {
            IQueryable<Fandom> fandoms = _context.Fandom;

            if (!string.IsNullOrEmpty(searchString))
            {
                fandoms = fandoms.Where(f => f.NomeFandom.Contains(searchString));
            }
            return View(await fandoms.ToListAsync());
        }

      
    }
}
