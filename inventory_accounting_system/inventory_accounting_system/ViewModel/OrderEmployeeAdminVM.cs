using System.Collections.Generic;
using System.Linq;
using inventory_accounting_system.Data;
using inventory_accounting_system.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace inventory_accounting_system.ViewModel {
    public class OrderEmployeeAdminVM : ViewComponent {
        #region Dependency Injection

        private readonly ApplicationDbContext _context;
        private readonly UserManager<Employee> _userManager;

        public OrderEmployeeAdminVM (ApplicationDbContext context, UserManager<Employee> userManager) {
            _context = context;
            _userManager = userManager;
        }

        #endregion

        public IEnumerable<OrderMessageAdminViewModel> OrderMessageAdminViewModels { get; set; }

        public string Invoke () {
            var orderMessage = _context.OrderEmployeeAdmins
                .Where (s => s.StatusAdmin == "New")
                .GroupBy (a => new { a.Id })
                .Select (g => new OrderMessageViewModel {
                    MessageCount = g.Count ()
                }).ToList ();

            return orderMessage.Count.ToString ();
        }

        public class OrderMessageAdminViewModel {
            public int MessageCountAdmin { get; set; }
        }

        public class OrderWatchViewModel {
            public OrderEmployeeAdmin OrderAdmin { get; set; }
            public IEnumerable<OrderEmployeeAdmin> OrderAdminsIEn { get; set; }
        }
    }
}