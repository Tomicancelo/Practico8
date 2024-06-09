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
    public class AlquileresController : Controller
    {
        private readonly VideoRentalContext _context;

        public AlquileresController(VideoRentalContext context)
        {
            _context = context;
        }

        // GET: Alquileres
        public async Task<IActionResult> Index()
        {
            var videoRentalContext = _context.Alquiler.Include(a => a.IdClienteNavigation).Include(a => a.IdCopiaNavigation);
            return View(await videoRentalContext.ToListAsync());
        }

        // GET: Alquileres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alquilere = await _context.Alquiler
                .Include(a => a.IdClienteNavigation)
                .Include(a => a.IdCopiaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alquilere == null)
            {
                return NotFound();
            }

            return View(alquilere);
        }

        // GET: Alquileres/Create
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_context.Cliente, "Id", "Id");
            ViewData["IdCopia"] = new SelectList(_context.Copia, "Id", "Id");
            return View();
        }

        // POST: Alquileres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdCopia,IdCliente,FechaAlquiler,FechaTope,FechaEntregada")] Alquilere alquilere)
        {
            if (ModelState.IsValid)
            {
                _context.Add(alquilere);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.Cliente, "Id", "Id", alquilere.IdCliente);
            ViewData["IdCopia"] = new SelectList(_context.Copia, "Id", "Id", alquilere.IdCopia);
            return View(alquilere);
        }

        // GET: Alquileres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alquilere = await _context.Alquiler.FindAsync(id);
            if (alquilere == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.Cliente, "Id", "Id", alquilere.IdCliente);
            ViewData["IdCopia"] = new SelectList(_context.Copia, "Id", "Id", alquilere.IdCopia);
            return View(alquilere);
        }

        // POST: Alquileres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdCopia,IdCliente,FechaAlquiler,FechaTope,FechaEntregada")] Alquilere alquilere)
        {
            if (id != alquilere.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(alquilere);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlquilereExists(alquilere.Id))
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
            ViewData["IdCliente"] = new SelectList(_context.Cliente, "Id", "Id", alquilere.IdCliente);
            ViewData["IdCopia"] = new SelectList(_context.Copia, "Id", "Id", alquilere.IdCopia);
            return View(alquilere);
        }

        // GET: Alquileres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alquilere = await _context.Alquiler
                .Include(a => a.IdClienteNavigation)
                .Include(a => a.IdCopiaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alquilere == null)
            {
                return NotFound();
            }

            return View(alquilere);
        }

        // POST: Alquileres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var alquilere = await _context.Alquiler.FindAsync(id);
            if (alquilere != null)
            {
                _context.Alquiler.Remove(alquilere);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlquilereExists(int id)
        {
            return _context.Alquiler.Any(e => e.Id == id);
        }
    }
}
