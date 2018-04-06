using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JeanStation.Models
{
    public class UserAccountModel
    {
        [Required]
        public int Id { get; set; }



        [Required]
        public string Account { get; set; }



        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }



        [Required]
        [Compare("Password")]
        [Display(Name ="Confirm")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}