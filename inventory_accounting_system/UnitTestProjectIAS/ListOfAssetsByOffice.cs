using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using inventory_accounting_system.Controllers;
using inventory_accounting_system.Data;
using inventory_accounting_system.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace UnitTestProjectIAS {
    public class ListOfAssetsByOffice {

        [Fact]
        public void Get_Report_On_Specific_Date () {

            using (
                var _context = new ApplicationDbContext (CreateNewContextOptions ())) {

                List<AssetsMoveStory> assetsList = new List<AssetsMoveStory> () {
                    new AssetsMoveStory {
                        Id = "92f90e93-d0ae-4dc2-bcec-07ed66c3d913",
                            DateStart = DateTime.Parse ("2019-01-18 0:00"),
                            OfficeToId = "7043e98c-5284-4b9f-9ea7-8bd1fb9ae7af"
                    },
                    new AssetsMoveStory {
                        Id = "a890c6c6-e013-4d40-a0e2-3bdf278bab66",
                            DateStart = DateTime.Parse ("2018-01-18 0:00"),
                            OfficeToId = "1fb61ce3-8355-44cd-a69a-5de75b2669c4"
                    },
                    new AssetsMoveStory {
                        Id = "d6301890-44f5-42ba-848d-dafd6dcc98a2",
                            DateStart = DateTime.Parse ("2018-01-18 0:00"),
                            OfficeToId = "1fb61ce3-8355-44cd-a69a-5de75b2669c4"
                    }

                }.ToList ();

                _context.AddRange (assetsList);
                _context.SaveChangesAsync ();

                List<Office> officeList = new List<Office> {
                    new Office { Id = "1fb61ce3-8355-44cd-a69a-5de75b2669c4", Title = "Office-1" },
                    new Office { Id = "293ed433-3588-4760-815a-e49798be4515", Title = "Office-2" }
                }.ToList ();

                _context.AddRange (officeList);
                _context.SaveChangesAsync ();

                AssetsController controller = new AssetsController (_context, null, null, null);
                DateTime date = DateTime.Parse ("2018-01-18 0:00");
                IActionResult result1 = controller.ListOfAssetsByOffice ("1fb61ce3-8355-44cd-a69a-5de75b2669c4", "2018-01-18") as IActionResult;
                DateTime dt = DateTime.ParseExact ("2018-01-18", "yyyy-MM-dd", CultureInfo.InvariantCulture);

                var resultId = _context.AssetsMoveStories.Where (a => a.OfficeToId == "1fb61ce3-8355-44cd-a69a-5de75b2669c4" && a.DateStart == dt);

                var viewResult = Assert.IsType<ViewResult> (result1);
                var model = Assert.IsAssignableFrom<IEnumerable<AssetsMoveStory>> (viewResult.Model);
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