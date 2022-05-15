using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyFinalProjectWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace main.Models.Authorisation

{
    public enum role
    {
        Customer,
        Manager,
        Admin
    }
    public class Register
    {
        [Required(ErrorMessage = "First name is required.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [Display(Name = "Username")]
        [Description("The username is used to login account.")]
        [Column(TypeName = "varchar(50)")]
        [Remote(action: "IsUsernameExists", controller: "Account", ErrorMessage = "This username is already used.")]

        public string username { get; set; }
        public string us { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        //Required attribute implements validation on Model item that this fields is mandatory for user
        [Required(ErrorMessage = "Phone is required.")]
        [Phone(ErrorMessage = "Please enter valid phone number.")]
        [Remote(action: "IsUserExists", controller: "Account",ErrorMessage ="This phone number is already registered.")]
        public string Phone { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 5)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Description("This field is used to confirm password and complete user registration")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Confirm Password is required.")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [DisplayName("Register as")]
        [Description("Describes user role.")]
        public role role { get; set; } = role.Admin;


    }
}
