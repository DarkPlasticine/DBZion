using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBZion.DAL.Entities
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public int ReceiptId { get; set; }

        [Required]
        [StringLength(20)]
        public string ReceiptType { get; set; }

        [Required]
        [StringLength(20)]
        public string ServiceType { get; set; }

        [Required]
        [Range(1, 999999)]
        public int Price { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public string Description { get; set; }

        public string Note { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public bool IsReady { get; set; }

        [Required]
        public bool Call { get; set; }

        public int? UserID { get; set; }
        public User User { get; set; }

        [Required]
        public string Worker { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public Order()
        {
            RowVersion = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };
        }

        public Order(int receiptId, string receiptType, string serviceType, int price, DateTime orderDate, string description, string note, bool isActive, bool isReady, bool call, User user, string worker)
        {
            ReceiptId = receiptId;
            ReceiptType = receiptType;
            ServiceType = serviceType;
            Price = price;
            OrderDate = orderDate;
            Description = description;
            Note = note;
            IsActive = isActive;
            IsReady = isReady;
            Call = call;
            User = user;
            Worker = worker;
            RowVersion = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };
        }
    }
}
