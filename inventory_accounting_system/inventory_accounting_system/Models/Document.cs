using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inventory_accounting_system.Models
{
    public class Document : Entity
    {
        public string AssetId { get; set; }
        public Asset Asset { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
    }
}
