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
    public class FanficsController : Controller
    {
        private readonly Uc_10_kauane_FandomWorldContext _context;

        public FanficsController(Uc_10_kauane_FandomWorldContext context)
        {
            _context = context;
        }

        // GET: Fanfics
        public async Task<IActionResult> Index()
        {
            var uc_10_kauane_FandomWorldContext = _context.Fanfic.Include(f => f.Fandom).Include(f => f.Plataforma);
            return View(await uc_10_kauane_FandomWorldContext.ToListAsync());
        }

        // GET: Fanfics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fanfic = await _context.Fanfic
                .Include(f => f.Fandom)
                .Include(f => f.Plataforma)
                .FirstOrDefaultAsync(m => m.FanficId == id);
            if (fanfic == null)
            {
                return NotFound();
            }

            return View(fanfic);
        }

        // GET: Fanfics/Create
        public IActionResult Create()
        {
            ViewData["FandomId"] = new SelectList(_context.Fandom, "FandomId", "NomeFandom");
            ViewData["PlataformaId"] = new SelectList(_context.Set<Plataforma>(), "PlataformaId", "Nome");
            return View();
        }

        // POST: Fanfics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FanficId,Titulo,Sinopse,Status,AnoPublicacao,FandomId,PlataformaId,Link")] Fanfic fanfic)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fanfic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FandomId"] = new SelectList(_context.Fandom, "FandomId", "NomeFandom", fanfic.FandomId);
            ViewData["PlataformaId"] = new SelectList(_context.Set<Plataforma>(), "PlataformaId", "Nome", fanfic.PlataformaId);
            return View(fanfic);
        }

        // GET: Fanfics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fanfic = await _context.Fanfic.FindAsync(id);
            if (fanfic == null)
            {
                return NotFound();
            }
            ViewData["FandomId"] = new SelectList(_context.Fandom, "FandomId", "NomeFandom", fanfic.FandomId);
            ViewData["PlataformaId"] = new SelectList(_context.Set<Plataforma>(), "PlataformaId", "Nome", fanfic.PlataformaId);
            return View(fanfic);
        }

        // POST: Fanfics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FanficId,Titulo,Sinopse,Status,AnoPublicacao,FandomId,PlataformaId,Link")] Fanfic fanfic)
        {
            if (id != fanfic.FanficId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fanfic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FanficExists(fanfic.FanficId))
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
            ViewData["FandomId"] = new SelectList(_context.Fandom, "FandomId", "NomeFandom", fanfic.FandomId);
            ViewData["PlataformaId"] = new SelectList(_context.Set<Plataforma>(), "PlataformaId", "Nome", fanfic.PlataformaId);
            return View(fanfic);
        }

        // GET: Fanfics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fanfic = await _context.Fanfic
                .Include(f => f.Fandom)
                .Include(f => f.Plataforma)
                .FirstOrDefaultAsync(m => m.FanficId == id);
            if (fanfic == null)
            {
                return NotFound();
            }

            return View(fanfic);
        }

        // POST: Fanfics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fanfic = await _context.Fanfic.FindAsync(id);
            if (fanfic != null)
            {
                _context.Fanfic.Remove(fanfic);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FanficExists(int id)
        {
            return _context.Fanfic.Any(e => e.FanficId == id);
        }

        //Pesquisa
        [HttpGet]
        public async Task<IActionResult> Index(string searchString)
        {
            IQueryable<Fanfic> fanfics = _context.Fanfic;

            if (!string.IsNullOrEmpty(searchString))
            {
                fanfics = fanfics.Where(f => f.Titulo.Contains(searchString));
            }
            return View(await fanfics.ToListAsync());
        }
    }
}
