using JeanStation.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace JeanStation.Context
{
    public class ProductContext:DbContext
    {
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<UserAccountModel> UserAccounts { get; set; }
        public DbSet<ShoppingCartModel> ShoppingCarts { get; set; }

        public DbSet <Order> Orders{ get; set; }
    }
}