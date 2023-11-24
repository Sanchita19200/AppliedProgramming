using Microsoft.AspNetCore.Cors; // CORS attribute for enabling Cross-Origin Resource Sharing
using System;
using System.ComponentModel.DataAnnotations; // Validation attributes

namespace DisasterAlleviationFoundation.Models
{
    // Represents the data model for goods donation in the Disaster Alleviation Foundation.
    public class GoodsDonationModel
    {
        // Primary key for the GoodsDonationModel.
        [Key]
        public int GID { get; set; }

        // Number of items being donated with required validation and display name.
        [Required(ErrorMessage = "Number of items is required."), Display(Name = "Number of Items")]
        public int numberofItems { get; set; }

        // Name of the donated good with required validation and display name.
        [Required(ErrorMessage = "Good name is required."), Display(Name = "Good's Name")]
        public string Goodname { get; set; }

        // Description of the donated good with required validation and display name.
        [Required(ErrorMessage = "Good description is required."), Display(Name = "Description")]
        public string Gooddescription { get; set; }

        // Category of the donated good with required validation and display name.
        [Required(ErrorMessage = "Product category is required."), Display(Name = "Product Category")]
        public string category { get; set; }

        // Flag indicating whether the donation is anonymous with required validation and display name.
        [Required(ErrorMessage = "Anonymous donation field is required."), Display(Name = "Anonymous Donation")]
        public bool IsAnonymous { get; set; }

        // Property for a custom category, if needed.
        public string customCategory { get; set; }

        // Date of the donation with required validation and display name.
        [Required(ErrorMessage = "Date is required."), Display(Name = "Date")]
        public DateTime Date { get; set; }

        // Properties for displaying to the admin.

        // Name of the donator.
        public string DonatorsName { get; set; }

        // Category of the donation.
        public string Category { get; set; }

        // Price of the donated good with required validation and display name.
        [Required(ErrorMessage = "Price of the good is required."), Display(Name = "Enter The Price of the Good")]
        public int GoodsPrice { get; set; }
    }
}
