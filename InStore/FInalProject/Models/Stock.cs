using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace main.Models
{
    [Table("stocks")]
    public class Stock
    {

        [Key]
        [ForeignKey("store_id")]
        public int store_id { get; set; }

        [Key]
        public int product_id { get; set; }

        [Description("The quantity of product in stock.")]
        [Required]
        [Range(0, 1000000, ErrorMessage = "Please enter valid positive number")]
        [DisplayName("Quantity")]
        public int quantity { get; set; } 
        

        [ForeignKey("product_id")]
        public Product products { get; set; }

        [ForeignKey("store_id")]
        public Store stores { get; set; }


    }
}
