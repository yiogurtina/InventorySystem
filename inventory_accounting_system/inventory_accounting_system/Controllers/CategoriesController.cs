using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using inventory_accounting_system.Data;
using inventory_accounting_system.Interface;
using inventory_accounting_system.Models;
using Moq;

namespace inventory_accounting_system.Controllers
{
    public class CategoriesController : Controller
    {
        #region Dependency Injection

        private readonly ApplicationDbContext _context;
//        Mock<ICategoryReposytory> mock = new Mock<ICategoryReposytory>();

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Index

        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.ToListAsync());
        }

        #endregion

        #region Test Mock

        //        // GET: Categories
        //                public async Task<IActionResult> Index()
        //        {
        //            mock.Setup(m => m.Categories).Returns(new List<Category>
        //            {
        //                new Category(){Id = "1c200a82-81f2-40f1-86b3-772014cb1e63", Name = "Мебель", Prefix = "F_"},
        //                new Category(){Id = "1c200a82-81f2-40f1-86b3-772014cb1e63", Name = "Компутеры", Prefix = "F_"},
        //                new Category(){Id = "1c200a82-81f2-40f1-86b3-772014cb1e63", Name = "Машины", Prefix = "Fльвытдм_"}
        //            });
        //
        //            return View(mock.Object.Categories.ToList());
        //        }

        #endregion

        #region Details

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .SingleOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        public IActionResult Create()
        {
            return View();
        }


        #endregion

        #region Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Prefix,Id")] Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.SingleOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }



        #endregion

        #region Edit

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,Prefix,Id")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
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

            return View(category);
        }

        #endregion

        #region Delete

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .SingleOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var category = await _context.Categories.SingleOrDefaultAsync(m => m.Id == id);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region CategoryExists

        private bool CategoryExists(string id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }

        #endregion
    }
}