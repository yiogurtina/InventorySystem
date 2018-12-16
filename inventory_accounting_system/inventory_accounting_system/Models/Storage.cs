using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace inventory_accounting_system.Models
{
    public class Storage : Entity
    {
        [Display(Name = "Название")] public string Title { get; set; }
        [Display(Name = "Имущество")] public IEnumerable<Asset> Assets { get; set; }
        [Display(Name = "Поставщики")] public IEnumerable<Supplier> Suppliers { get; set; }
        public string EmployeeId { get; set; }
        [Display(Name = "Сотрудник")] public Employee Employee { get; set; }
    }
}