using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBZion.DAL.Entities
{
    [Table("Archive")]
    public class ArchivedOrder
    {
        [Key]
        public int OrderId { get; set; }

        public int ReceiptId { get; set; }

        public string ServiceType { get; set; }

        public int Price { get; set; }

        public DateTime OrderDate { get; set; }

        public string Description { get; set; }

        public string Note { get; set; }

        public int? UserID { get; set; }
        public User User { get; set; }

        public ArchivedOrder()
        {

        }

        public ArchivedOrder(int receiptId, string serviceType, int price, DateTime orderDate, string description, string note, User user)
        {
            ReceiptId = receiptId;
            ServiceType = serviceType;
            Price = price;
            OrderDate = orderDate;
            Description = description;
            Note = note;
            User = user;
        }
    }
}
