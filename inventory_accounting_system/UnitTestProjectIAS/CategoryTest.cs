using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using inventory_accounting_system.Controllers;
using inventory_accounting_system.Data;
using inventory_accounting_system.Interface;
using inventory_accounting_system.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace UnitTestProjectIAS {
    public class CategoryTest {

        [Fact]
        public void Get_Create_Category () {

            //Given   
            var options = new DbContextOptionsBuilder<ApplicationDbContext> ()
                .UseInMemoryDatabase (databaseName: "aspnet-InventorySystem")
                .Options;

            using (var _context = new ApplicationDbContext (options)) {

                Category category1 = new Category { Id = "300007e1-8592-4c4a-93a8-5ec26040b62f", Name = "Мебель", Prefix = "001" };
                Category category2 = new Category { Id = "b03f15cc-5ba3-4c50-a4e7-e4dfaaa2ea7e", Name = "Компьютер", Prefix = "002" };

                //When

                CategoriesController controller = new CategoriesController (_context);
                var result1 = controller.Create (category1);
                var result2 = controller.Create (category2);

                var rSearchId = _context.Categories.SingleOrDefault (a => a.Id == "300007e1-8592-4c4a-93a8-5ec26040b62f");
                var rSearchName = _context.Categories.SingleOrDefault (a => a.Name == "Компьютер");

                var rSearchId2 = _context.Categories.SingleOrDefault (a => a.Id == "1fa7b5e6-0264-4a12-9842-fe5581b8a338");
                var rSearchName2 = _context.Categories.SingleOrDefault (a => a.Name == "Машины");

                //Then
                Assert.NotNull (result1);
                Assert.NotNull (result2);

                Assert.Equal (rSearchId2, null);
                Assert.False (rSearchName2 != null);

                Assert.Equal (rSearchId.Id, "300007e1-8592-4c4a-93a8-5ec26040b62f");
                Assert.Equal (rSearchId.Name, "Мебель");
                Assert.Equal (rSearchId.Prefix, "001");

            }

        }

        [Fact]
        public void Get_Delete_Category () {

            //Given
            var options = new DbContextOptionsBuilder<ApplicationDbContext> ()
                .UseInMemoryDatabase (databaseName: "aspnet-InventorySystem")
                .Options;

            using (var _context = new ApplicationDbContext (options)) {

                Category category1 = new Category { Id = "300007e1-8592-4c4a-93a8-5ec26040b62f", Name = "Мебель", Prefix = "001" };
                Category category2 = new Category { Id = "2130aabd-4545-4063-9c1c-a96784ecdbce", Name = "Компьютеры", Prefix = "002" };
                _context.AddRange (category1, category2);
                _context.SaveChangesAsync ();

                CategoriesController controller = new CategoriesController (_context);
                //Whenф
                Category categoryClass = new Category ();

                Task<IActionResult> result1 = controller.DeleteConfirmed (category1.Id);

                var categorySearch1 = _context.Categories.SingleOrDefault (a => a.Id == category1.Id);
                var categorySearch2 = _context.Categories.SingleOrDefault (a => a.Id == category2.Id);

                //Then
                Assert.NotNull (result1);
                Assert.True (_context.Categories.Count () == 1);
                Assert.True (categorySearch2.Name == "Компьютеры");
                Assert.False (categorySearch2.Equals (null));
                Assert.Equal (categorySearch1, null);
            }
        }

        [Fact]
        public void Get_Edit_Category () {

            //Given
            var options = new DbContextOptionsBuilder<ApplicationDbContext> ()
                .UseInMemoryDatabase (Guid.NewGuid ().ToString ())
                .Options;

            using (var _context = new ApplicationDbContext (options)) {

                Category category1 = new Category { Id = "300007e1-8592-4c4a-93a8-5ec26040b62f", Name = "Мебель", Prefix = "001" };
                _context.Add (category1);
                _context.SaveChangesAsync ();

                CategoriesController controller = new CategoriesController (_context);
                //Whenф
                Category categoryClass = new Category ();

                Task<IActionResult> result1 = controller.Edit (category1.Id);
                var resultEdit = _context.Categories.SingleOrDefault (e => e.Id == category1.Id);
                if (resultEdit != null) {

                    resultEdit.Name = "Furniture";
                    resultEdit.Prefix = "fur-";
                    _context.Update (resultEdit);
                    _context.SaveChanges ();
                }

                //Then
                Assert.NotNull (result1);
                Assert.True (_context.Categories.Count () == 1);
                Assert.True (resultEdit.Name == "Furniture");
                Assert.True (resultEdit.Prefix == "fur-");
            }
        }

        [Fact]
        public void Get_Index_Category () {

            //Given
            // var _context = UseInMemoryDataBase ();
            var options = new DbContextOptionsBuilder<ApplicationDbContext> ()
                .UseInMemoryDatabase (databaseName: "aspnet-InventorySystem")
                .Options;
            using (var _context = new ApplicationDbContext (options)) {

                List<Category> categoryList = new List<Category> () {
                new Category { Id = "92f90e93-d0ae-4dc2-bcec-07ed66c3d913", Name = "Мебель", Prefix = "001" },
                new Category { Id = "2130aabd-4545-4063-9c1c-a96784ecdbce", Name = "Компьютеры", Prefix = "002" },
                new Category { Id = "9ca62a34-1937-47f0-ad02-68c7184e2ea1", Name = "Машины", Prefix = "003" },
                new Category { Id = "01879bac-772b-44b4-a261-5639ebae241c", Name = "Кондицеонеры", Prefix = "004" }
                };

                _context.AddRange (categoryList);
                _context.SaveChangesAsync ();

                //When

                CategoriesController controller = new CategoriesController (_context);

                IActionResult result1 = controller.Index () as IActionResult;

                var resultEdit = _context.Categories.ToList ();
                //Then

                var viewResult = Assert.IsType<ViewResult> (result1);
                var model = Assert.IsAssignableFrom<IEnumerable<Category>> (viewResult.Model);
                Assert.Equal (resultEdit.Count (), model.Count ());
            }

        }

        private ApplicationDbContext UseInMemoryDataBase () {
            var options = new DbContextOptionsBuilder<ApplicationDbContext> ()
                .UseInMemoryDatabase (databaseName: "aspnet-InventorySystem")
                .Options;
            return new ApplicationDbContext (options);
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