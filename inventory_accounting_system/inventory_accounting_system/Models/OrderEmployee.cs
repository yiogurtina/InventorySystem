using System;
using System.ComponentModel.DataAnnotations;

namespace inventory_accounting_system.Models
{
    public class OrderEmployee : Entity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime DateFrom { get; set; } = DateTime.Now;
        public DateTime? DateTo { get; set; } = DateTime.Now;

        public string OfficeId { get; set; }
        [Display(Name = "Офис")]
        public Office Office { get; set; }

        public string EmployeeFromId { get; set; }
        [Display(Name = "Сотрудник")]
        public Employee EmployeeFrom { get; set; }

        public string EmployeeToId { get; set; }
        [Display(Name = "Сотрудник")]
        public Employee EmployeeTo { get; set; }

        public string Status { get; set; } = "New";
    }
}