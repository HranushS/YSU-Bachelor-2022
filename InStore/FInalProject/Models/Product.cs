using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace main.Models
{
    [Table("products")]
    public class Product
    {
        [Key]
        public int product_id { get; set; }


        [DisplayName("Name")]
        [Column(TypeName = "varchar(50)")]
        [StringLength(50, ErrorMessage = "Input is too long.")]
        [Required]
        public string product_name { get; set; }


        [NotMapped]
        [DataType(DataType.Date, ErrorMessage = "Incorrect date")]
        [Description("Production date of product.")]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Production Date")]
        public DateOnly? production_date { get; set; }


        [NotMapped]
        [DataType(DataType.Date, ErrorMessage = "Incorrect date")]
        [Description("Expiry date of product.")]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Expiry Date")]
        public DateOnly? expiry_date { get; set; }

        [StringLength(50, ErrorMessage = "Input is too long.")]
        [DisplayName("Category")]
        [Column(TypeName = "varchar(50)")]
        public string? category { get; set; }


        [DisplayName("Weight")]
        public double? weight { get; set; }


        [DisplayName("Size(m³)")]
        public double? size { get; set; }


        //[Description("The picture of product.")]
        //[DisplayName("Picture")]
        //public string? img { get; set; }

        [StringLength(50, ErrorMessage = "Input is too long.")]
        [Column(TypeName = "varchar(50)")]
        [DisplayName("Brand")]
        [Required]
        public string brand { get; set; }


        [DisplayName("Detaild description")]
        [Description("Detaild description of product.")]
        public string? detailed_description { get; set; }


        //[NotMapped]
        //[DisplayName("Upload photo")]
        //[DisplayFormat(DataFormatString = "jpg/png")]
        //public IFormFile imageFile { get; set; }

        public ICollection<Orders> orders { get; set; }
        public ICollection<Stock> stocks { get; set; }



    }
}
