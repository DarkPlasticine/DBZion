using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBZion.DAL.Entities
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReceiptId { get; set; }

        [Required]
        [StringLength(20)]
        public string ServiceType { get; set; }

        [Required]
        [Range(1, 999999)]
        public int Price { get; set; }

        public DateTime OrderDate { get; set; }

        [Required]
        public string Description { get; set; }

        public string Note { get; set; }

        public bool IsReady { get; set; }

        public bool Call { get; set; }

        public int? UserID { get; set; }
        public User User { get; set; }

        public Order()
        {

        }

        public Order(int receiptId, string serviceType, int price, DateTime orderDate, string description, string note, bool isReady, bool call, User user)
        {
            ReceiptId = receiptId;
            ServiceType = serviceType;
            Price = price;
            OrderDate = orderDate;
            Description = description;
            Note = note;
            IsReady = isReady;
            Call = call;
            User = user;
        }
    }
}
