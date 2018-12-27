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
        public Office Office { get; set; }
        [Display(Name="Оффис")]public string OfficeId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Number { get; set; }
        public IEnumerable<Asset> Assets { get; set; }
        public bool IsDelete { get; set; }

    }
}
