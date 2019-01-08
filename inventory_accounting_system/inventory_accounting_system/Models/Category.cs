using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace inventory_accounting_system.Models
{
    public class Category : Entity
    {
        [Required(ErrorMessage = "Название категории не должно быть пустым")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Название категории не должно быть короче 3 символов и длиннее 20")]
        [RegularExpression("^[а-яА-Яa-zA-Z0-9]*$", ErrorMessage = "Можно использовать только буквы и цифры")]
        [Display(Name = "Название")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Строка префикс не должно быть пустым")]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "Префикс категории не должен быть короче 2 символов и длиннее 10")]
        [Display(Name = "Префикс")]
        public string Prefix { get; set; }

    }
}
