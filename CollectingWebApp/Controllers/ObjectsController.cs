using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CollectingWebApp.Data;
using CollectingWebApp.Models;

namespace CollectingWebApp.Controllers
{
    public class ObjectsController : Controller
    {
        private readonly AppDbContext _context;

        public ObjectsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Objects
        public async Task<IActionResult> Index(int? CollectionId)
        {
            if (CollectionId != null)
            {
                var AppDbContext = _context.Object
                    .Include(v => v.Collection)
                    .Where(o => o.CollectionId == CollectionId);

                return View(await AppDbContext.ToListAsync());
            }
            else
            {
                var appDbContext = _context.Object.Include(c => c.Collection);
                return View(await appDbContext.ToListAsync());
            }
        }

        // GET: Objects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Object == null)
            {
                return NotFound();
            }

            var @object = await _context.Object
                .Include(c => c.Collection)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@object == null)
            {
                return NotFound();
            }

            return View(@object);
        }

        // GET: Objects/Create
        public IActionResult Create()
        {
            ViewData["CollectionId"] = new SelectList(_context.Collection, "Id", "Name");
            return View();
        }

        // POST: Objects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ObjectName,ObjectDescription,Price,Worth,CollectionId")] Models.Object @object)
        {
            //if (ModelState.IsValid)
            //{
            //    _context.Add(@object);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            _context.Add(@object);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            ViewData["CollectionId"] = new SelectList(_context.Collection, "Id", "Name", @object.CollectionId);
            return View(@object);
        }

        // GET: Objects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Object == null)
            {
                return NotFound();
            }

            var @object = await _context.Object.FindAsync(id);
            if (@object == null)
            {
                return NotFound();
            }
            ViewData["CollectionId"] = new SelectList(_context.Collection, "Id", "Name", @object.CollectionId);
            return View(@object);
        }

        // POST: Objects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ObjectName,ObjectDescription,Price,Worth,CollectionId")] Models.Object @object)
        {
            if (id != @object.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@object);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ObjectExists(@object.Id))
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
            ViewData["CollectionId"] = new SelectList(_context.Collection, "Id", "Name", @object.CollectionId);
            return View(@object);
        }

        // GET: Objects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Object == null)
            {
                return NotFound();
            }

            var @object = await _context.Object
                .Include(c => c.Collection)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@object == null)
            {
                return NotFound();
            }

            return View(@object);
        }

        // POST: Objects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Object == null)
            {
                return Problem("Entity set 'AppDbContext.Object'  is null.");
            }
            var @object = await _context.Object.FindAsync(id);
            if (@object != null)
            {
                _context.Object.Remove(@object);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ObjectExists(int id)
        {
          return (_context.Object?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
