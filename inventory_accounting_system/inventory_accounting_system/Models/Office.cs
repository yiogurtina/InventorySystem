using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inventory_accounting_system.Models
{
    public class Office : Entity
    {
        public string Title { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
        public IEnumerable<Asset> Assets { get; set; }

    }
}
