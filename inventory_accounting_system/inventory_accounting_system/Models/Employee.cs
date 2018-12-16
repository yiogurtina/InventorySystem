using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace inventory_accounting_system.Models
{
    public class Employee : IdentityUser
    {
        [Display(Name = "Имя")] public string Login { get; set; }
    }
}
