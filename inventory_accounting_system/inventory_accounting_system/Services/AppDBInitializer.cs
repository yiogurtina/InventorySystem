using inventory_accounting_system.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using inventory_accounting_system.Data;

namespace inventory_accounting_system.Services
{
    public class AppDBInitializer
    {
        private string UserId;
        public async Task SeedAsync(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Employee>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
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
                        Name = "admin",
                        Surname = "admin",
                        UserName = "admin", 
                        Email = "admin@admin.com",
                        Login = "admin"
                    };
                    UserId = user.Id;
                    var result = await userManager.CreateAsync(user, "admin");

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "Admin");
                    }
                }
                var storage = context.Storages.FirstOrDefault(b => b.IsMain);
                if (storage == null)
                {
                     context.Storages.Add(new Storage()
                     {
                         Id = Guid.NewGuid().ToString(),
                         Name = "Main storage",
                         IsMain = true,
                         OwnerId = UserId
                        });
                }
                context.SaveChanges();

            }
        }
    }
}
