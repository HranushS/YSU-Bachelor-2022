using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace main.Models
{
    [Table("users")]
    public partial class Users /*: IdentityUser*/
    {
        [Key]
        public int user_id { get; set; }
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
        public string username { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        //Required attribute implements validation on Model item that this fields is mandatory for user
        [Required(ErrorMessage = "Phone is required.")]
        [Phone(ErrorMessage = "Please enter valid phone number.")]
        public string Phone { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        
        public string role { get; set; } = "Admin";

        //[Required(AllowEmptyStrings = false, ErrorMessage = "Confirm-Password is required.")]
        //[DataType(DataType.Password)]
        //[Compare("Password")]
        //// [Compare("Password",ErrorMessage =="Password and confirm password should be same.")]
        //[DisplayName("Confirm Password")]
        //public string ConfirmPassword { get; set; }
    }
}
