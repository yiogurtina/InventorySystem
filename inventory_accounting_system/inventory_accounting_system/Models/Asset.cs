using System;

namespace inventory_accounting_system.Models
{
    public class Asset : Entity
    {
        public string Name { get;set; }
        public string CategoryId { get; set; }
        public Category  Category{ get; set; }
        public string InventNumber { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        public string OfficeId { get; set; }
        public Office Office { get; set; }

        public string StorageId { get; set; }
        public Storage Storage { get; set; }
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