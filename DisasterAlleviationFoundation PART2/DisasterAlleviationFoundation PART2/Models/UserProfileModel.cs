using System.ComponentModel.DataAnnotations; // Validation attributes

namespace DisasterAlleviationFoundation.Models
{
    // Represents the data model for user profiles in the Disaster Alleviation Foundation.
    public class UserProfileModel
    {
        // Primary key for the UserProfileModel.
        [Key]
        public int UID { get; set; }

        // Username property with required validation and display name.
        [Required(ErrorMessage = "Username is required."), Display(Name = "Username")]
        public string UserName { get; set; }

        // Email property with required validation, email address format, and display name.
        [Required(ErrorMessage = "Email Address is required."), EmailAddress(ErrorMessage = "Invalid Email Address."), Display(Name = "Email Address")]
        public string Email { get; set; }

        // Password property with required validation, data type set to password, and display name.
        [Required(ErrorMessage = "Password is required."), Display(Name = "Password"), DataType(DataType.Password)]
        public string Password { get; set; }

        // Confirm Password property with required validation, data type set to password, display name, and comparison to the Password property.
        [Required(ErrorMessage = "Confirm Password is required."), Display(Name = "Confirm Password"), DataType(DataType.Password), Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string Cpassword { get; set; }
    }
}
