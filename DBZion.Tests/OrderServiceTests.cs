using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DBZion.BLL.Services;
using DBZion.BLL.Interfaces;
using DBZion.DAL.Entities;
using System.Collections.Generic;

namespace DBZion.Tests
{
    [TestClass]
    public class OrderServiceTests
    {
        private string testConStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ZionTest;Integrated Security=True";
        private IOrderService service;

        public OrderServiceTests()
        {
            service = new OrderService(testConStr);
        }

        [TestMethod]
        public void OrderIsAddedTest()
        {
            int ordersCountBefore = service.GetOrders().Count;

            string receiptType = @"Ремонт Б/У";
            string surname = "Валеронов";
            string firstName = "Валерон";
            string middleName = "Валеронович";
            string phoneNumber = "123456";
            string serviceType = "Тест";
            int price = 500;
            DateTime orderDate = DateTime.Now;
            string description = "123";
            string note = "123";
            bool isActive = true;
            bool isReady = false;
            bool call = false;
            string worker = "Васыль";

            service.AddOrder(surname, firstName, middleName, phoneNumber, 5, receiptType, serviceType, price, orderDate, description, note, isActive, isReady, call, worker);


            int ordersCountAfter = service.GetOrders().Count;

            Assert.AreEqual(ordersCountBefore + 1, ordersCountAfter);
        }
    }
}
