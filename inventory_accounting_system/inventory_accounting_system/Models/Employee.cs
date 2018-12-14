using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace inventory_accounting_system.Models
{
    public class Employee : IdentityUser
    {
        public string Login { get; set; }
    }
}
