using System.Collections.Generic;
using inventory_accounting_system.Controllers;
using inventory_accounting_system.Data;
using inventory_accounting_system.Interface;
using inventory_accounting_system.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTestProjectIAS {
    [TestClass]
    public class AssetEventEdit {

        [TestMethod]
        public void DeleteAssetEventTest () {
            var context = UseInMemoryDataBase ();

            context.AssetEvents.Add (new EventAsset () {
                Id = "1",
                    Title = "Eat Pattaya"
            });
            var controller = new AssetEventController (context);

            var result = controller.DeleteEvent ("1");

            var test = context.AssetEvents.Find ("1");

            Assert.IsNull (test);
        }

        [TestMethod]
        public void AddAssetEventTest () {
            var context = UseInMemoryDataBase ();

            var controller = new AssetEventController (context);

            var result = controller.AddEvent ("1");

            var okRes = result as IActionResult;

            Assert.IsNotNull (okRes);
        }

        [TestMethod]
        public void AddNewAssetEventTest () {
            var context = UseInMemoryDataBase ();

            context.Assets.Add(new Asset()
            {
                Id = "1",
                Name = "asset"
            });

            context.AssetEvents.Add(new EventAsset()
            {
                Id = "1",
                Title = "Pee"
            });

            var controller = new AssetEventController (context);

            EventAsset eventAsset = new EventAsset()
            {
                Id = "2",
                Content = "asd"
            };

            var result = controller.AddNewEvent (eventAsset, "Ежедневно", "1");

            Assert.IsNotNull(result);
        }

        public static ApplicationDbContext UseInMemoryDataBase () {
            var options = new DbContextOptionsBuilder<ApplicationDbContext> ()
                .UseInMemoryDatabase (databaseName: "aspnet-InventorySystem")
                .Options;
            return new ApplicationDbContext (options);
        }
    }
}