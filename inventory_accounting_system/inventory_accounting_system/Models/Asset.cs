using System;

namespace inventory_accounting_system.Models
{
    public class Asset
    {
        public string Name { get;set; }
        public string CategoryId { get; set; }
        public Category  Category{ get; set; }
        public string InventNumber { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

//        public string OfficesId { get; set; }
//        public Offices Offices { get; set; }
//
//        public string StorageId { get; set; }
//        public Storage Storage { get; set; }
//
        public string SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public string EmployeeId{ get; set; }  
        public Employee Employee { get; set; }
        public string ImagesUrl { get; set; }
//        public string EventId { get; set; }
//        public Event Event { get; set; }


    }
}