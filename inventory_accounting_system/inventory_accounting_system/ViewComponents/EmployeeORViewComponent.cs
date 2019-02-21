using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using inventory_accounting_system.Data;
using inventory_accounting_system.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace inventoryaccountingsystem.inventoryaccountingsystem.ViewComponents {

    public class EmployeeORViewComponent : ViewComponent {

        #region Dependency Injection

        private readonly ApplicationDbContext _context;
        private readonly UserManager<Employee> _userManager;

        public EmployeeORViewComponent (ApplicationDbContext context, UserManager<Employee> userManager) {
            _context = context;
            _userManager = userManager;
        }
        #endregion

        public IViewComponentResult Invoke (string employeeId) {

            IQueryable<Asset> empId = _context.Assets
                .Include (a => a.Category)
                .Include (a => a.Office)
                .Include (a => a.Employee)
                .Include (a => a.Supplier);

            if (employeeId == null) {
                return View ("Assets", "ReportOnStockChoice");
            }

            empId = empId.Where (a => a.EmployeeId == employeeId);

            var resultEmp = _context.Users.FirstOrDefault (u => u.Id == employeeId);

            ViewData["EmployeeName"] = resultEmp.Name.ToString ();

            var officeIdEmployee = _context.Offices.Where (a => a.Id == resultEmp.OfficeId).FirstOrDefault ();
            ViewData["EmployeeIdOfficeReport"] = officeIdEmployee.Id;

            return View (empId);

        }

    }
}