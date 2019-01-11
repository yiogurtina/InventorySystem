using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inventory_accounting_system.Models
{
    public class AssetOnStorage : Entity
    {
        public string AssetId { get; set; }
        public Asset Asset { get; set; }

        public string EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public string StorageId { get; set; }
        public Storage Storage { get; set; }
    }
}
