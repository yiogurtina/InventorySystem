using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inventory_accounting_system.Models
{
    public class EventType : Entity
    {
        public string Type { get; set; }
        public string Description { get; set; }
    }
}
