using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using inventory_accounting_system.Data;
using inventory_accounting_system.Models;
using Microsoft.AspNetCore.Identity;
using inventory_accounting_system.ViewModel;

namespace inventory_accounting_system.Controllers
{
    public class OfficesController : Controller
    {
        #region Dependency Injection

        private readonly ApplicationDbContext _context;
        private readonly UserManager<Employee> _userManager;

        public OfficesController(ApplicationDbContext context, UserManager<Employee> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        #endregion

        #region Index

        public async Task<IActionResult> Index(string officeId)
        {

            #region Search office Manager

            var userId = _userManager.GetUserId(User);
            var userName = _userManager.GetUserName(User);

            ViewData["UserId"] = userName;

            var officeIdEmployee = _context.Offices;

            List<string> managers = new List<string>();
            var userFromOff = _context.Users.Where(u => u.IsDelete == false);
            foreach (var usr in userFromOff)
            {
                if (await _userManager.IsInRoleAsync(usr, "User") && usr.Id == userId) // нашли юзера который залогинен
                {
                    var userLoginNew = usr.Id;
                    var userOfficeId = usr.OfficeId;

                    foreach (var office in officeIdEmployee)
                    {
                        if (userOfficeId == office.Id)
                        {
                            foreach (var usrManager in userFromOff)
                            {
                                if (await _userManager.IsInRoleAsync(usrManager, "Manager") && usrManager.OfficeId == userOfficeId)
                                {
                                    ViewData["EmployeeId"] = new SelectList(_context.Users.Where(u => u.Id == usrManager.Id), "Id", "Name");
                                    ViewData["OfficeId"] = new SelectList(_context.Offices.Where(o => o.Id == usr.OfficeId), "Id", "Title");

                                    ViewData["EmployeeIdName"] = usrManager.Name;
                                    ViewData["OfficeIdTitle"] = office.Title;
                                }
                            }
                            
                        }
                    }

                    
                    

                }
            }

            #endregion

            var offices = _context.Offices.ToList();
            ViewData["Offices"] = new SelectList(offices.OrderByDescending(x => x.Title), "Id", "Title", officeId);

            if (officeId.IsNullOrEmpty())
            {
                var defaultOffice = offices
                    .FirstOrDefault();
                if (defaultOffice == null)
                {
                    return View();
                }
                officeId = defaultOffice.Id;
            }

            List<Employee> employees = _context.Users.Where(e => e.OfficeId == officeId).ToList();

            var categoryAssets = _context.Assets
                .Where(a => a.OfficeId == officeId) 
                .Include(a => a.Category)
                .GroupBy(a => new { a.CategoryId, a.Category.Name })
                .Select(g => new CategoryAssetCountViewModel
                {
                    Id = g.Key.CategoryId,
                    CategoryName = g.Key.Name,
                    AssetsCount = g.Count()
                }).ToList();

            OfficeIndexViewModel model = new OfficeIndexViewModel()
            {
                CategoryAssetCountViewModels = categoryAssets,
                Employees = employees,
                Office = _context.Offices.FirstOrDefault(e => e.Id == officeId)
            };

            return View(model);
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var office = await _context.Offices
                .SingleOrDefaultAsync(m => m.Id == id);
            if (office == null)
            {
                return NotFound();
            }

            return View(office);
        }

        #endregion

        #region Create

        // GET: Offices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Offices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Id")] Office office)
        {
            if (ModelState.IsValid)
            {
                _context.Add(office);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(office);
        }

        #endregion

        #region Edit

        // GET: Offices/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var office = await _context.Offices.SingleOrDefaultAsync(m => m.Id == id);
            if (office == null)
            {
                return NotFound();
            }
            return View(office);
        }

        // POST: Offices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Title,Id")] Office office)
        {
            if (id != office.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(office);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OfficeExists(office.Id))
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
            return View(office);
        }

        #endregion

        #region Delete

        // GET: Offices/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var office = await _context.Offices
                .SingleOrDefaultAsync(m => m.Id == id);
            if (office == null)
            {
                return NotFound();
            }

            return View(office);
        }

        // POST: Offices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var office = await _context.Offices.SingleOrDefaultAsync(m => m.Id == id);
            _context.Offices.Remove(office);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region CategoryAssets

        public async Task<IActionResult> CategoryAssets(string officeId, string categoryId)
        {
            ViewData["OfficeId"] = new SelectList(_context.Offices, "Id", "Title");
            var assets = _context.Assets
                .Include(a => a.Category)
                .Include(a => a.Supplier)
                .Where(a => a.IsActive == false)
                .Where(a => a.OfficeId == officeId)
                .Where(a => a.CategoryId == categoryId);
            return View(await assets.ToListAsync());
        }

        #endregion

        #region OfficeExists

        private bool OfficeExists(string id)
        {
            return _context.Offices.Any(e => e.Id == id);
        }

        #endregion
    }
}
