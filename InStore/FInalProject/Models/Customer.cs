using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace main.Models
{
    [Table("customers")]
    public class Customer
    {
        [Key]
        public int customer_id { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "Input is too long.")]
        [DisplayName("First name")]
        public string first_name { get; set; }

        [StringLength(25, ErrorMessage = "Input is too long.")]
        [DisplayName("Last name")]
        [Required]
        public string last_name { get; set; }

        [DisplayName("Full name")]
        public string full_name { get { return first_name + " " + last_name; } }

        [EmailAddress(ErrorMessage = "Please enter valid email address")]
        [DisplayName("Email")]
        [Column(TypeName = "varchar(100)")]
        [StringLength(100, ErrorMessage = "Input is too long.")]
        public string? email { get; set; }

        [Remote(action: "IsCustomerExists", controller: "Customers", ErrorMessage = "This phone number is already used.")]
        [Phone(ErrorMessage ="Please enter valid phone number.")]
        [DisplayName("Phone")]
        [StringLength(25, ErrorMessage = "Input is too long.")]
        public string phone { get; set; }

        [Column(TypeName = "varchar(100)")]
        [StringLength(100, ErrorMessage = "Input is too long.")]
        [DisplayName("Address")]
        public string? address { get; set; }

        [Column(TypeName = "varchar(50)")]
        [StringLength(50, ErrorMessage = "Input is too long.")]
        [DisplayName("Company")]
        public string? company { get; set; }

        public ICollection<Orders> orders { get; set; }

    }
}
