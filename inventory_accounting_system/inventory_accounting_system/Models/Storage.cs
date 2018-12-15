using System.Collections.Generic;

namespace inventory_accounting_system.Models
{
    public class Storage : Entity
    {
        public string Title { get; set; }
        public IEnumerable<Asset> Assets { get; set; }
        public IEnumerable<Supplier> Suppliers { get; set; }
        public string EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}