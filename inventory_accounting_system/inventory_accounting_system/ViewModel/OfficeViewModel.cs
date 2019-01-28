using inventory_accounting_system.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inventory_accounting_system.ViewModel
{
    public class OfficeIndexViewModel
    {
        //public Employee Manager { get; set; }
        public IEnumerable<CategoryAssetCountViewModel> CategoryAssetCountViewModels { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
        public Office Office { get; set; }
    }

    public class CategoryAssetCountViewModel
    {
        public string Id { get; set; }
        public string CategoryName { get; set; }
        public int AssetsCount { get; set; }
    }
}
