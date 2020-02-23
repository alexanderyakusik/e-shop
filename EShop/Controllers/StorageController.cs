using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EShop.Data;
using EShop.Models.App;
using EShop.Infrastructure.Extensions;

namespace EShop.Controllers
{
    public class StorageController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StorageController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Storage
        public async Task<IActionResult> Index()
        {
            var storageGoods = await _context.Good.Where(good => !good.InCatalog).ToListAsync();
            var catalogGoods = await _context.Good.Where(good => good.InCatalog).ToListAsync();

            return View(new StorageGoodsModel { StorageGoods = storageGoods, CatalogGoods = catalogGoods, });
        }

        // GET: Storage/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var good = await _context.Good
                .FirstOrDefaultAsync(m => m.Id == id);
            if (good == null)
            {
                return NotFound();
            }

            return View(good);
        }

        // GET: Storage/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Storage/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,Price,Amount,InCatalog")] Good good)
        {
            if (ModelState.IsValid)
            {
                _context.Add(good);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(good);
        }

        // GET: Storage/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var good = await _context.Good.FindAsync(id);
            if (good == null)
            {
                return NotFound();
            }
            return View(good);
        }

        // POST: Storage/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,Price,Amount,InCatalog")] Good good)
        {
            if (id != good.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(good);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GoodExists(good.Id))
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
            return View(good);
        }

        // GET: Storage/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var good = await _context.Good
                .FirstOrDefaultAsync(m => m.Id == id);
            if (good == null)
            {
                return NotFound();
            }

            return View(good);
        }

        // POST: Storage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var good = await _context.Good.FindAsync(id);
            _context.Good.Remove(good);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AddToCatalog(int? id)
        {
            return await SwitchStorage(id, "Storage");
        }

        public async Task<IActionResult> AddToStorage(int? id)
        {
            return await SwitchStorage(id, "Catalog");
        }

        private async Task<IActionResult> SwitchStorage(int? id, string source)
        {
            if (id == null)
            {
                return NotFound();
            }

            var good = await _context.Good.FindAsync(id);
            if (good == null)
            {
                return NotFound();
            }

            good.InCatalog = !good.InCatalog;
            _context.Update(good);
            await _context.SaveChangesAsync();

            TempData.SetActiveTab(source);

            return RedirectToAction(nameof(Index));
        }

        private bool GoodExists(int id)
        {
            return _context.Good.Any(e => e.Id == id);
        }
    }
}
