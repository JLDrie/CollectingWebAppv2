using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CollectingWebApp.Data;
using CollectingWebApp.Models;
using Microsoft.AspNetCore.Mvc.TagHelpers;

namespace CollectingWebApp.Controllers
{
    public class CollectionsController : Controller
    {
        private readonly AppDbContext _context;

        public CollectionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Collections
        public async Task<IActionResult> Index(int? CategoryId)
        {

            if (CategoryId != null)
            {

                var AppDbContext = _context.Collection
                    .Include(v => v.Category)
                    .Where(o => o.CategoryId == CategoryId);
                return View(await AppDbContext.ToListAsync());
            }
            else
            {
                Counter();
                var appDbContext = _context.Collection.Include(c => c.Category);
                return View(await appDbContext.ToListAsync());
            }
        }

        // GET: Collections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Collection == null)
            {
                return NotFound();
            }

            var collection = await _context.Collection
                .Include(c => c.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (collection == null)
            {
                return NotFound();
            }

            return View(collection);
        }

        // GET: Collections/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name");
            return View();
        }

        // POST: Collections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,DateOfCreation,CategoryId")] Collection collection)
        {
            //if (ModelState.IsValid)
            //{
            //    _context.Add(collection);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            _context.Add(collection);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", collection.CategoryId);
            return View(collection);
        }

        // GET: Collections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Collection == null)
            {
                return NotFound();
            }

            var collection = await _context.Collection.FindAsync(id);
            if (collection == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", collection.CategoryId);
            return View(collection);
        }

        // POST: Collections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,DateOfCreation,CategoryId")] Collection collection)
        {
            if (id != collection.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(collection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CollectionExists(collection.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", collection.CategoryId);
            return View(collection);
        }

        // GET: Collections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Collection == null)
            {
                return NotFound();
            }

            var collection = await _context.Collection
                .Include(c => c.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (collection == null)
            {
                return NotFound();
            }

            return View(collection);
        }

        // POST: Collections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Collection == null)
            {
                return Problem("Entity set 'AppDbContext.Collection'  is null.");
            }
            var collection = await _context.Collection.FindAsync(id);
            if (collection != null)
            {
                _context.Collection.Remove(collection);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CollectionExists(int id)
        {
          return (_context.Collection?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        public void Counter()
        {
            //int i = 0;
            //var context = _context.Object;
            //foreach(var item in context)
            //{
            //    if(item.CollectionId == collid)
            //    {
            //        //int i = 0;
            //        i++;
            //        //List<int> count = new List<int>();

            //    }
            //    ViewBag.Count = i;

            //}
            //int o = 0;
            //foreach(var collection in _context.Collection)
            //{
            //    foreach(var item in _context.Object)
            //    {
            //        if(item.CollectionId == collection.Id) 
            //        {
            //            o++;
            //        }
            //    }
            //    ViewBag.Count = o;

            //}
            List<Models.Object> objectscountlist = new List<Models.Object>();
            int collid = 2;
            int i = 0;
            foreach (var item1 in _context.Object)
            {
                if(item1.CollectionId == collid)
                {
                    objectscountlist.Add(item1);
                    i = objectscountlist.Count();
                }

            }
            ViewBag.Count = i;

            //foreach (var item in _context.Collection)
            //{
            //    List<Models.Object> objects = new List<Models.Object>();
            //    Models.Object object1 = new Models.Object();

            //    objects.Add(object1);
            //    i = objects.Count();
            //}
            //    ViewBag.Count = i;
            
         //   _context.Object
        }

        //public async Task ObjectCounter()
        //{
        //    var collection =  _context.Collection;
        //    foreach (var collection1 in collection)
        //    {
        //        foreach(var item in _context.Object)
        //        {
        //            if (collection1.Id == item.CollectionId)
        //            {
        //                collection1.Objects.Add(item);
        //            }
        //            ViewBag.Count = collection1.Objects.Count();


        //        }
        //    }

        //}
    }
}
