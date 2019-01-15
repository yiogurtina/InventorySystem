using System;
using System.ComponentModel.DataAnnotations;

namespace inventory_accounting_system.Models
{
    public class OrderEmployee : Entity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime DateFrom { get; set; } = DateTime.Now;

        public string OfficeId { get; set; }
        [Display(Name = "Офис")]
        public Office Office { get; set; }

        public string EmployeeId { get; set; }
        [Display(Name = "Сотрудник")]
        public Employee Employee { get; set; }

        public string Status { get; set; } = "New";
    }
}