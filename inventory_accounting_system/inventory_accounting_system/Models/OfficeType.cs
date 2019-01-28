using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace inventory_accounting_system.Models
{
    public class OfficeType:Entity
    {
        [Display(Name = "Название")]
        public string Name { get; set; }
    }
}
