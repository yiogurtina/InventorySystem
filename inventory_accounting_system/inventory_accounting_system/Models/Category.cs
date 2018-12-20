using System;
using System.ComponentModel.DataAnnotations;

namespace inventory_accounting_system.Models
{
    public class Category : Entity
    {
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "Префикс")]
        public string Prefix { get; set; }

        public Event Event { get; set; }
        [Display(Name = "Событие")]
        public string EventId { get; set; }
    }
}