using inventory_accounting_system.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace inventory_accounting_system.ViewModel
{
    public class MoveViewModel
    {
        public Asset Asset { get; set; }
        public AssetsMoveStory AssetsMoveStory { get; set; }
        public IEnumerable<AssetsMoveStory> AssetsMoveStories { get; set; }
    }

    public class DetailsAssetViewModel
    {
        public Asset Asset { get; set; }
        public IEnumerable<Document> Documents { get; set; }
        public IEnumerable<AssetsMoveStory> AssetsMoveStories { get; set; }
    }

    public class EditAssetViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }

        [Display(Name = "Категория")]
        public string CategoryId { get; set; }

        [Display(Name = "Офис")]
        public string OfficeId { get; set; }


        [Display(Name = "Сотрудник")]
        public string EmployeeId { get; set; }


        [Display(Name = "Инвентарный номер")]
        [Remote(action: "ValidateInventNumber", controller: "Validation", AdditionalFields = "Id", ErrorMessage = "Такой номер уже существует")]
        public string InventNumber { get; set; }
        public string OldInventNumber { get; set; }


        [Display(Name = "Маска не может быть меньше 6")]
        public int InventPrefix { get; set; }

        [Display(Name = "Дата")]
        public DateTime Date { get; set; } = DateTime.Now;

        [Display(Name = "Поставщик")]
        public string SupplierId { get; set; }

        public string SerialNum { get; set; }

        public IFormFile Image { get; set; }
        public string ImagePath { get; set; }

        [Display(Name = "Цена")]
        public double Price { get; set; }

        public bool InStock { get; set; }

        public bool IsActive { get; set; }
    }
}
