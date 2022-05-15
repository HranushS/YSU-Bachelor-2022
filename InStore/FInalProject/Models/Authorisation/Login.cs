using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace main.Models.Authorisation
{
    
    public class Login //: PageModel
    {
        [Required(ErrorMessage = "Username is required.")]
        [Display(Name = "Username")]
        [Description("The username is used to login account.")]
        [Column(TypeName = "varchar(50)")]
        public string username { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DisplayName("Login as")]
        [Description("Describes user role.")]
        public role role { get; set; } = role.Admin;
        public ISession session { get; set; }
    }
}
