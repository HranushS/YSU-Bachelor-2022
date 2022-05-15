using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace main.Models
{
    public enum orderStatus
    {
        Pending,
        Processing,
        Rejected,
        Completed
    }
    public enum type
    {
        Entry,
        Exit
    }

    [Table("orders")]
    public class Orders
    {
        [Key]
        public int order_id { get; set; }
        public int customer_id { get; set; }

        [Required]
        [Column(TypeName = "varchar(25)")]
        [DisplayName("Order Status")]
        [Description("Describes state of an order.")]
        public orderStatus order_status { get; set; } = orderStatus.Processing;
    
        [NotMapped]
        [DataType(DataType.Date, ErrorMessage = "Incorrect date")]
        [Description("Date of order registration.")]
        //[DisplayFormat(DataFormatString = "{yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Order Date")]
        public DateTime order_date { get; set; } = DateTime.Now;

        [NotMapped]
        [DataType(DataType.Date, ErrorMessage = "Incorrect date")]
        [Description("Assumed date of order fulfillment.")]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Required date")]
        public DateOnly? required_date { get; set; }

        [NotMapped]
        [DataType(DataType.Date, ErrorMessage = "Incorrect date")]
        [Description("Date when the order shipped to shop or from shop.")]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Shipped date")]
        public DateOnly? shipped_date { get; set; } 


        [Description("Indicates the type of order, entry or exit․")]
        [Required]
        [DisplayName("Type")]
        public type type { get; set; } = type.Entry;

        public int product_id { get; set; }


        [Description("Product quantity.")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid positive number")]
        [DisplayName("Quantity")]
        public int quantity { get; set; }

        public int? store_id { get; set; }




        [ForeignKey("customer_id")]
        public Customer customer { get; set; }
        
        [ForeignKey("product_id")]
        public Product products { get; set; }

        [ForeignKey("store_id")]
        public Store store { get; set; }


    }
}
