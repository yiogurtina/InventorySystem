using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace inventory_accounting_system.Models
{
    // Add profile data for application users by adding properties to the Employee class
    public class Employee : IdentityUser
    {
        public string Login { get; set; }
    }
}
