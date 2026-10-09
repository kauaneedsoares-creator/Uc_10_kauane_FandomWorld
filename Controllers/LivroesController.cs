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
    public class LivroesController : Controller
    {
        private readonly Uc_10_kauane_FandomWorldContext _context;

        public LivroesController(Uc_10_kauane_FandomWorldContext context)
        {
            _context = context;
        }

        // GET: Livroes
        public async Task<IActionResult> Index()
        {
            var uc_10_kauane_FandomWorldContext = _context.Livro.Include(l => l.Fandom);
            return View(await uc_10_kauane_FandomWorldContext.ToListAsync());
        }

        // GET: Livroes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = await _context.Livro
                .Include(l => l.Fandom)
                .FirstOrDefaultAsync(m => m.LivroId == id);
            if (livro == null)
            {
                return NotFound();
            }

            return View(livro);
        }

        // GET: Livroes/Create
        public IActionResult Create()
        {
            ViewData["FandomId"] = new SelectList(_context.Fandom, "FandomId", "NomeFandom");
            return View();
        }

        // POST: Livroes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LivroId,Titulo,Autor,AnoPublicacao,Sinopse,FandomId")] Livro livro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(livro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FandomId"] = new SelectList(_context.Fandom, "FandomId", "NomeFandom", livro.FandomId);
            return View(livro);
        }

        // GET: Livroes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = await _context.Livro.FindAsync(id);
            if (livro == null)
            {
                return NotFound();
            }
            ViewData["FandomId"] = new SelectList(_context.Fandom, "FandomId", "NomeFandom", livro.FandomId);
            return View(livro);
        }

        // POST: Livroes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LivroId,Titulo,Autor,AnoPublicacao,Sinopse,FandomId")] Livro livro)
        {
            if (id != livro.LivroId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(livro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LivroExists(livro.LivroId))
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
            ViewData["FandomId"] = new SelectList(_context.Fandom, "FandomId", "NomeFandom", livro.FandomId);
            return View(livro);
        }

        // GET: Livroes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = await _context.Livro
                .Include(l => l.Fandom)
                .FirstOrDefaultAsync(m => m.LivroId == id);
            if (livro == null)
            {
                return NotFound();
            }

            return View(livro);
        }

        // POST: Livroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var livro = await _context.Livro.FindAsync(id);
            if (livro != null)
            {
                _context.Livro.Remove(livro);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LivroExists(int id)
        {
            return _context.Livro.Any(e => e.LivroId == id);
        }



        //Pesquisa
        [HttpGet]
        public async Task<IActionResult> Index(string searchString)
        {
            IQueryable<Livro> livros = _context.Livro;

            if (!string.IsNullOrEmpty(searchString))
            {
                livros = livros.Where(l => l.Titulo.Contains(searchString));
            }
            return View(await livros.ToListAsync());
        }
    }
}
