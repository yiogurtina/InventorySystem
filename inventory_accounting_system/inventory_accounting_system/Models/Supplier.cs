using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inventory_accounting_system.Models
{
    public class Supplier : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
