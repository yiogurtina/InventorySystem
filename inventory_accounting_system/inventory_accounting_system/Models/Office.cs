using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace inventory_accounting_system.Models
{
    public class Office : Entity
    {
        [Required(ErrorMessage = "Название офиса не должно быть пустым")]
        [StringLength(20, MinimumLength = 3, ErrorMessage="Название офиса не должно быть короче 3 символов и длиннее 20")]
        [RegularExpression("^[а-яА-Яa-zA-Z0-9]*$", ErrorMessage = "Можно использовать только буквы и цифры")]
        [Display(Name = "Название")]
        public string Title { get; set; }

        [Display(Name = "Сотрудники")]
        public IEnumerable<Employee> Employees { get; set; }

        [Display(Name = "Имущество")]
        public IEnumerable<Asset> Assets { get; set; }

    }
}
