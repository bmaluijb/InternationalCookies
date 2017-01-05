using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InternationalCookies.Data.Models
{
    public class Register
    {
        [Required]  
        [Display(Name = "Store name")]
        public string StoreName { get; set; }

        [Required]
        [Display(Name = "Store country")]
        public string StoreCountry { get; set; }

        [Required]
        [Display(Name = "Where do you want to store your data?")]
        public string StoreDataRegions { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email-address")]
        public string PersonEmail { get; set; }

        [Required]
        [Display(Name = "First name")]
        public string PersonFirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string PersonLastName { get; set; }
    }
}
