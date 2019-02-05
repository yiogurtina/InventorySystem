using inventory_accounting_system.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using inventory_accounting_system.Controllers;
using inventory_accounting_system.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;
using Assert = Xunit.Assert;

namespace UnitTestProjectIAS
{
    public class SuppliersTest
    {
        [TestMethod]
        [Fact]
        public void SuppliersCreate()
        {

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "aspnet-InventorySystem")
                .Options;

            using (var _context = new ApplicationDbContext(options))
            {

                Supplier vendor = new Supplier() { Id = "1" };
                var controller = new SuppliersController(_context);
                var result1 = controller.Create(vendor);
                var rSearchId = _context.Suppliers.SingleOrDefault(a => a.Id == "1");
                Assert.NotNull(result1);
                Assert.Equal(rSearchId.Id, "1");
              

            }

        }

        [Fact]
        public void SupplierDelete()
        {
            
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "aspnet-InventorySystem")
                .Options;

            using (var _context = new ApplicationDbContext(options))
            {

                Supplier vendor = new Supplier() { Id = "1" };
                _context.Add(vendor);
                _context.SaveChangesAsync();
                var controller = new SuppliersController(_context);
                Supplier supp = new Supplier();
                Task<IActionResult> result1 = controller.Delete(vendor.Id);
                var supSearch = _context.Suppliers.SingleOrDefault(a => a.Id == vendor.Id);

                Assert.NotNull(result1);
                Assert.Equal(null, supSearch);
            }
        }

        [Fact]
        public void SuppliersEdit()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var _context = new ApplicationDbContext(options))
            {
                Supplier vendor = new Supplier() { Id = "1", Name = "Samsung" };
                _context.Add(vendor);
                _context.SaveChangesAsync();

                var controller = new SuppliersController(_context);
                Supplier supp = new Supplier();

                Task<IActionResult> result = controller.Edit(vendor.Id);
                var editedResult = _context.Suppliers.SingleOrDefault(e => e.Id == vendor.Id);
                if (editedResult != null)
                {

                    editedResult.Name = "LG";
                    _context.Update(editedResult);
                    _context.SaveChanges();
                }

                //Then
                Assert.NotNull(vendor);
                Assert.False(editedResult.Id == "a");
                Assert.True(editedResult.Name == "LG");
                
            }
        }

        [Fact]
        public void CheckIndexSuppliers()
        {

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "aspnet-InventorySystem")
                .Options;
            using (var _context = new ApplicationDbContext(options))
            {

                List<Supplier> supList = new List<Supplier>() {
                new Supplier { Id = "1", Name = "LG"},
                new Supplier { Id = "2", Name = "Samsung"},
                new Supplier { Id = "3", Name = "MI"},
                
                };

                _context.AddRange(supList);
                _context.SaveChangesAsync();

                var controller = new SuppliersController(_context);
                IActionResult result = controller.Index() as IActionResult;

                var resultEdit = _context.Suppliers.ToList();
                /*var viewResult =*/ //Assert.IsType<ViewResult>(result);
                //var model = Assert.IsAssignableFrom<IEnumerable<Supplier>>(viewResult.Model);


                Assert.True(resultEdit.Count() == 3);
            }

        }
    }
}

