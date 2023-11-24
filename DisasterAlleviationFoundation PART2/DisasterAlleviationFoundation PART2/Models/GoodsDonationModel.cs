using Microsoft.AspNetCore.Cors;
using System.ComponentModel.DataAnnotations;

namespace DisasterAlleviationFoundation.Models
{
    public class GoodsDonationModel
    {
        [Key]
        public int GID { get; set; }

        [Required]
        [Display(Name ="Number of Items")]
        public int numberofItems { get; set; }

        [Required]
        [Display(Name ="Good's Name")]
        public string Goodname { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string Gooddescription { get;set;}

        [Required]
        [Display(Name ="Product Category")]
        public string category { get; set; }
       
        [Required]
        [Display(Name = "Anonymous Donation")]
        public bool IsAnonymous { get; set; }

        public string customCategory { get; set; } // Added property for custom category

        [Required]
        [Display(Name ="Date")]
        public DateTime Date { get; set; }



        //This is for displaying to the admin//
        public string DonatorsName { get; set; }
        public string Category { get; set; }

        [Required]
        [Display(Name = "Enter The Price of the Good")]
        public int GoodsPrice { get; set;}
    }
}
