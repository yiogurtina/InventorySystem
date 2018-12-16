using System;
using System.ComponentModel.DataAnnotations;

namespace inventory_accounting_system.Models
{
    public class Category : Entity
    {
        [Display(Name = "Название")] public string Name { get; set; }
        [Display(Name = "Префикс")] public string Prefix { get; set; }
    }
}