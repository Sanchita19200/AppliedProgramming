using Microsoft.AspNetCore.Cors; // CORS attribute for enabling Cross-Origin Resource Sharing
using Microsoft.AspNetCore.Mvc.ActionConstraints; // Action Constraints for MVC actions
using System;
using System.ComponentModel.DataAnnotations; // Validation attributes
using System.Drawing;

namespace DisasterAlleviationFoundation.Models
{
    // Represents the data model for a disaster in the Disaster Alleviation Foundation.
    public class DisasterModel
    {
        // Primary key for the DisasterModel.
        [Key]
        public int DID { get; set; }

        // Name of the disaster with required validation and display name.
        [Required(ErrorMessage = "Disaster name is required."), Display(Name = "Enter Disaster Name")]
        public string DisasterName { get; set; }

        // Description of the disaster with required validation and display name.
        [Required(ErrorMessage = "Disaster description is required."), Display(Name = "Enter Description of Disaster")]
        public string DisasterDescription { get; set; }

        // Location of the disaster (e.g., Address) with required validation and display name.
        [Required(ErrorMessage = "Location is required."), Display(Name = "Enter Location of the Disaster")]
        public string Location { get; set; }

        // Type of aid required with required validation and display name.
        [Required(ErrorMessage = "Aid type is required."), Display(Name = "Choose AID Required")]
        public string AidType { get; set; }

        // Start date of the disaster with required validation and display name.
        [Required(ErrorMessage = "Start date is required."), Display(Name = "Disaster Start Date")]
        public DateTime StartDate { get; set; }

        // End date of the disaster with required validation and display name.
        [Required(ErrorMessage = "End date is required."), Display(Name = "Disaster End Date")]
        public DateTime EndDate { get; set; }

        // Properties for allowing the admin to allocate funds for the disaster.

        // ID of the disaster for fund allocation with required validation and display name.
        [Required(ErrorMessage = "Disaster ID is required."), Display(Name = "Enter Disaster ID for Fund Allocation")]
        public int Disaster_id { get; set; }

        // Amount to allocate for the disaster with required validation and display name.
        [Required(ErrorMessage = "Allocation amount is required."), Display(Name = "Enter Allocation Amount")]
        public int AllocationAmount { get; set; }

        // Properties for allowing the user to allocate goods for the disaster.

        // ID of the disaster for goods allocation with required validation and display name.
        [Required(ErrorMessage = "Disaster ID is required."), Display(Name = "Enter The Disaster ID for Goods Allocation")]
        public int goodsID { get; set; }

        // Type of goods to allocate with required validation and display name.
        [Required(ErrorMessage = "Goods type is required."), Display(Name = "Select Available Goods")]
        public string GoodType { get; set; }
    }
}
