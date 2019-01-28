using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace inventory_accounting_system.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Строка Логин не должна быть пустой")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Логинка пользователя не должна быть короче 3 символов и длиннее 20")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Електронный почтовый адрес не должен быть пустым")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

//        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public Office Office { get; set; }
        [Required (ErrorMessage = "Выберите офис")]
        [Display(Name = "Офис")]
        public string OfficeId { get; set; }

        [Required(ErrorMessage = "Строка Имя не должна быть пустой")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Имя не должно быть короче 3 символов и длиннее 20")]
        [RegularExpression("^[а-яА-Яa-zA-Z]*$", ErrorMessage = "Можно использовать только буквы")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Строка Фамилия не должна быть пустой")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Фамилия не должна быть короче 3 символов и длиннее 20")]
        [RegularExpression("^[а-яА-Яa-zA-Z]*$", ErrorMessage = "Можно использовать только буквы")]
        public string Surname { get; set; }
        public string Number { get; set; }
    }
}
