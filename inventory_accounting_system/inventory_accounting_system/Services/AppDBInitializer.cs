using inventory_accounting_system.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inventory_accounting_system.Services
{
    public class AppDBInitializer
    {
        public async Task SeedAsync(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Employee>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync("Admin"))
                {
                    IdentityRole roleAdmin = new IdentityRole("Admin");
                    await roleManager.CreateAsync(roleAdmin);
                }
                if (!await roleManager.RoleExistsAsync("User"))
                {
                    IdentityRole roleUser = new IdentityRole("User");
                    await roleManager.CreateAsync(roleUser);
                }

                if (!await roleManager.RoleExistsAsync("Manager"))
                {
                    IdentityRole roleManag = new IdentityRole("Manager");
                    await roleManager.CreateAsync(roleManag);
                }

                Employee admin = await userManager.FindByNameAsync("admin");
                if (admin == null)
                {
                    var user = new Employee
                    {
                        UserName = "admin", // login admin
                        Email = "admin@admin.com",
                        Login = "admin"
                    };
                    var result = await userManager.CreateAsync(user, "admin"); // pass: admin
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "Admin");
                    }

                }
            }
        }
    }
}
