using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inventory_accounting_system.Models
{
    public class Event : Entity
    {
        public string Title { get; set; }
        public Category Category { get; set; }
        public string CategoryId { get; set; }
    }
}
