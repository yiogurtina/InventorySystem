using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace inventory_accounting_system.Models
{
    public class Supplier : Entity
    {
        [Display(Name = "Имя")] public string Name { get; set; }
        [Display(Name = "Описание")] public string Description { get; set; }
    }
}
