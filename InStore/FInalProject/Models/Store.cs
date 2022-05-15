using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace main.Models
{
    [Table("stores")]
    public class Store
    {
        [Key]
        public int store_id { get; set; }

        [DisplayName("Name")]
        [Column(TypeName = "varchar(50)")]
        [Description("The name of store.")]
        [StringLength(50, ErrorMessage = "Input is too long.")]
        [Required]
        public string store_name { get; set; }


        [Column(TypeName = "varchar(25)")]
        [StringLength(25, ErrorMessage = "Input is too long.")]
        [Phone(ErrorMessage = "Please enter valid phone number")]
        [DisplayName("Phone")]
        [Required]
        public string? phone { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Not valid email address")]
        [DisplayName("Email")]
        [Column(TypeName = "varchar(50)")]
        [Description("The first name of employee")]
        [StringLength(50, ErrorMessage = "Input is too long.")]
        public string? email { get; set; }

        [Column(TypeName = "varchar(100)")]
        [Description("The first name of employee")]
        [StringLength(100, ErrorMessage = "Input is too long.")]
        [DisplayName("Address")]
        [Required]
        public string address { get; set; }

        [Required]
        [Column(TypeName = "varchar(5)")]
        [StringLength(5, ErrorMessage = "Incorrect zip code")]
        [DisplayName("zip code")]
        public string? zip_code { get; set; }  

        [DisplayName("volume(m³)")]
        [Column(TypeName = "float")]
        public double? free_space { get; set; }

        [Range(-1000, 1000000, ErrorMessage = "Please enter valid number.")]
        [DisplayName("min temperature(°C)")]
        public int? min_temperature { get; set; }

        [Range(-1000, 1000000, ErrorMessage = "Please enter valid number.")]
        [DisplayName("max temperature(°C)")]
        public int? max_temperature { get; set; }

        [Range(0, 100, ErrorMessage = "Please enter valid positive number in range of 0-100%.")]
        [DisplayName("relative humidity(%)")]
        public int? relative_humidity { get; set; }

        public ICollection<Staffs> staffs { get; set; }      
                                                              
        public ICollection<Stock> stocks { get; set; }

        public ICollection<Orders> orders { get; set; }

    }
}

