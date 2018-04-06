using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JeanStation.Models
{
    public class ProductModel
    {
        [Required]
        public int Id { get; set; }


        [Required(ErrorMessage ="Please Input The Product Id")]
        [Display(Name ="Product Id")]      
        public  int ProductId { get; set; }



        [Required(ErrorMessage = "Please Input The Product Name")]
        [Display(Name = "Product Name")]
        public  string ProductName { get; set; }



        [Required(ErrorMessage = "Please Input The Product Category")]
        [Display(Name = "Product  Category")]
        public  string Category { get; set; }



        [Required(ErrorMessage = "Please Input Details About The Product")]
        [Display(Name = "More details")]
        [DataType(DataType.MultilineText)]
        public  string Detail { get; set; }



        //[Required(ErrorMessage = "Please Choose a Picture")]
        [ScaffoldColumn(false)]
        [Display(Name = "Image")]
        [DataType(DataType.ImageUrl)]
        public  string Image { get; set; }



        [Required(ErrorMessage = "Please Input The Price")]
        [DataType(DataType.Currency,ErrorMessage ="Please Input The Correct Price")]
        [Display(Name = "Price")]
        public  float Price { get; set; }


        [Required(ErrorMessage ="please Input The Amount of The Product")]
        [Range(0, 2147483647,ErrorMessage ="Please check the amount")]
        [Display(Name = "Amount")]
        public  int Amount { get; set; }

        [Required(ErrorMessage = "please Input The Amount of The Sold Product")]
        [Range(0, 2147483647, ErrorMessage = "Please check the Sold")]
        [Display(Name = "Sold")]
        public  int Sold { get; set; }

    }
}