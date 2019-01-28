using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using inventory_accounting_system.Data;
using inventory_accounting_system.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;

namespace inventory_accounting_system.ViewComponents {
    public class AssetsListViewComponent : ViewComponent {

        #region Dependency Injection

        private readonly ApplicationDbContext _context;
        private readonly UserManager<Employee> _userManager;

        public AssetsListViewComponent (ApplicationDbContext context, UserManager<Employee> userManager) {
            _context = context;
            _userManager = userManager;
        }
        #endregion

        public async Task<IViewComponentResult> InvokeAsync (SelectList officeId, SelectList userId) {

            // Transfer officeId, EmployeeId and UserId to ViewComponent from Index/Office

            #region Search office Manager
            var userIdUser = _userManager.GetUserId (Request.HttpContext.User);
            var officeIdEmployee = _context.Offices;
            var userName = _userManager.GetUserName (Request.HttpContext.User);

            ViewData["UserId"] = userName;

            List<string> managers = new List<string> ();
            var userFromOff = _context.Users.Where (u => u.IsDelete == false);
            foreach (var usr in userFromOff) {
                if (await _userManager.IsInRoleAsync (usr, "User") && usr.Id == userIdUser) // нашли юзера который залогинен и его роль
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

            /* #region Admin */
            string officeIdS = string.Empty;
            string userIdS = string.Empty;
            var officeAll = _context.Offices;

            foreach (var item in officeId) {
                officeIdS = item.Value;
            }

            foreach (var item in userId) {

                userIdS = item.Value;
            }

            var mainStorage = _context.Offices.FirstOrDefault (s => s.IsMain);
            var assets = _context.Assets
                .Include (a => a.Category)
                .Include (a => a.Supplier)
                .Include (a => a.Office)
                .Where (a => a.IsActive)
                .Where (a => a.InStock)
                .Where (a => a.OfficeId == mainStorage.Id);

            return View (assets.ToList ());

            /* #endregion */

        }
    }
}