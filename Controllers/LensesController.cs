using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LensZone.Data;
using LensZone.Models;
using Microsoft.AspNetCore.Authorization;

namespace LensZone.Controllers
{
    public class LensesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LensesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Lenses
        public async Task<IActionResult> Index()
        {
              return _context.Lens != null ? 
                          View(await _context.Lens.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Lens'  is null.");
        }
        // GET: Lenses/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }
        public async Task<IActionResult> ShowSearchResults(string SearchPhrase)
        {
            return View("Index", await _context.Lens.Where( j=> j.Name.Contains(SearchPhrase)).ToListAsync());
        }

        // PoST: Lenses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Lens == null)
            {
                return NotFound();
            }

            var lens = await _context.Lens
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lens == null)
            {
                return NotFound();
            }

            return View(lens);
        }

        [Authorize] 

        // GET: Lenses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lenses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Mobile,LeftEye,RightEye")] Lens lens)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lens);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lens);
        }

        // GET: Lenses/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Lens == null)
            {
                return NotFound();
            }

            var lens = await _context.Lens.FindAsync(id);
            if (lens == null)
            {
                return NotFound();
            }
            return View(lens);
        }

        // POST: Lenses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Mobile,LeftEye,RightEye")] Lens lens)
        {
            if (id != lens.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lens);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LensExists(lens.Id))
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
            return View(lens);
        }

        // GET: Lenses/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Lens == null)
            {
                return NotFound();
            }

            var lens = await _context.Lens
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lens == null)
            {
                return NotFound();
            }

            return View(lens);
        }

        // POST: Lenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Lens == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Lens'  is null.");
            }
            var lens = await _context.Lens.FindAsync(id);
            if (lens != null)
            {
                _context.Lens.Remove(lens);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LensExists(int id)
        {
          return (_context.Lens?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
