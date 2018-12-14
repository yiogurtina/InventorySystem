using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inventory_accounting_system.Models
{
    public class Entity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
    }
}
