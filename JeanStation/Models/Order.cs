using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JeanStation.Models
{
    public class Order
    {
        [Required]
        public int Id { get; set; }


        [Required]
        public int OrderId { get; set; }


        [Required]
        public int ProductId { get; set; }


        [Required]
        public int Amount { get; set; }


        [Required]
        [DataType(DataType.Currency)]
        public int Price { get; set; }


        [Required]
        public string UserId { get; set; }



        [Required]
        [DataType(DataType.MultilineText)]
        public string UserAddress { get; set; }



        [Required]       
        public long UserPhone{ get; set; }


        [Required]
        public string Status { get; set; }


    }
}