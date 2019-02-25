using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace inventory_accounting_system.Models {
    public class Employee : IdentityUser {

        [Required (ErrorMessage = "Логин не должно быть пустым")]
        [StringLength (20, MinimumLength = 3, ErrorMessage = "Логинка пользователя не должна быть короче 3 символов и длиннее 20")]
        [Display (Name = "Пользователь")]
        public string Login { get; set; }

        public Office Office { get; set; }

        [Display (Name = "Офис")]
        public string OfficeId { get; set; }

        [Required (ErrorMessage = "Строчка Имя не должна быть пустой")]
        [StringLength (20, MinimumLength = 3, ErrorMessage = "Имя пользователя не должно быть короче 3 символов и длиннее 20")]
        [Display (Name = "Имя")]
        public string Name { get; set; }

        [Required (ErrorMessage = "Строчка Фамилия не должна быть пустой")]
        [StringLength (20, MinimumLength = 2, ErrorMessage = "Фамилия пользователя не должна быть короче 2 символов и длиннее 20")]
        [Display (Name = "Фамилия")]
        public string Surname { get; set; }
        public string Number { get; set; }
        public IEnumerable<Asset> Assets { get; set; }
        public IEnumerable<EventAsset> EventAssets { get; set; }
        public IEnumerable<AssetsMoveStory> assetsMoveStoriesFrom { get; set; }
        public IEnumerable<AssetsMoveStory> assetsMoveStoriesTo { get; set; }
        public bool IsDelete { get; set; }

    }
}