using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using inventory_accounting_system.Controllers;
using inventory_accounting_system.Data;
using inventory_accounting_system.Models;
using inventory_accounting_system.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

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

            var okRes = result as Task<IActionResult>;

            Assert.IsNotNull(result);
            Assert.IsNotNull(okRes);
        }
        [TestMethod]
        public void DetailsTest()
        {
            context.Add(new Event()
            {
                Id = "1",
                Title = "grra"
            });
            var controller = new EventsController(context, null);

            var result = controller.Details("1");

            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void CreateTest()
        {
            context.Add(new Event()
            {
                Id = "1",
                Title = "gege"
            });
            var controller = new EventsController(context, null);

            var item = context.Events.Find("1");

            var result = controller.Create(item);

            var okRes = result as Task<IActionResult>;

            Assert.IsNotNull(okRes);
        }
        //[TestMethod]
        public void EditTest()
        {
            context.Add(new Event()
            {
                Id = "1",
                Title = "gege"
            });
            context.Add(new Employee()
            {
                Id = "1",
                Name = "jana"
            });
            var controller = new EventsController(context, null);

            var updatedEvent = new Event()
            {
                Id = "1",
                Title = "koko"
            };

            var edited = controller.Edit("1", updatedEvent);

            var result = context.Events.Find("1");


            Assert.AreNotSame(result.Title, updatedEvent.Title);
        }
        [TestMethod]
        public void TestDeleteConfirmed()
        {
            context.Add(new Event()
            {
                Id = "1",
                Title = "koko"
            });
            var controller = new EventsController(context, null);

            var result = controller.DeleteConfirmed("1");

            var checkRes = context.Events.Find("1");

            Assert.IsNotNull(checkRes);

        }
        [TestMethod]
        public void TestDelete()
        {
            context.Add(new Event()
            {
                Id = "1",
                Title = "koko"
            });
            var controller = new EventsController(context, null);

            var result = controller.Delete("1");

            var okResult = result as Task<IActionResult>;

            Assert.IsNotNull(okResult);
        }
        //[TestMethod]
        public void TestUpdateExpiredEvents()
        {

            var mockUserManager = new Mock<UserManager<Employee>>(
                new Mock<IUserStore<Employee>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<IPasswordHasher<Employee>>().Object,
                new IUserValidator<Employee>[0],
                new IPasswordValidator<Employee>[0],
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<IServiceProvider>().Object,
                new Mock<ILogger<UserManager<Employee>>>().Object);

            context.Add(new EventAsset()
            {
                Id = "1",
                Title = "koko",
                DeadLine = DateTime.Now.AddDays(1)
            });

            var controller = new EventsController(context, mockUserManager.Object);

            controller.UpdateExpiredEvents();

            var ev = context.AssetEvents.Find("1");

            Assert.AreEqual(DateTime.Now.AddDays(1).Date, ev.DeadLine.Date);
        }
    }
}
