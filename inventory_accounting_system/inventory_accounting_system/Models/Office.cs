using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace inventory_accounting_system.Models
{
    public class Office : Entity
    {
        [Display(Name = "Название")]
        public string Title { get; set; }

        [Display(Name = "Сотрудники")]
        public IEnumerable<Employee> Employees { get; set; }

        [Display(Name = "Имущество")]
        public IEnumerable<Asset> Assets { get; set; }

        [Display(Name = "Тип")]
        public string OfficeTypeId { get; set; }
        public OfficeType OfficeType { get; set; }

    }
}
