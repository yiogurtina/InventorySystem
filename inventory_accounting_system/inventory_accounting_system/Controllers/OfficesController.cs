using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Castle.Core.Internal;
using inventory_accounting_system.Data;
using inventory_accounting_system.Models;
using inventory_accounting_system.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace inventory_accounting_system.Controllers {
    public class OfficesController : Controller {
        #region Dependency Injection

        private readonly ApplicationDbContext _context;
        private readonly UserManager<Employee> _userManager;

        public OfficesController (ApplicationDbContext context, UserManager<Employee> userManager) {
            _context = context;
            _userManager = userManager;
        }

        #endregion

        #region Index

        [Authorize]
        public async Task<IActionResult> Index (string officeId) {

            var mainStorage = _context.Offices.FirstOrDefault (o => o.Title == "Главный склад");
            if (mainStorage != null) {
                ViewData["OfficesIdMain"] = mainStorage.Id;
            }

            ViewData["OfficeSelect"] = officeId;
            var userId = _userManager.GetUserId (User);
            var officeIdEmployee = _context.Offices;

            /* #region Searching Manager */
            var officeIdEmployeeNew = _context.Offices;
            var userFromOffNow = _context.Users.Where (u => u.IsDelete == false);
            foreach (var usr in userFromOffNow) {
                if (await _userManager.IsInRoleAsync (usr, "Manager") && usr.Id == userId) // нашли юзера который залогинен и его роль
                {
                    var userOfficeId = usr.OfficeId;
                    foreach (var office in officeIdEmployee) {
                        if (userOfficeId == office.Id) {

                            ViewData["OfficeNameManager"] = usr.Name.ToString ();

                            var officesManager = _context.Offices.ToList ();

                            if (officeId.IsNullOrEmpty ()) {
                                var defaultOffice = officesManager
                                    .FirstOrDefault (o => o.Id == userOfficeId);
                                officeId = defaultOffice.Id;
                            }

                            List<Employee> employeesUser = new List<Employee> ();
                            var userFromOffUserId = _context.Users.Where (u => u.IsDelete == false);
                            foreach (var item in userFromOffUserId) {
                                if (await _userManager.IsInRoleAsync (item, "User")) {
                                    var userOfficeIdUser = item.OfficeId;
                                    foreach (var officeUser in officeIdEmployee) {
                                        if (userOfficeIdUser == officeUser.Id) {
                                            employeesUser.Add (item);
                                        }
                                    }
                                }

                            }

                            var categoryAssetsManager = _context.Assets
                                .Where (a => a.OfficeId == officeId)
                                .Include (a => a.Category)
                                .GroupBy (a => new { a.CategoryId, a.Category.Name })
                                .Select (g => new CategoryAssetCountViewModel {
                                    Id = g.Key.CategoryId,
                                        CategoryName = g.Key.Name,
                                        AssetsCount = g.Count ()
                                }).ToList ();

                            OfficeIndexViewModel modelManager = new OfficeIndexViewModel () {
                                CategoryAssetCountViewModels = categoryAssetsManager,
                                Employees = employeesUser,
                                Office = _context.Offices.FirstOrDefault (e => e.Id == officeId)
                            };

                            return View (modelManager);

                        }
                    }
                }
            }
            /* #endregion */

            #region Search office Manager

            var userName = _userManager.GetUserName (User);

            ViewData["UserId"] = userName;

            List<string> managers = new List<string> ();
            var userFromOff = _context.Users.Where (u => u.IsDelete == false);
            foreach (var usr in userFromOff) {
                if (await _userManager.IsInRoleAsync (usr, "User") && usr.Id == userId) // нашли юзера который залогинен и его роль
                {
                    ViewData["EmployeeToId"] = new SelectList (_context.Users.Where (u => u.Id == usr.Id), "Id", "Name");
                    var userOfficeId = usr.OfficeId;

                    foreach (var office in officeIdEmployee) {
                        if (userOfficeId == office.Id) {
                            foreach (var usrManager in userFromOff) {
                                if (await _userManager.IsInRoleAsync (usrManager, "Manager") && usrManager.OfficeId == userOfficeId) // нашли менеджера офиса к которому относиться данный сотрудник
                                {
                                    ViewData["EmployeeFromId"] = new SelectList (_context.Users.Where (u => u.Id == usrManager.Id), "Id", "Name");
                                    ViewData["OfficeId"] = new SelectList (_context.Offices.Where (o => o.Id == usr.OfficeId), "Id", "Title");

                                    ViewData["EmployeeFromIdName"] = usrManager.Name;
                                    ViewData["OfficeIdTitle"] = office.Title;
                                }
                            }

                        }
                    }

                }
            }

            #endregion

            /* #region Searching User */

            var userFromOffUser = _context.Users.Where (u => u.IsDelete == false);
            foreach (var usr in userFromOffUser) {
                if (await _userManager.IsInRoleAsync (usr, "User") && usr.Id == userId) // нашли юзера который залогинен и его роль
                {
                    var userOfficeId = usr.OfficeId;
                    foreach (var office in officeIdEmployee) {
                        if (userOfficeId == office.Id) {

                            ViewData["OfficeNameUser"] = usr.Name.ToString ();
                            ViewData["OfficeNameUserId"] = usr.Id;

                            var officesManager = _context.Offices.ToList ();

                            if (officeId.IsNullOrEmpty ()) {
                                var defaultOffice = officesManager
                                    .FirstOrDefault (o => o.Id == userOfficeId);
                                officeId = defaultOffice.Id;
                            }

                            var categoryAssetsManager = _context.Assets
                                .Where (a => a.OfficeId == officeId)
                                .Where (a => a.EmployeeId == usr.Id)
                                .Include (a => a.Category)
                                .GroupBy (a => new { a.CategoryId, a.Category.Name })
                                .Select (g => new CategoryAssetCountViewModel {
                                    Id = g.Key.CategoryId,
                                        CategoryName = g.Key.Name,
                                        AssetsCount = g.Count ()
                                }).ToList ();

                            OfficeIndexViewModel modelManager = new OfficeIndexViewModel () {
                                CategoryAssetCountViewModels = categoryAssetsManager,
                                Office = _context.Offices.FirstOrDefault (e => e.Id == officeId)
                            };

                            return View (modelManager);

                        }
                    }

                }
            }
            /* #endregion */

            /* #region Admin */
            var offices = _context.Offices.ToList ();

            if (officeId.IsNullOrEmpty ()) {
                var defaultOffice = offices
                    .FirstOrDefault ();
                if (defaultOffice == null) {
                    return View ();
                }
                officeId = defaultOffice.Id;
            }

            ViewData["Offices"] = new SelectList (offices.OrderByDescending (x => x.Title), "Id", "Title", officeId);
            List<Employee> employees = _context.Users.Where (e => e.OfficeId == officeId).ToList ();

            var categoryAssets = _context.Assets
                .Where (a => a.OfficeId == officeId)
                .Include (a => a.Category)
                .GroupBy (a => new { a.CategoryId, a.Category.Name })
                .Select (g => new CategoryAssetCountViewModel {
                    Id = g.Key.CategoryId,
                        CategoryName = g.Key.Name,
                        AssetsCount = g.Count ()
                }).ToList ();

            OfficeIndexViewModel model = new OfficeIndexViewModel () {
                CategoryAssetCountViewModels = categoryAssets,
                Employees = employees,
                Office = _context.Offices.FirstOrDefault (e => e.Id == officeId)
            };

            return View (model);
            /* #endregion */
        }

        public async Task<IActionResult> Details (string id) {
            if (id == null) {
                return NotFound ();
            }

            var office = await _context.Offices
                .SingleOrDefaultAsync (m => m.Id == id);
            if (office == null) {
                return NotFound ();
            }

            return View (office);
        }

        #endregion

        #region Create

        // GET: Offices/Create
        public IActionResult Create () {
            return View ();
        }

        // POST: Offices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create ([Bind ("Title,Id")] Office office) {
            if (ModelState.IsValid) {
                _context.Add (office);
                await _context.SaveChangesAsync ();

                Storage st = new Storage ();
                st.Name = $"Склад {office.Title}";
                st.IsMain = false;
                st.OfficeId = office.Id;

                _context.Storages.Add (st);

                office.StorageId = st.Id;
                _context.Update (office);

                await _context.SaveChangesAsync ();
                return RedirectToAction (nameof (Index));
            }
            return View (office);
        }

        #endregion

        #region Edit

        // GET: Offices/Edit/5
        public async Task<IActionResult> Edit (string id) {
            if (id == null) {
                return NotFound ();
            }
            ViewData["OfficeSelect"] = id;
            var office = await _context.Offices.SingleOrDefaultAsync (m => m.Id == id);
            if (office == null) {
                return NotFound ();
            }
            return View (office);
        }

        // POST: Offices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit (string id, [Bind ("Title,Id")] Office office) {
            if (id != office.Id) {
                return NotFound ();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update (office);
                    await _context.SaveChangesAsync ();
                } catch (DbUpdateConcurrencyException) {
                    if (!OfficeExists (office.Id)) {
                        return NotFound ();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction (nameof (Index));
            }
            return View (office);
        }

        #endregion

        #region Delete

        // GET: Offices/Delete/5
        public async Task<IActionResult> Delete (string id) {

            if (id == null) {
                return NotFound ();
            }
            ViewData["OfficeSelect"] = id;
            var office = await _context.Offices
                .SingleOrDefaultAsync (m => m.Id == id);
            if (office == null) {
                return NotFound ();
            }

            return View (office);
        }

        // POST: Offices/Delete/5
        [HttpPost, ActionName ("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed (string id) {
            var office = await _context.Offices.SingleOrDefaultAsync (m => m.Id == id);
            var storage = await _context.Storages.SingleOrDefaultAsync (s => s.OfficeId == id);

            if (storage != null) {
                _context.RemoveRange (storage, office);
                await _context.SaveChangesAsync ();
                return RedirectToAction (nameof (Index));
            } else {
                _context.Offices.Remove (office);
                await _context.SaveChangesAsync ();
                return RedirectToAction (nameof (Index));
            }

        }

        #endregion       

        #region OfficeExists

        private bool OfficeExists (string id) {
            return _context.Offices.Any (e => e.Id == id);
        }

        #endregion

        #region AddOfficeManager

        public async Task<IActionResult> SetManagerOffice (string officeId) {
            var currOffice = await _context.Offices.FindAsync (officeId);
            return View ();
        }

        #endregion
    }
}