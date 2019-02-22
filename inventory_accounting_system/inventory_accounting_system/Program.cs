using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using inventory_accounting_system.Data;
using inventory_accounting_system.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace inventory_accounting_system
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetService<ApplicationDbContext>();
                    var userManager = services.GetRequiredService<UserManager<Employee>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                    IdentityDataInit.SeedData(userManager, roleManager);

                    DataSeeder.Seed(context);
                    // DataSeeder.SeedUsersOffices(userManager);
                }
                catch
                {
                    //Обработка ошибки
                }
            }
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
