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
    public class OrderListAdminViewComponent : ViewComponent {
        #region Dependency Injection

        private readonly ApplicationDbContext _context;
        private readonly UserManager<Employee> _userManager;

        public OrderListAdminViewComponent (ApplicationDbContext context, UserManager<Employee> userManager) {
            _context = context;
            _userManager = userManager;
        }
        #endregion

        public IViewComponentResult Invoke () {

            IQueryable<OrderEmployeeAdmin> empOrdersAdmin = _context.OrderEmployeeAdmins
                .Include (a => a.OfficeAdmin)
                .Include (a => a.EmployeeFromAdmin)
                .Include (a => a.EmployeeToAdmin)
                .Where (e => e.StatusAdmin == "New");

            return View (empOrdersAdmin);

        }

    }

}