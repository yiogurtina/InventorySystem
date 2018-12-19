using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace inventory_accounting_system.Models
{
    public class AssetsMoveStory : Entity
    {

        public string AssetId { get; set; }
        public Asset Asset { get; set; }

        public string OfficeId { get; set; }
        [Display(Name = "Офис")]
        public Office Office { get; set; }

        public string EmployeeId { get; set; }
        [Display(Name = "Сотрудник")]
        public Employee Employee { get; set; }

        [Display(Name = "Дата начало")]
        public DateTime DateStart { get; set; }

        [Display(Name = "Дата конец")]
        public DateTime DateEnd { get; set; }

        
    }
}
