using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using inventory_accounting_system.Controllers;
using inventory_accounting_system.Data;
using inventory_accounting_system.Models;
using Microsoft.AspNetCore.Mvc;

namespace UnitTestProjectIAS
{
    [TestClass]
    public class EmployeeTestController
    {
        private ApplicationDbContext context = AssetEventEdit.UseInMemoryDataBase();

        [TestMethod]
        public void TestIndex()
        {
            var controller = new EmployesController(context, null);

            var result = controller.Index() as ActionResult;

            Assert.IsNotNull(result);
        }

        public void TestDetails()
        {
            context.Add(new Employee()
            {
                Id = "1",
                Name = "Lolka"
            });

            var controller = new EmployesController(context, null);

            var result = controller.Details("1");

            Assert.IsNotNull(result);
        }

        public void TestDelete()
        {
            context.Add(new Employee()
            {
                Id = "1",
                Name = "Lolka"
            });

            var controller = new EmployesController(context, null);

            var result = controller.DeleteConfirmed("1");

            var confirmed = context.Users.Find("1");

            Assert.IsNull(confirmed);
        }

        public void TestUserAssets()
        {
            context.Add(new Employee()
            {
                Id = "1",
                Name = "Lolka"
            });
            context.Add(new Asset()
            {
                Id = "1",
                Name = "lolotrolo"
            });
            var controller = new EmployesController(context, null);

            var result = controller.UserAssets("1");

            Assert.IsNotNull(result);
        }

        public void TestUsers()
        {
            context.Add(new Employee()
            {
                Id = "1",
                Name = "Lolka"
            });
            context.Add(new Employee()
            {
                Id = "2",
                Name = "Lolka"
            });

            var controller = new EmployesController(context, null);

            var result = controller.Users().ToAsyncEnumerable();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }
    }
}
