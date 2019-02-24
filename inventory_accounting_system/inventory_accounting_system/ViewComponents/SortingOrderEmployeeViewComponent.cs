using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using inventory_accounting_system.Data;
using inventory_accounting_system.Models;
using inventory_accounting_system.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace inventory_accounting_system.ViewComponents {
    public class SortingOrderEmployeeViewComponent : ViewComponent {
        #region Dependency Injection

        private readonly ApplicationDbContext _context;
        private readonly UserManager<Employee> _userManager;

        public SortingOrderEmployeeViewComponent (ApplicationDbContext context, UserManager<Employee> userManager) {
            _context = context;
            _userManager = userManager;
        }
        #endregion

        public IViewComponentResult Invoke () {

            var userName = _userManager.GetUserName (Request.HttpContext.User);

            ViewData["UserId"] = userName;

            var efNull = _context.OrderEmployees.Where (f => f.EmployeeFromId == null);

            var empOrders = _context.OrderEmployees
                .Include (a => a.Asset)
                .Include (a => a.Office)
                .Include (a => a.EmployeeFrom)
                .Include (a => a.EmployeeTo)
                .Where (e => e.Status == "New")
                .GroupBy (e => new { e.EmployeeFromId, e.EmployeeFrom.Name, e.Status })
                .Select (g => new SotringEmployeeOrderViewModel {
                    Id = g.Key.EmployeeFromId,
                        SendFromName = g.Key.Name,
                        StatusVM = g.Key.Status,
                        OrderCount = g.Count ()
                }).ToList ();

            EmployeeOrderViewModel empOrderVM = new EmployeeOrderViewModel () {
                SotringEmployeeOrderViewModel = empOrders
            };

            return View (empOrderVM);
        }
    }
}