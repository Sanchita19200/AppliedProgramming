using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace DisasterAlleviationFoundation.Models
{
    public class UserProfileModel
    {
        [Key]
        public int UID { get; set; }
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string Cpassword { get; set; }
    }
}
