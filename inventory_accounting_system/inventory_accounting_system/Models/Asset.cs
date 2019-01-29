using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace inventory_accounting_system.Models {
    public class Asset : Entity {
        [Required (ErrorMessage = "Название актива не должно быть пустым")]
        [StringLength (20, MinimumLength = 3, ErrorMessage = "Название актива не должно быть короче 3 символов и длиннее 20")]
        [RegularExpression ("^[а-яА-Яa-zA-Z0-9 -]*$", ErrorMessage = "Можно использовать только буквы и цифры")]
        [Display (Name = "Название")]
        public string Name { get; set; }

        [Display (Name = "Категория")]
        public string CategoryId { get; set; }
        public Category Category { get; set; }

        public string OfficeId { get; set; }

        [Display (Name = "Офис")]
        public Office Office { get; set; }

        public string EmployeeId { get; set; }

        [Display (Name = "Сотрудник")]
        public Employee Employee { get; set; }

        [Display (Name = "Инвентарный номер")]
        public string InventNumber { get; set; }
        
        [Display (Name = "Маска не может быть меньше 6")]
        public int InventPrefix { get; set; }

        [Display (Name = "Дата")]
        public DateTime Date { get; set; } = DateTime.Now;

        [Display (Name = "Поставщик")]
        public string SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        public string SerialNum { get; set; }

        [NotMapped]
        [Display (Name = "Изображение")]
        public IFormFile Image { get; set; }
        public string ImagePath { get; set; }

        [NotMapped]
        [Display (Name = "Технические документы")]
        public IFormFile Document { get; set; }

        [Display (Name = "Технические документы")]
        public string DocumentPath { get; set; }
        public string StatusMovingAssets { get; set; }

        [Display(Name = "Цена")]
        public double Price { get; set; }

        public IEnumerable<AssetsMoveStory> AssetsMoveStories { get; set; }
        public IEnumerable<Document> Documents { get; set; }

        public bool InStock { get; set; }

        public bool IsActive { get; set; }

        public IEnumerable<EventAsset> AssetEvents { get; set; }

    }
}