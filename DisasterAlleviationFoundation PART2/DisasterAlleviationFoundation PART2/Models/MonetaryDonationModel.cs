using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace DisasterAlleviationFoundation.Models
{
    public class MonetaryDonationModel
    {
        [Key]
        public int DID { get; set; }

        [Required]
        [Display(Name = "Amount")]
        public double DonationAmount { get; set; }

        [Required]
        [Display(Name = "Date")]
        public string DonationDate { get; set; }

        [Required]
        [Display(Name = "Anonymous Donation")]
        public bool IsAnonymous { get; set; }

        public string DonatorsName { get; set; }
    }
}
