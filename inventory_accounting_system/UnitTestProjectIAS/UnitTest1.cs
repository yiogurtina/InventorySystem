using System;
using System.Collections.Generic;
using inventory_accounting_system.Interface;
using inventory_accounting_system.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTestProjectIAS
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var mock = new Mock<ICategoryReposytory>();
            mock.Setup(r => r.Categories).Returns(GetCategories());
        }

        private List<Category> GetCategories()
        {
            var categories = new List<Category>()
            {
                new Category()
                {
                    Id = Guid.NewGuid().ToString(), Name = "Kira"
                }        
            };
            return categories;
        }
    }
}
