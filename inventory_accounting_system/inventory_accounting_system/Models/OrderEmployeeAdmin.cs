using System;
using System.ComponentModel.DataAnnotations;

namespace inventory_accounting_system.Models
{
    public class OrderEmployeeAdmin : Entity
    {
        public string TitleAdmin { get; set; }
        public string ContentUser { get; set; }
        public string ContentAdmin { get; set; }
        public DateTime DateFromAdmin { get; set; } = DateTime.Now;
        public DateTime? DateToAdmin { get; set; } = DateTime.Now;

        public string OfficeAdminId { get; set; }
        [Display(Name = "Офис")]
        public Office OfficeAdmin { get; set; }

        public string EmployeeFromAdminId { get; set; }
        [Display(Name = "Сотрудник")]
        public Employee EmployeeFromAdmin { get; set; }

        public string EmployeeToAdminId { get; set; }
        [Display(Name = "Сотрудник")]
        public Employee EmployeeToAdmin { get; set; }

        public string OrderEmployeeId { get; set; }
        public OrderEmployee OrderEmployee { get; set; }

        public string StatusAdmin { get; set; } = "New";
    }
}