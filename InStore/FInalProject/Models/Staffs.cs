using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Web;
using Microsoft.AspNetCore.Mvc;


namespace main.Models
{
    public enum position
    {
     CEO,
     COO,
     CFO,
     CMO,
     CTO,
	 President,
	 Manager,
	 Porter,
	 Watcher,
	 Cleaner,
    }
    [Table("staffs")]
    public class Staffs
    {
        [Key]
        public int staff_id { get; set; }
        [Required]
        [DisplayName("First name")]
        [Column(TypeName = "varchar(50)")]
        [Description("The first name of employee")]
        [StringLength(50, ErrorMessage = "Input is too long.")]
        public string first_name { get; set; }

        [Required]
        [DisplayName("Last name")]
        [Description("The last name of employee")]
        [Column(TypeName = "varchar(50)")]
        [StringLength(50, ErrorMessage = "Input is too long.")]
        public string last_name { get; set; }


        [DisplayName("Full name")]
        public string full_name { get { return first_name + " " + last_name; } }


        [EmailAddress(ErrorMessage = "Please enter valid email address.")]
        [Description("The email address of employee.")]
        [DisplayName("Email")]
        [StringLength(100, ErrorMessage = "Input is too long.")]
        [Required(ErrorMessage ="Emailis required.")]
        [Column(TypeName = "varchar(100)")]
        public string? email { get; set; }

        [Remote(action: "IsStaffExists", controller: "Staffs", ErrorMessage = "This phone number is already registered.")]
        [StringLength(25, ErrorMessage = "Input is too long.")]
        [DisplayName("Phone")]
        [Column(TypeName = "varchar(25)")]
        [Phone(ErrorMessage ="Please enter valid phone number.")]
        [Required(ErrorMessage ="Employee phone number is required.")]
        [Description("The phone number of employee.It is confidential, please don't share it.")]
        public string phone { get; set; }


        //[Description("The photo of employee. It is confidential information, please don't share it.")]
        //[DisplayName("Picture")]
        //[Column(TypeName = "varchar(255)")]
        //[StringLength(255, ErrorMessage = "Input is too long.")]
        //public string? img { get; set; }

        [Required]
        [Description("The job title of employee.")]
        [Column(TypeName = "varchar(50)")]
        [DisplayName("Position")]
        public position position { get; set; } = position.Manager;

        [NotMapped]
        [DataType(DataType.Date,ErrorMessage ="Incorrect date")]
        [Description("The date, when employee started work here.")]
        [DisplayName("Start Date")]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateOnly? start_date { get; set; }

        [NotMapped]
        [DataType(DataType.Date, ErrorMessage = "Incorrect date")]
        [Description("The date, when employee left his job, you can delete him from employee list.")]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("End Date")]
        public DateOnly? end_date { get; set; }

        public int? store_id { get; set; }


        [ForeignKey("store_id")]
        public Store stores { get; set; }

        //[NotMapped]
        //[DisplayName("Upload photo")]
        //[DisplayFormat(DataFormatString ="jpg/png")]
        //public IFormFile imageFile { get; set; }
    }
}
