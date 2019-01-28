using System;

namespace inventory_accounting_system.Models
{
    public class InventoryNumberHistory : Entity
    {
        public DateTime CreateDate { get; set; }
        public DateTime? ChangeDate { get; set; }
        public string Been { get; set; }
        public string Become { get; set; }
        public string AssetIdCreate { get; set; }   
    }
}   