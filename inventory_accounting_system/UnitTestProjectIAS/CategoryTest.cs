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
using Xunit;

namespace UnitTestProjectIAS {
    public class CategoryTest {

        [Fact]
        public void Get_Create_Category () {

            //Given         
            var _context = UseInMemoryDataBase ();

            Category categoryA = new Category ();

            Category category1 = new Category { Id = "300007e1-8592-4c4a-93a8-5ec26040b62f", Name = "Мебель", Prefix = "001" };
            Category category2 = new Category { Id = "b03f15cc-5ba3-4c50-a4e7-e4dfaaa2ea7e", Name = "Компьютер", Prefix = "002" };

            CategoriesController controller = new CategoriesController (_context);
            //When

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

            Assert.Equal (rSearchName.Id, "b03f15cc-5ba3-4c50-a4e7-e4dfaaa2ea7e");
            Assert.Equal (rSearchName.Name, "Компьютер");
            Assert.Equal (rSearchName.Prefix, "002");

        }

        [Fact]
        public void Get_Delete_Category () {

            //Given
            var _context = UseInMemoryDataBase ();

            Category category1 = new Category { Id = "300007e1-8592-4c4a-93a8-5ec26040b62f", Name = "Мебель", Prefix = "001" };
            Category category2 = new Category { Id = "2130aabd-4545-4063-9c1c-a96784ecdbce", Name = "Компьютеры", Prefix = "002" };
            _context.AddRange (category1, category2);
            _context.SaveChanges ();

            CategoriesController controller = new CategoriesController (_context);
            //Whenф
            Category categoryClass = new Category ();

            Task<IActionResult> result1 = controller.DeleteConfirmed ("300007e1-8592-4c4a-93a8-5ec26040b62f");

            var categorySearch1 = _context.Categories.SingleOrDefault (a => a.Id == category1.Id);
            var categorySearch2 = _context.Categories.SingleOrDefault (a => a.Id == category2.Id);

            //Then
            Assert.NotNull (result1);
            Assert.True (_context.Categories.Count () == 1);
            Assert.True (categorySearch2.Name == "Компьютеры");
            Assert.False (categorySearch2.Equals (null));
            Assert.Equal (categorySearch1, null);
        }

        private ApplicationDbContext UseInMemoryDataBase () {
            var options = new DbContextOptionsBuilder<ApplicationDbContext> ()
                .UseInMemoryDatabase (databaseName: "aspnet-InventorySystem")
                .Options;
            return new ApplicationDbContext (options);
        }

    }
}