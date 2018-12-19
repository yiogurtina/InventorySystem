using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace inventory_accounting_system.Models
{
    public class Asset : Entity
    {
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "Категория")]
        public string CategoryId { get; set; }
        public Category Category { get; set; }

        [Display(Name = "Инвентарный номер")]
        public string InventNumber { get; set; }

        [Display(Name = "Инвентарный префикс")]
        public string InventPrefix { get; set; }

        [Display(Name = "Дата")]
        public DateTime Date { get; set; } = DateTime.Now;

        [Display(Name = "Поставщик")]
        public string SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        public string SerialNum { get; set; }

        [NotMapped]
        [Display(Name = "Изображение")]
        public IFormFile Image { get; set; }
        public string ImagePath { get; set; }

        public IEnumerable<AssetsMoveStory> AssetsMoveStories { get; set; }

        public bool IsActive { get; set; }
    }
}