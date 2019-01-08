using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace inventory_accounting_system.Models
{
    public class Supplier : Entity
    {
        [Required(ErrorMessage = "Название Поставщика не должно быть пустым")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Название Поставщика не должно быть короче 3 символов и длиннее 20")]
        [RegularExpression("^[а-яА-Яa-zA-Z0-9]*$", ErrorMessage = "Можно использовать только буквы и цифры")]
        [Display(Name = "Имя")]
        public string Name { get; set; }


        [Display(Name = "Описание")]
        public string Description { get; set; }
    }
}
