using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inventory_accounting_system.Models
{
    public class EventAsset : Entity
    {
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime DeadLine { get; set; }
        public string AssetId { get; set; }
        public Asset Asset { get; set; }
        public int DaysCountBeforeAlert { get; set; }
    }
}
