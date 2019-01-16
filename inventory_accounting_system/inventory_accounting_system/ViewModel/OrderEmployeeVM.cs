using System.Collections.Generic;
using inventory_accounting_system.Models;

namespace inventory_accounting_system.ViewModel
{
    public class OrderEmployeeVM
    {
        public IEnumerable<OrderMessageViewModel> OrderMessageViewModels { get; set; }
        public IEnumerable<OrderEmployee> OrderEmployees { get; set; }
    }

    public class OrderMessageViewModel
    {
        public int MessageCount { get; set; }
    }
}