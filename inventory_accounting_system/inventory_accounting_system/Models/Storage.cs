using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace inventory_accounting_system.Models
{
    public class Storage : Entity
    {
        public string Name { get; set; }
        public IEnumerable<Asset> Assets { get; set; }
        public Employee Owner { get; set; }
        public string OwnerId { get; set; }

        [ForeignKey("Office")]
        public string OfficeId { get; set; }
        public Office Office { get; set; }
        


        public bool IsMain { get; set; }
    }
}
