using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using inventory_accounting_system.Data;
using inventory_accounting_system.Models;

namespace inventory_accounting_system.Controllers
{
    public class AssetsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AssetsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Assets
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Assets.Include(a => a.Category)
                .Include(a => a.Employee)
                .Include(a => a.Office)
                .Include(a => a.Storage)
                .Include(a => a.Supplier);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Assets/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset = await _context.Assets
                .Include(a => a.Category)
                .Include(a => a.Employee)
                .Include(a => a.Office)
                .Include(a => a.Storage)
                .Include(a => a.Supplier)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (asset == null)
            {
                return NotFound();
            }

            return View(asset);
        }

        // GET: Assets/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id");
            ViewData["EmployeeId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["OfficeId"] = new SelectList(_context.Offices, "Id", "Id");
            ViewData["StorageId"] = new SelectList(_context.Storages, "Id", "Id");
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Id");
            return View();
        }

        // POST: Assets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,CategoryId,InventNumber,Date,OfficeId,StorageId,SupplierId,EmployeeId,ImagesUrl,Id")] Asset asset)
        {
            if (ModelState.IsValid)
            {
                _context.Add(asset);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", asset.CategoryId);
            ViewData["EmployeeId"] = new SelectList(_context.Users, "Id", "Id", asset.EmployeeId);
            ViewData["OfficeId"] = new SelectList(_context.Offices, "Id", "Id", asset.OfficeId);
            ViewData["StorageId"] = new SelectList(_context.Storages, "Id", "Id", asset.StorageId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Id", asset.SupplierId);
            return View(asset);
        }

        // GET: Assets/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset = await _context.Assets.SingleOrDefaultAsync(m => m.Id == id);
            if (asset == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", asset.CategoryId);
            ViewData["EmployeeId"] = new SelectList(_context.Users, "Id", "Id", asset.EmployeeId);
            ViewData["OfficeId"] = new SelectList(_context.Offices, "Id", "Id", asset.OfficeId);
            ViewData["StorageId"] = new SelectList(_context.Storages, "Id", "Id", asset.StorageId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Id", asset.SupplierId);
            return View(asset);
        }

        // POST: Assets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,CategoryId,InventNumber,Date,OfficeId,StorageId,SupplierId,EmployeeId,ImagesUrl,Id")] Asset asset)
        {
            if (id != asset.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(asset);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssetExists(asset.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", asset.CategoryId);
            ViewData["EmployeeId"] = new SelectList(_context.Users, "Id", "Id", asset.EmployeeId);
            ViewData["OfficeId"] = new SelectList(_context.Offices, "Id", "Id", asset.OfficeId);
            ViewData["StorageId"] = new SelectList(_context.Storages, "Id", "Id", asset.StorageId);
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Id", asset.SupplierId);
            return View(asset);
        }

        // GET: Assets/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset = await _context.Assets
                .Include(a => a.Category)
                .Include(a => a.Employee)
                .Include(a => a.Office)
                .Include(a => a.Storage)
                .Include(a => a.Supplier)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (asset == null)
            {
                return NotFound();
            }

            return View(asset);
        }

        // POST: Assets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var asset = await _context.Assets.SingleOrDefaultAsync(m => m.Id == id);
            _context.Assets.Remove(asset);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssetExists(string id)
        {
            return _context.Assets.Any(e => e.Id == id);
        }
    }
}
