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
    public class MarcasController : Controller
    {
        private readonly BdprestamosContext _context;

        public MarcasController(BdprestamosContext context)
        {
            _context = context;
        }

        // GET: Marcas
        public async Task<IActionResult> Index()
        {
              return _context.Marcas != null ? 
                          View(await _context.Marcas.ToListAsync()) :
                          Problem("Entity set 'BdprestamosContext.Marcas'  is null.");
        }

        // GET: Marcas/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Marcas == null)
            {
                return NotFound();
            }

            var marca = await _context.Marcas
                .FirstOrDefaultAsync(m => m.NombreMarca == id);
            if (marca == null)
            {
                return NotFound();
            }

            return View(marca);
        }

        // GET: Marcas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Marcas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NombreMarca,Descripcion,TipoH,Exactitud")] Marca marca)
        {
            if (ModelState.IsValid)
            {
                _context.Add(marca);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(marca);
        }

        // GET: Marcas/Edit
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Marcas == null)
            {
                return NotFound();
            }

            var marca = await _context.Marcas.FindAsync(id);
            if (marca == null)
            {
                return NotFound();
            }
            return View(marca);
        }

        // POST: Marcas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("NombreMarca,Descripcion,TipoH,Exactitud")] Marca marca)
        {
            if (id != marca.NombreMarca)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(marca);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MarcaExists(marca.NombreMarca))
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
            return View(marca);
        }

        // GET: Marcas/Delete
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Marcas == null)
            {
                return NotFound();
            }

            var marca = await _context.Marcas
                .FirstOrDefaultAsync(m => m.NombreMarca == id);
            if (marca == null)
            {
                return NotFound();
            }

            return View(marca);
        }

        // POST: Marcas/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Marcas == null)
            {
                return Problem("Entity set 'BdprestamosContext.Marcas'  is null.");
            }
            var marca = await _context.Marcas.FindAsync(id);
            if (marca != null)
            {
                _context.Marcas.Remove(marca);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MarcaExists(string id)
        {
          return (_context.Marcas?.Any(e => e.NombreMarca == id)).GetValueOrDefault();
        }
    }
}
