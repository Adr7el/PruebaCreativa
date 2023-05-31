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
    public class EquiposController : Controller
    {
        private readonly BdprestamosContext _context;

        public EquiposController(BdprestamosContext context)
        {
            _context = context;
        }

        // GET: Equipos
        public async Task<IActionResult> Index()
        {
            var bdprestamosContext = _context.Equipos.Include(e => e.Nombre_de_la_Marca);
            return View(await bdprestamosContext.ToListAsync());
        }

        // GET: Equipos/Details
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Equipos == null)
            {
                return NotFound();
            }

            var equipo = await _context.Equipos
                .Include(e => e.Nombre_de_la_Marca)
                .FirstOrDefaultAsync(m => m.NumeroSerie == id);
            if (equipo == null)
            {
                return NotFound();
            }

            return View(equipo);
        }

        // GET: Equipos/Create
        public IActionResult Create()
        {
            ViewData["NombreMarca"] = new SelectList(_context.Marcas, "NombreMarca", "NombreMarca");
            return View();
        }

        // POST: Equipos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NumeroSerie,NombreMarca,NombreEquipo,Descripcion")] Equipo equipo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(equipo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NombreMarca"] = new SelectList(_context.Marcas, "NombreMarca", "NombreMarca", equipo.NombreMarca);
            return View(equipo);
        }

        // GET: Equipos/Edit
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Equipos == null)
            {
                return NotFound();
            }

            var equipo = await _context.Equipos.FindAsync(id);
            if (equipo == null)
            {
                return NotFound();
            }
            ViewData["NombreMarca"] = new SelectList(_context.Marcas, "NombreMarca", "NombreMarca", equipo.NombreMarca);
            return View(equipo);
        }

        // POST: Equipos/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("NumeroSerie,NombreMarca,NombreEquipo,Descripcion")] Equipo equipo)
        {
            if (id != equipo.NumeroSerie)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(equipo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipoExists(equipo.NumeroSerie))
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
            ViewData["NombreMarca"] = new SelectList(_context.Marcas, "NombreMarca", "NombreMarca", equipo.NombreMarca);
            return View(equipo);
        }

        // GET: Equipos/Delete
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Equipos == null)
            {
                return NotFound();
            }

            var equipo = await _context.Equipos
                .Include(e => e.Nombre_de_la_Marca)
                .FirstOrDefaultAsync(m => m.NumeroSerie == id);
            if (equipo == null)
            {
                return NotFound();
            }

            return View(equipo);
        }

        // POST: Equipos/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Equipos == null)
            {
                return Problem("Entity set 'BdprestamosContext.Equipos'  is null.");
            }
            var equipo = await _context.Equipos.FindAsync(id);
            if (equipo != null)
            {
                _context.Equipos.Remove(equipo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EquipoExists(string id)
        {
          return (_context.Equipos?.Any(e => e.NumeroSerie == id)).GetValueOrDefault();
        }
    }
}
