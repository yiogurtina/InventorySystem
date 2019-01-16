using System.Collections.Generic;
using System.Linq;
using inventory_accounting_system.Data;
using inventory_accounting_system.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace inventory_accounting_system.ViewModel
{
    public class OrderEmployeeVM : ViewComponent
    {
        #region Dependency Injection

        private readonly ApplicationDbContext _context;
        private readonly UserManager<Employee> _userManager;

        public OrderEmployeeVM(ApplicationDbContext context, UserManager<Employee> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        #endregion

        public IEnumerable<OrderMessageViewModel> OrderMessageViewModels { get; set; }
        public IEnumerable<OrderEmployee> OrderEmployees { get; set; }


        public string Invoke()
        {
            var orderMessage = _context.OrderEmployees
                .Where(s => s.Status == "New")
                .GroupBy(a => new { a.Id})
                .Select(g => new OrderMessageViewModel
                {
                    MessageCount = g.Count()
                }).ToList();

            return orderMessage.Count.ToString();
        }
    }

    public class OrderMessageViewModel
    {
        public int MessageCount { get; set; }
    }

    
}