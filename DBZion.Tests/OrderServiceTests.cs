using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DBZion.BLL.Services;
using DBZion.BLL.Interfaces;

namespace DBZion.Tests
{
    [TestClass]
    public class OrderServiceTests
    {
        private string testConStr = @"Data Source=.\sqlserv;Initial Catalog=ZionTest;Integrated Security=True";
        private IOrderService service;

        public OrderServiceTests()
        {
            service = new OrderService(testConStr);
        }

        [TestMethod]
        public void OrderIsAddedTest()
        {
            int ordersCountBefore = service.GetOrders().Count;

            string surname = "Валеронов";
            string firstName = "Валерон";
            string middleName = "Валеронович";
            string phoneNumber = "123456";
            string serviceType = "Тест";
            int price = 500;
            DateTime orderDate = DateTime.Now;
            string description = "123";
            string note = "123";

            service.AddOrder(surname, firstName, middleName, phoneNumber, serviceType, price, orderDate, description, note);


            int ordersCountAfter = service.GetOrders().Count;

            Assert.AreEqual(ordersCountBefore + 1, ordersCountAfter);
        }
    }
}
