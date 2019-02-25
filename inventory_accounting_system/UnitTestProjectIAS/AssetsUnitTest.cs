using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using inventory_accounting_system.Controllers;
using inventory_accounting_system.Data;
using inventory_accounting_system.Interface;
using inventory_accounting_system.Models;
using inventory_accounting_system.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace UnitTestProjectIAS {
    public class AssetsUnitTest {
        
        [Fact]
        public async Task Get_CategoryAssets_Assets () {

            using (var context = new ApplicationDbContext (CreateNewContextOptions ())) {

                List<Asset> assetsList = new List<Asset> () {
                    new Asset {Id = "1", CategoryId = "1", OfficeId = "1", Name = "Стул", InStock = false, IsActive = true},
                    new Asset {Id = "2", CategoryId = "2", OfficeId = "2", Name = "Стул"},
                    new Asset {Id = "3", CategoryId = "1", OfficeId = "1", Name = "Стул", IsActive = true},
                    new Asset {Id = "4", CategoryId = "4", OfficeId = "4", Name = "Стул"},
                    new Asset {Id = "5", CategoryId = "1", OfficeId = "1", Name = "Стул", IsActive = true}

                }.ToList ();

                context.AddRange (assetsList);
                await context.SaveChangesAsync ();

                AssetsController controller = new AssetsController (context, null, null, null);
                IActionResult result1 = controller.CategoryAssets ("1", "1").Result;

                var result = context.Assets
                    .Where(a => a.OfficeId == "1")
                    .Where(a => a.CategoryId == "1")
                    .Where(a => a.IsActive == true)
                    .Where(a => a.InStock == false);

                Assert.NotNull(result1);
                Assert.Equal(3, result.Count());
            }

        }
        
        [Fact]
        public async Task Get_Details_Assets () {

            using (var context = new ApplicationDbContext (CreateNewContextOptions ())) {

                Asset asset = new Asset(){Id = "3", CategoryId = "1", OfficeId = "1", Name = "Стул", IsActive = true};

                context.Add (asset);
                await context.SaveChangesAsync ();

                AssetsController controller = new AssetsController (context, null, null, null);
                var result1 = controller.Details(asset.Id ) as Task<IActionResult>;

                var result = await context.Assets
                    .Include (a => a.Category)
                    .Include (a => a.Supplier)
                    .SingleOrDefaultAsync (m => m.Id == "3");
                
                DetailsAssetViewModel model = new DetailsAssetViewModel () {
                    Asset = result,
                    AssetsMoveStories = context.AssetsMoveStories
                        .Where (f => f.AssetId == "3")
                    };


                Assert.NotNull(result1);
                Assert.NotNull(model);
                Assert.Equal("1", model.Asset.CategoryId);
            }

        }
        
        [Fact]
        public async Task Get_Create_Assets () {

            using (var context = new ApplicationDbContext (CreateNewContextOptions ())) {

                Asset asset = new Asset();

                AssetsController controller = new AssetsController (context, null, null, null);
                var result1 = controller.Create(asset, "555", "333", null ) as Task<IActionResult>;
                 
                asset.Id = "1";
                asset.CategoryId = "3";
                asset.Name = "Стул";
                asset.SerialNum = "555";
                
                context.Add (asset);
                await context.SaveChangesAsync ();

               

                Assert.NotNull (result1);
                Assert.True(asset.Name == "Стул");
                Assert.True(asset.SerialNum == "555");
                Assert.True(asset.Id == "1");
                Assert.True(asset.CategoryId == "3");
            }
        }
        
        [Fact]
        public async Task Get_Edit_Assets () {

            using (var context = new ApplicationDbContext (CreateNewContextOptions ())) {

                Asset asset = new Asset(){Id = "3", CategoryId = "1", OfficeId = "1", Name = "Стул", IsActive = true};

                context.Add (asset);
                await context.SaveChangesAsync ();
                
                AssetsController controller = new AssetsController (context, null, null, null);
                var result1 = controller.Edit("3") as Task<IActionResult>;

                var s = context.Assets.FirstOrDefault(a => a.Id == "3");
                if (s != null) s.Name = "Диван";

                context.Update(s);
                await context.SaveChangesAsync ();

               

                Assert.NotNull (result1);
                Assert.True(s.Name == "Диван");
            }
        }
        
        
        [Fact]
        public async Task Get_Delete_Assets () {

            using (var context = new ApplicationDbContext (CreateNewContextOptions ())) {

                List<Asset> assetsList = new List<Asset> () {
                    new Asset {Id = "1", CategoryId = "1", OfficeId = "1", Name = "Стул", InStock = false, IsActive = true},
                    new Asset {Id = "2", CategoryId = "2", OfficeId = "2", Name = "Стул"},
                    new Asset {Id = "3", CategoryId = "1", OfficeId = "1", Name = "Стул", IsActive = true},
                    new Asset {Id = "4", CategoryId = "4", OfficeId = "4", Name = "Стул"},
                    new Asset {Id = "5", CategoryId = "1", OfficeId = "1", Name = "Стул", IsActive = true}

                }.ToList ();

                context.AddRange (assetsList);
                await context.SaveChangesAsync ();
                
                AssetsController controller = new AssetsController (context, null, null, null);
                var result1 = controller.DeleteConfirmed("3") as Task<IActionResult>;

                var s = context.Assets.FirstOrDefault(a => a.Id == "3");

                context.Remove(s);
                await context.SaveChangesAsync ();

                var list = context.Assets;

                Assert.NotNull (result1);
                Assert.Equal(4, list.Count());
            }
        }
        
        [Fact]
        public async Task Get_Move_Assets () {

            using (var context = new ApplicationDbContext (CreateNewContextOptions ()))
            {

                AssetsMoveStory assetsList = new AssetsMoveStory();

                
                
                AssetsController controller = new AssetsController (context, null, null, null);
                var result1 = controller.Move(assetsList) as Task<IActionResult>;

                assetsList.Id = "1";
                assetsList.AssetId = "3";
                
                context.Add (assetsList);
                await context.SaveChangesAsync ();

                Assert.NotNull (result1);
                Assert.Equal("3" , assetsList.AssetId);
            }
        }
        
        [Fact]
        public async Task Get_InventNumberSearch_Assets () {

            using (var context = new ApplicationDbContext (CreateNewContextOptions ())) {

                List<Asset> assetsList = new List<Asset> () {
                    new Asset {Id = "1" , InventNumber = "52"},
                    new Asset {Id = "2" , InventNumber = "5"},
                    new Asset {Id = "3" , InventNumber = "1"},
                    new Asset {Id = "4" , InventNumber = "4"},
                    new Asset {Id = "5" , InventNumber = "3"}

                }.ToList ();

                context.AddRange (assetsList);
                await context.SaveChangesAsync ();
                
                AssetsController controller = new AssetsController (context, null, null, null);
                var result1 = controller.DeleteConfirmed("3") as Task<IActionResult>;

                var s = context.Assets.FirstOrDefault(a => a.InventNumber == "3");

                context.Remove(s);
                await context.SaveChangesAsync ();

                var list = context.Assets;

                Assert.NotNull (result1);
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