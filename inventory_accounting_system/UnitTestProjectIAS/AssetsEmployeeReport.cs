using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using inventory_accounting_system.Controllers;
using inventory_accounting_system.Data;
using inventory_accounting_system.Interface;
using inventory_accounting_system.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace UnitTestProjectIAS {
    public class AssetsEmployeeReport {

        [Fact]
        public void Get_Report_Employee () {

            using (
                var _context = new ApplicationDbContext (CreateNewContextOptions ())) {

                List<Asset> assetsList = new List<Asset> () {
                    new Asset {
                        Id = "92f90e93-d0ae-4dc2-bcec-07ed66c3d913",
                            Name = "Стул",
                            CategoryId = "9d58ec6c-7782-4898-aded-c4ffe629c212",
                            Date = DateTime.Parse ("2019-01-18 0:00"),
                            EmployeeId = "624b872d-879d-4161-af16-21b93d882734",
                            Price = 500,
                            InventNumber = "0010523526",
                            StatusMovingAssets = "transfer_in",
                            OfficeId = "996d401f-1658-4410-bdca-ff600dd24070"
                    },
                    new Asset {
                        Id = "a890c6c6-e013-4d40-a0e2-3bdf278bab66",
                            Name = "Стул",
                            CategoryId = "9d58ec6c-7782-4898-aded-c4ffe629c212",
                            Date = DateTime.Parse ("2018-01-18 0:00"),
                            EmployeeId = "624b872d-879d-4161-af16-21b93d882734",
                            Price = 550,
                            InventNumber = "0010523526",
                            StatusMovingAssets = "transfer_in",
                            OfficeId = "996d401f-1658-4410-bdca-ff600dd24070"
                    }

                }.ToList ();

                _context.AddRange (assetsList);
                _context.SaveChangesAsync ();

                List<Employee> employeeList = new List<Employee> {
                    new Employee { Id = "5534b323-bc8e-45d5-980f-9fc7c9ac102f", Name = "TestName1", OfficeId = "53ad099d-d8c0-46eb-b538-824f4379f3f0" },
                    new Employee { Id = "624b872d-879d-4161-af16-21b93d882734", Name = "TestName2", OfficeId = "996d401f-1658-4410-bdca-ff600dd24070" }
                }.ToList ();

                _context.AddRange (employeeList);
                _context.SaveChangesAsync ();

                AssetsController controller = new AssetsController (_context, null, null, null);
                IActionResult result1 = controller.EmployeeOrderReport ("624b872d-879d-4161-af16-21b93d882734") as IActionResult;

                var resultId = _context.Assets.Where (a => a.EmployeeId == "624b872d-879d-4161-af16-21b93d882734");

                var viewResult = Assert.IsType<ViewResult> (result1);
                var model = Assert.IsAssignableFrom<IEnumerable<Asset>> (viewResult.Model);
                Assert.Equal (resultId.Count (), model.Count ());

            }

        }

        private static DbContextOptions<ApplicationDbContext> CreateNewContextOptions () {

            var serviceProvider = new ServiceCollection ()
                .AddEntityFrameworkInMemoryDatabase ()
                .BuildServiceProvider ();

            var builder = new DbContextOptionsBuilder<ApplicationDbContext> ();
            builder.UseInMemoryDatabase (databaseName: "aspnet-InventorySystem")
                .UseInternalServiceProvider (serviceProvider);

            return builder.Options;
        }

    }
}