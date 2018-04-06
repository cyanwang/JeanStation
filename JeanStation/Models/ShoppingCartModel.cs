using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JeanStation.Models
{
    public class ShoppingCartModel
    {
        [Required]
        public int Id { get; set; }


        [Required]
        [Display(Name = "Product Id")]
       
        public int ProductId { get; set; }


        [Required]
        [Display(Name = "Product Name")]
        
        public string ProductName { get; set; }


        [Required]
        [Display(Name = "User Id")]
   
       
        public string UserID { get; set; }


        [Required]
       
        [Display(Name = "Unit Price")]
        public int UnitPrice { get; set; }


        [Required]
        public int Amount { get; set; }

    }
  

}