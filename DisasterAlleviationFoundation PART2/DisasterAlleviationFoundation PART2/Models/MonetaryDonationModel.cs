using System.ComponentModel.DataAnnotations; // Validation attributes

namespace DisasterAlleviationFoundation.Models
{
    // Represents the data model for monetary donations in the Disaster Alleviation Foundation.
    public class MonetaryDonationModel
    {
        // Primary key for the MonetaryDonationModel.
        [Key]
        public int DID { get; set; }

        // Amount of the monetary donation with required validation and display name.
        [Required(ErrorMessage = "Donation amount is required."), Display(Name = "Amount")]
        public double DonationAmount { get; set; }

        // Date of the monetary donation with required validation and display name.
        [Required(ErrorMessage = "Donation date is required."), Display(Name = "Date")]
        public string DonationDate { get; set; }

        // Flag indicating whether the donation is anonymous with required validation and display name.
        [Required(ErrorMessage = "Anonymous donation field is required."), Display(Name = "Anonymous Donation")]
        public bool IsAnonymous { get; set; }

        // Name of the donator.
        public string DonatorsName { get; set; }
    }
}
