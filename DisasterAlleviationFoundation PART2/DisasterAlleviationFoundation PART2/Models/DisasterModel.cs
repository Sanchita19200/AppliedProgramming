using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace DisasterAlleviationFoundation.Models
{
    public class DisasterModel
    {
        [Key]
        public int DID { get; set; }
        [Required]
        [Display(Name ="Enter Disastername")]
        public string DisasterName { get; set; }

        [Required]
        [Display(Name = "Enter Description of Disaster")]
        public string DisasterDescription { get; set;}

        [Required]
        [Display(Name= "Enter location of the disaster e.g Address etc")]
        public string Location { get; set;} 

        [Required]
        [Display(Name = "Choose AID required")]
        public string AidType { get; set; }

        [Required]
        [Display(Name ="Disaster Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name ="Disaster End Date")]
        public DateTime EndDate { get; set; }


        //This is going to be used allow the admin to allocate funds for the disaster//

        [Required]
        [Display(Name ="Enter Disaster ID you want to allocate funds to")]
        public int Disaster_id { get; set; }

        [Required]
        [Display(Name ="Enter Amount")]
        public int AllocationAmount { get; set; }


        //This is to allow the user to allocate goods for the disaster//
        [Required]
        [Display(Name = "Enter The Disaster ID")]
        public int goodsID { get; set; }

        [Required]
        [Display(Name = "Select Available Goods")]
        public string GoodType { get; set; }
    }
}
