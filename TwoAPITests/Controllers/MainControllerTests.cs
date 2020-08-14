using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwoAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using TwoAPI.DB;
using TwoAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace TwoAPI.Controllers.Tests
{
    [TestClass()]
    public class MainControllerTests
    {
        [TestMethod()]
        public void ItemsByNameTest()
        {
            var items = new List<Item>
            {
                new Item(){Id=1,Name="Item1",Description="Item1-Desc"
                ,SubCategoryId=1, SubCategory =new SubCategory(){Id=1,Name="SubCat",
                   CategoryId=1, Category =new Category(){ Id=1,Name="Cat"} }}
            }.AsQueryable();
            var itemMock = new Mock<DbSet<Item>>();
            itemMock.As<IQueryable<Item>>().Setup(m => m.Provider).Returns(items.Provider);
            itemMock.As<IQueryable<Item>>().Setup(m => m.Expression).Returns(items.Expression);
            itemMock.As<IQueryable<Item>>().Setup(m => m.ElementType).Returns(items.ElementType);
            itemMock.As<IQueryable<Item>>().Setup(m => m.GetEnumerator()).Returns(items.GetEnumerator());

            var contextMock = new Mock<TwoAPIDbContext>();
            contextMock.Setup(x => x.Items).Returns(itemMock.Object);

            var controller = new MainController(contextMock.Object);

            Assert.Equals("Item1", controller.ItemsByName(string.Empty).ToList()[0].Name);
        }
    }
}