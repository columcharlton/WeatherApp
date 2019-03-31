using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using System.Web;
using System.Web.UI.WebControls;    //Need for validation summary

namespace SimpleLogin.Models
{
    public class UserModel
    {

        [Required]
        [EmailAddress]
        [StringLength (150)]
        [Display(Name ="Email address: ")]
        public string Email { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6)]
        [Display(Name ="Passord: ")]
        public string Password { get; set; }

        [Display(Name = "Remember on this computer")]
        public bool RememberMe { get; set; }
    }
}