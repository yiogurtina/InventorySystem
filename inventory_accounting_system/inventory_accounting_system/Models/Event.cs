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
        public string Content { get; set; }
        public string Periodicity { get; set; }
        public IEnumerable<EventAsset> EventAssets { get; set; }
    }
}
