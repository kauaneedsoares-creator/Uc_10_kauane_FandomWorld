using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uc_10_kauane_FandomWorld.Controllers;
using Uc_10_kauane_FandomWorld.Data;
using Uc_10_kauane_FandomWorld.Models;

namespace Uc_10_kauane_FandomWorld.Controllers
{
    public class PersonagemsController : Controller
    {
        private readonly Uc_10_kauane_FandomWorldContext _context;

        public PersonagemsController(Uc_10_kauane_FandomWorldContext context)
        {
            _context = context;
        }

        // GET: Personagems
        public async Task<IActionResult> Index()
        {
            var uc_10_kauane_FandomWorldContext = _context.Personagem.Include(p => p.Fandom);
            return View(await uc_10_kauane_FandomWorldContext.ToListAsync());
        }

        // GET: Personagems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personagem = await _context.Personagem
                .Include(p => p.Fandom)
                .FirstOrDefaultAsync(m => m.PersonagemId == id);
            if (personagem == null)
            {
                return NotFound();
            }

            return View(personagem);
        }

        // GET: Personagems/Create
        public IActionResult Create()
        {
            ViewData["FandomId"] = new SelectList(_context.Fandom, "FandomId", "NomeFandom");
            return View();
        }

        // POST: Personagems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonagemId,Nome,Descricao,FandomId")] Personagem personagem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personagem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FandomId"] = new SelectList(_context.Fandom, "FandomId", "NomeFandom", personagem.FandomId);
            return View(personagem);
        }

        // GET: Personagems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personagem = await _context.Personagem.FindAsync(id);
            if (personagem == null)
            {
                return NotFound();
            }
            ViewData["FandomId"] = new SelectList(_context.Fandom, "FandomId", "NomeFandom", personagem.FandomId);
            return View(personagem);
        }

        // POST: Personagems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PersonagemId,Nome,Descricao,FandomId")] Personagem personagem)
        {
            if (id != personagem.PersonagemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personagem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonagemExists(personagem.PersonagemId))
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
            ViewData["FandomId"] = new SelectList(_context.Fandom, "FandomId", "NomeFandom", personagem.FandomId);
            return View(personagem);
        }

        // GET: Personagems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personagem = await _context.Personagem
                .Include(p => p.Fandom)
                .FirstOrDefaultAsync(m => m.PersonagemId == id);
            if (personagem == null)
            {
                return NotFound();
            }

            return View(personagem);
        }

        // POST: Personagems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var personagem = await _context.Personagem.FindAsync(id);
            if (personagem != null)
            {
                _context.Personagem.Remove(personagem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonagemExists(int id)
        {
            return _context.Personagem.Any(e => e.PersonagemId == id);
        }


        //Pesquisa
        [HttpGet]
        public async Task<IActionResult> Index(string searchString)
        {
            IQueryable<Personagem> personagems = _context.Personagem;

            if (!string.IsNullOrEmpty(searchString))
            {
                personagems = personagems.Where(p => p.Nome.Contains(searchString));
            }
            return View(await personagems.ToListAsync());
        }
    }
}
