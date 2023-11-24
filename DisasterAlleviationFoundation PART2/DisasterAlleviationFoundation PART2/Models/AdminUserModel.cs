using System.ComponentModel.DataAnnotations;

namespace DisasterAlleviationFoundation.Models
{
    // Represents the data model for an administrative user.
    public class AdminUserModel
    {
        // Primary key for the AdminUserModel.
        [Key]
        public int AID { get; set; }

        // Admin username property with required validation and display name.
        [Required(ErrorMessage = "Admin username is required."), Display(Name = "Admin username")]
        public string UserName { get; set; }

        // Email property with required validation, email address format, and display name.
        [Required(ErrorMessage = "Email Address is required."), EmailAddress(ErrorMessage = "Invalid Email Address."), Display(Name = "Email Address")]
        public string Email { get; set; }

        // Password property with required validation, display name, and data type set to password.
        [Required(ErrorMessage = "Password is required."), Display(Name = "Password"), DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
