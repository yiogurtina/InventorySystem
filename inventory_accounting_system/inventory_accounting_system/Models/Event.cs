using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace inventory_accounting_system.Models
{
    public class Event : Entity
    {
        public string Title { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string TimeLast { get; set; }
        public Category Category { get; set; }
        [Display(Name = "Категория")]
        public string CategoryId { get; set; }
    }
}
