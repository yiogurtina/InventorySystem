using System;
using System.ComponentModel.DataAnnotations;

namespace inventory_accounting_system.Models
{
    public class Category : Entity
    {
        public string Name { get; set; }
        public string Prefix { get; set; }
    }
}