using System;

namespace DBZion.DAL.Entities
{
    public class Order
    {
        public int OrderId { get; set; }

        public int ReceiptId { get; set; }

        public string ServiceType { get; set; }

        public int Price { get; set; }

        public DateTime OrderDate { get; set; }

        public string Description { get; set; }

        public string Note { get; set; }

        public int? UserID;
        public User User;

        public Order()
        {

        }

        public Order(string serviceType, int price, DateTime orderDate, string description, string note, User user)
        {
            ServiceType = serviceType;
            Price = price;
            OrderDate = orderDate;
            Description = description;
            Note = note;
            User = user;
        }
    }
}
