using System;
using System.Collections.Generic;
using System.Text;
using inventory_accounting_system.Controllers;
using inventory_accounting_system.Data;
using inventory_accounting_system.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProjectIAS
{
    [TestClass]
    public class TestEventController
    {
        private ApplicationDbContext context = AssetEventEdit.UseInMemoryDataBase();

        [TestMethod]
        public void IndexTest()
        {
            context.Add(new Event()
            {
                Id = "1",
                Title = "new"
            });
            var controller = new EventsController(context, null);

            var result = controller.Index();

            Assert.IsNotNull(result);
        }
    }
}
