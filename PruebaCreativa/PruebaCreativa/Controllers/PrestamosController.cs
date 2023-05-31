using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PruebaCreativa.Models;

namespace PruebaCreativa.Controllers
{
    public class PrestamosController : Controller
    {
        private readonly BdprestamosContext _context;

        public PrestamosController(BdprestamosContext context)
        {
            _context = context;
        }

        // GET: Prestamos
        public async Task<IActionResult> Index()
        {
            var bdprestamosContext = _context.Prestamos.Include(p => p.Nombre_de_la_Marca);
            return View(await bdprestamosContext.ToListAsync());
        }

        // GET: Prestamos/Details
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Prestamos == null)
            {
                return NotFound();
            }

            var prestamo = await _context.Prestamos
                .Include(p => p.Nombre_de_la_Marca)
                .FirstOrDefaultAsync(m => m.Persona == id);
            if (prestamo == null)
            {
                return NotFound();
            }

            return View(prestamo);
        }

        // GET: Prestamos/Create
        public IActionResult Create()
        {
            ViewData["NombreMarca"] = new SelectList(_context.Marcas, "NombreMarca", "NombreMarca");
            return View();
        }

        // POST: Prestamos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Persona,NombreMarca,NombreEquipo,FechaInicio,FechaFin,Estado")] Prestamo prestamo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prestamo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NombreMarca"] = new SelectList(_context.Marcas, "NombreMarca", "NombreMarca", prestamo.NombreMarca);
            return View(prestamo);
        }

        // GET: Prestamos/Edit
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Prestamos == null)
            {
                return NotFound();
            }

            var prestamo = await _context.Prestamos.FindAsync(id);
            if (prestamo == null)
            {
                return NotFound();
            }
            ViewData["NombreMarca"] = new SelectList(_context.Marcas, "NombreMarca", "NombreMarca", prestamo.NombreMarca);
            return View(prestamo);
        }

        // POST: Prestamos/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Persona,NombreMarca,NombreEquipo,FechaInicio,FechaFin,Estado")] Prestamo prestamo)
        {
            if (id != prestamo.Persona)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prestamo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrestamoExists(prestamo.Persona))
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
            ViewData["NombreMarca"] = new SelectList(_context.Marcas, "NombreMarca", "NombreMarca", prestamo.NombreMarca);
            return View(prestamo);
        }

        // GET: Prestamos/Delete
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Prestamos == null)
            {
                return NotFound();
            }

            var prestamo = await _context.Prestamos
                .Include(p => p.Nombre_de_la_Marca)
                .FirstOrDefaultAsync(m => m.Persona == id);
            if (prestamo == null)
            {
                return NotFound();
            }

            return View(prestamo);
        }

        // POST: Prestamos/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Prestamos == null)
            {
                return Problem("Entity set 'BdprestamosContext.Prestamos'  is null.");
            }
            var prestamo = await _context.Prestamos.FindAsync(id);
            if (prestamo != null)
            {
                _context.Prestamos.Remove(prestamo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrestamoExists(string id)
        {
          return (_context.Prestamos?.Any(e => e.Persona == id)).GetValueOrDefault();
        }
    }
}
