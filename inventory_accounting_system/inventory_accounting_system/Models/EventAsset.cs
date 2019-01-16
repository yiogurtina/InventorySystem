using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inventory_accounting_system.Models
{
    public class EventAsset : Entity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime DeadLine { get; set; }
        public string AssetId { get; set; }
        public Asset Asset { get; set; }
        public string EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public string EventId { get; set; }
        public Event Event { get; set; }
        public int Period { get; set; }

    }
}
