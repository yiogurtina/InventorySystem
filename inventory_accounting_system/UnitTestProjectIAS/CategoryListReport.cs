using System;
using System.Collections.Generic;
using System.Linq;
using inventory_accounting_system.Controllers;
using inventory_accounting_system.Data;
using inventory_accounting_system.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace UnitTestProjectIAS {
    public class CategoryListReport {
        [Fact]
        public void Get_Category_Report () {

            using (
                var _context = new ApplicationDbContext (CreateNewContextOptions ())) {

                List<Asset> assetsList = new List<Asset> () {
                    new Asset {
                        Id = "92f90e93-d0ae-4dc2-bcec-07ed66c3d913",
                            Name = "Стул",
                            CategoryId = "d0b7d7cc-cd08-46bb-9765-b875c9c49077",
                            Date = DateTime.Parse ("2019-01-18 0:00"),
                            EmployeeId = "624b872d-879d-4161-af16-21b93d882734",
                            Price = 500
                    },
                    new Asset {
                        Id = "a890c6c6-e013-4d40-a0e2-3bdf278bab66",
                            Name = "Стул",
                            CategoryId = "3ef9cc62-a305-4373-b258-c555e0c9fb4a",
                            Date = DateTime.Parse ("2018-01-18 0:00"),
                            EmployeeId = "624b872d-879d-4161-af16-21b93d882734",
                            Price = 550
                    },
                    new Asset {
                        Id = "d6301890-44f5-42ba-848d-dafd6dcc98a2",
                            Name = "Диван",
                            CategoryId = "3ef9cc62-a305-4373-b258-c555e0c9fb4a",
                            Date = DateTime.Parse ("2018-01-18 0:00"),
                            EmployeeId = "624b872d-879d-4161-af16-21b93d882734",
                            Price = 550
                    }

                }.ToList ();

                _context.AddRange (assetsList);
                _context.SaveChangesAsync ();

                List<Category> categoryList = new List<Category> {
                    new Category { Id = "3ef9cc62-a305-4373-b258-c555e0c9fb4a", Name = "Мебель" },
                    new Category { Id = "d0b7d7cc-cd08-46bb-9765-b875c9c49077", Name = "Машины" }
                }.ToList ();

                _context.AddRange (categoryList);
                _context.SaveChangesAsync ();

                AssetsController controller = new AssetsController (_context, null, null, null);
                IActionResult result1 = controller.CategoryList ("3ef9cc62-a305-4373-b258-c555e0c9fb4a") as IActionResult;

                var resultId = _context.Assets.Where (a => a.CategoryId == "3ef9cc62-a305-4373-b258-c555e0c9fb4a");

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