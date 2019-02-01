using inventory_accounting_system.Data;
using inventory_accounting_system.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inventory_accounting_system
{
    public class DataSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            if (!context.Categories.Any())
            {
                var categories = new List<Category>
                        {
                            new Category { Name = "Компьютеры",Prefix="001" },
                            new Category { Name = "Кондиционеры",Prefix="002" },
                            new Category { Name = "Мебель",Prefix="003" },
                            new Category { Name = "Авто" ,Prefix="004"}
                        };
                context.AddRange(categories);
                context.SaveChanges();
            }

            if (!context.Suppliers.Any())
            {
                var suppliers = new List<Supplier>
                        {
                            new Supplier { Name = "Самсунг"},
                            new Supplier { Name = "Intel"},
                            new Supplier { Name = "Panasonic"},
                            new Supplier { Name = "ОАО Мебель"},
                            new Supplier { Name = "Sony"}

                        };
                context.AddRange(suppliers);
                context.SaveChanges();
            }



            if (!context.Offices.Any(b => b.IsMain))
            {
                var storages = new List<Office>
                        {
                            new Office { Title = "Главный склад", IsMain=true},
                            new Office { Title = "Офис 1", IsMain=false},
                            new Office { Title = "Офис 2", IsMain=false},
                            new Office { Title = "Офис 3", IsMain=false},
                            new Office { Title = "Офис 4", IsMain=false}

                        };
                context.AddRange(storages);
                context.SaveChanges();
            }


        }


    }

    public static class IdentityDataInit
    {
        public static void SeedData(
            UserManager<Employee> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);

        }

        public static void SeedUsers(UserManager<Employee> userManager)
        {

            if (userManager.FindByNameAsync("admin").Result == null)
            {
                Employee user = new Employee(){
                    Id= Guid.NewGuid().ToString(),
                    Name = "admin",
                    Surname = "admin",
                    UserName = "admin", 
                    Email = "admin@admin.com",
                    Login = "admin"
                };

                var result = userManager.CreateAsync(user, "admin").Result;  //Password

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,"admin").Wait();
                }
            }

        }

        public static void SeedRoles
            (RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }


            if (!roleManager.RoleExistsAsync("Manager").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Manager";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("User").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "User";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }
    }
}
