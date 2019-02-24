using System;
using System.Collections.Generic;
using inventory_accounting_system.Models;

namespace inventory_accounting_system.ViewModel {
    public class EmployeeOrderViewModel {
        public IEnumerable<SotringEmployeeOrderViewModel> SotringEmployeeOrderViewModel { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
        public IEnumerable<OrderEmployee> OrderEmployee { get; set; }
    }
    public class SotringEmployeeOrderViewModel {
        public string Id { get; set; }
        public string SendFromName { get; set; }
        public string EmployeeFromId { get; set; }
        public string TitleSort { get; set; }
        public string OfficesVM { get; set; }
        public string AssetsImagePath { get; set; }
        public Asset AssetVM { get; set; }
        public DateTime DateFromVM { get; set; }
        public DateTime? DateToVM { get; set; }
        public string StatusVM { get; set; }
        public string ContentVM { get; set; }
        public int OrderCount { get; set; }
    }
}