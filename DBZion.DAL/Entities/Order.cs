using System;

namespace DBZion.DAL.Entities
{
    public class Order
    {
        public int OrderId { get; set; }

        public int ReceiptId { get; set; }

        public string ServiceType { get; set; }

        public int Price { get; set; }

        public int? UserID;
        public User User;

        public DateTime OrderDate { get; set; }

        public string Description { get; set; }

        public string Note { get; set; }

        public Order()
        {

        }

        public Order(string serviceType, int price, User user, DateTime orderDate, string description, string note)
        {

        }
    }
}
