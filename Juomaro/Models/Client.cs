using Juomaro.Extras;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Juomaro.Models
{
    public class Client:BasicModel
    {

        [Display(Name = "Name")]
        public String Name { get; set; }

        
        [Display(Name = "Email")]
        public String Email { get; set; }

        [Display(Name = "Phone Number")]
        public String PhoneNumber { get; set; }

        [Display(Name = "Country")]
        public String Country { get; set; }

        public ApplicationUser Merchant { get; set; }

        public string MerchantId { get; set; }
    }
}