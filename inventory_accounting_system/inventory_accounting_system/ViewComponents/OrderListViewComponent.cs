using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using inventory_accounting_system.Data;
using inventory_accounting_system.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace inventory_accounting_system.ViewComponents {
    public class OrderListViewComponent : ViewComponent {
        #region Dependency Injection

        private readonly ApplicationDbContext _context;
        private readonly UserManager<Employee> _userManager;

        public OrderListViewComponent (ApplicationDbContext context, UserManager<Employee> userManager) {
            _context = context;
            _userManager = userManager;
        }
        #endregion

        public IViewComponentResult Invoke () {

            var userName = _userManager.GetUserName (Request.HttpContext.User);

            ViewData["UserId"] = userName;

            var efNull = _context.OrderEmployees.Where (f => f.EmployeeFromId == null);
            
            
            IQueryable<OrderEmployee> empOrders = _context.OrderEmployees
                .Include (a => a.Asset)
                .Include (a => a.Office)
                .Include (a => a.EmployeeFrom)
                .Include (a => a.EmployeeTo)
                .Where (e => e.Status == "New");

            return View (empOrders);

        }

    }

}