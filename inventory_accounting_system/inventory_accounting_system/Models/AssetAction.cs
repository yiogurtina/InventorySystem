using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace inventory_accounting_system.Models
{
    public class AssetAction : Entity
    {

        public string AssetId { get; set; }
        public Asset Asset { get; set; }

        public string StorageFromId { get; set; }
        [Display(Name = "Офис/Склад от")]
        public Storage StorageFrom { get; set; }

        public string StorageToId { get; set; }
        [Display(Name = "Офис/Склад куда")]
        public Storage StorageTo { get; set; }

        public string EmployeeFromId { get; set; }
        [Display(Name = "Сотрудник от")]
        public Employee EmployeeFrom { get; set; }

        public string EmployeeToId { get; set; }
        [Display(Name = "Сотрудник кому")]
        public Employee EmployeeTo { get; set; }

        [Display(Name = "Дата начало")]
        public DateTime DateStart { get; set; }

        [Display(Name = "Дата конец")]
        public DateTime DateEnd { get; set; }


    }
}
