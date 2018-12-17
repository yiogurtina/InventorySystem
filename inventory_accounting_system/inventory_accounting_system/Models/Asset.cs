using System;
using System.ComponentModel.DataAnnotations;

namespace inventory_accounting_system.Models
{
    public class Asset : Entity
    {
        [Display(Name = "Название")] public string Name { get; set; }
        public string CategoryId { get; set; }
        [Display(Name = "Категория")] public Category Category { get; set; }
        [Display(Name = "Инвентарный номер")] public string InventNumber { get; set; }
        [Display(Name = "Инвентарный префикс")] public string InventPrefix { get; set; }
        [Display(Name = "Дата")] public DateTime Date { get; set; } = DateTime.Now;

        public string OfficeId { get; set; }
        [Display(Name = "Офис")] public Office Office { get; set; }

        public string StorageId { get; set; }
        [Display(Name = "Склад")] public Storage Storage { get; set; }

        public string SupplierId { get; set; }
        [Display(Name = "Пастовщик")] public Supplier Supplier { get; set; }
        public string EmployeeId { get; set; }
        [Display(Name = "Сотрудник")] public Employee Employee { get; set; }

        [Display(Name = "Изображение")] public string ImagesUrl { get; set; }

        //        public string EventId { get; set; }
        //        public Event Event { get; set; }
        public string SerialNum { get; set; }
    }
}