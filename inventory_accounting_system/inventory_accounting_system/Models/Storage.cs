using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inventory_accounting_system.Models
{
    public class Storage : Entity
    {
        public string Name { get; set; }
        public IEnumerable<Asset> Assets { get; set; }
        public Employee Owner { get; set; }
        public string OwnerId { get; set; }
        public bool IsMain { get; set; }
    }
}
