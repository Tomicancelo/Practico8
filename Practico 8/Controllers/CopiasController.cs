using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Practico_8.Models;

namespace Practico_8.Controllers
{
    public class CopiasController : Controller
    {
        private readonly VideoRentalContext _context;

        public CopiasController(VideoRentalContext context)
        {
            _context = context;
        }

        // GET: Copias
        public async Task<IActionResult> Index()
        {
            var videoRentalContext = _context.Copia.Include(c => c.IdPeliculaNavigation);
            return View(await videoRentalContext.ToListAsync());
        }

        // GET: Copias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var copia = await _context.Copia
                .Include(c => c.IdPeliculaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (copia == null)
            {
                return NotFound();
            }

            return View(copia);
        }

        // GET: Copias/Create
        public IActionResult Create()
        {
            ViewData["IdPelicula"] = new SelectList(_context.Pelicula, "Id", "Id");
            return View();
        }

        // POST: Copias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdPelicula,Deteriorada,Formato,PrecioAlquiler")] Copia copia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(copia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPelicula"] = new SelectList(_context.Pelicula, "Id", "Id", copia.IdPelicula);
            return View(copia);
        }

        // GET: Copias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var copia = await _context.Copia.FindAsync(id);
            if (copia == null)
            {
                return NotFound();
            }
            ViewData["IdPelicula"] = new SelectList(_context.Pelicula, "Id", "Id", copia.IdPelicula);
            return View(copia);
        }

        // POST: Copias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdPelicula,Deteriorada,Formato,PrecioAlquiler")] Copia copia)
        {
            if (id != copia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(copia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CopiaExists(copia.Id))
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
            ViewData["IdPelicula"] = new SelectList(_context.Pelicula, "Id", "Id", copia.IdPelicula);
            return View(copia);
        }

        // GET: Copias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var copia = await _context.Copia
                .Include(c => c.IdPeliculaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (copia == null)
            {
                return NotFound();
            }

            return View(copia);
        }

        // POST: Copias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var copia = await _context.Copia.FindAsync(id);
            if (copia != null)
            {
                _context.Copia.Remove(copia);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CopiaExists(int id)
        {
            return _context.Copia.Any(e => e.Id == id);
        }
    }
}
