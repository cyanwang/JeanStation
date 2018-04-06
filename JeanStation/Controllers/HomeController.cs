using JeanStation.Context;
using JeanStation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JeanStation.Controllers
{
    public class HomeController : Controller
    {
        ProductContext db = new ProductContext();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult malecloth()
        {
            var products = db.Products.Where(g=>g.Category=="男上衣");
            string strResponse = "<table style='width:100%; padding:30px;'>";
            foreach (var good in products)
            {
                strResponse += "<tr>";

                strResponse += "<td>";//一级
                //  "<img src='../../Images/Star.png' style='height:20px; width:20px;'/>";               
                strResponse += "<img src='../Images/" + good.Image.ToString() + "'/>";
                //strResponse += "<img src='../Images/1.jpg'/>";
                strResponse += "</td>";

                strResponse += "<td >";//二级

                strResponse += "<table >";//<table>

                strResponse += "<tr>";
                    strResponse += "<td>";
                //strResponse += "添加到购物车:<input type='submit' id='"+good.ProductId+"'  value='"+good.ProductId+ "'  name='action' />";
                strResponse += "<a href='/Home/ProductDetails?productId="+good.ProductId+"'>"+"查看详细信息"+"</a>";
         strResponse += "</td>";
                strResponse += "</tr>";

                strResponse += "<tr>";
                   strResponse += "<td>";
                       strResponse += "<h4>商品名称：" + good.ProductName + "</h4><br/>";
                   strResponse += "</td>";
                strResponse += "</tr>";
             
              
                strResponse += "<tr>";
                  strResponse += "<td>";
                     strResponse += "<h4>价格：" + good.Price + "</h4><br/>";
                strResponse += "</td>";
                strResponse += "</tr>";

                strResponse += "<tr>";
                strResponse += "<td>";
                strResponse += "<h4>已售：" + good.Sold+ "</h4><br/>";
                strResponse += "</td>";
                strResponse += "</tr>";

                strResponse += "<tr>";
                strResponse += "<td>";
                strResponse += "<h4>库存：" + good.Amount + "</h4><br/>";
                strResponse += "</td>";
                strResponse += "</tr>";

                strResponse += "</table>";//</table>

                strResponse += "</td>";

                strResponse += "</tr>";
            }
            strResponse += "</table>";
            ViewBag.products = strResponse;
            return View();
        }
        [HttpGet]
        public ActionResult womancloth()
        {
            var products = db.Products.Where(g => g.Category == "女上衣");
            string strResponse = "<table style='width:100%; padding:30px;'>";
            foreach (var good in products)
            {
                strResponse += "<tr>";

                strResponse += "<td>";//一级
                //  "<img src='../../Images/Star.png' style='height:20px; width:20px;'/>";               
                strResponse += "<img src='../Images/" + good.Image.ToString() + "'/>";
                //strResponse += "<img src='../Images/1.jpg'/>";
                strResponse += "</td>";

                strResponse += "<td >";//二级

                strResponse += "<table >";//<table>

                strResponse += "<tr>";
                strResponse += "<td>";
                //strResponse += "添加到购物车:<input type='submit' id='"+good.ProductId+"'  value='"+good.ProductId+ "'  name='action' />";
                strResponse += "<a href='/Home/ProductDetails?productId=" + good.ProductId + "'>" + "查看详细信息" + "</a>";
                strResponse += "</td>";
                strResponse += "</tr>";

                strResponse += "<tr>";
                strResponse += "<td>";
                strResponse += "<h4>商品名称：" + good.ProductName + "</h4><br/>";
                strResponse += "</td>";
                strResponse += "</tr>";


                strResponse += "<tr>";
                strResponse += "<td>";
                strResponse += "<h4>价格：" + good.Price + "</h4><br/>";
                strResponse += "</td>";
                strResponse += "</tr>";

                strResponse += "<tr>";
                strResponse += "<td>";
                strResponse += "<h4>已售：" + good.Sold + "</h4><br/>";
                strResponse += "</td>";
                strResponse += "</tr>";

                strResponse += "</table>";//</table>

                strResponse += "</td>";

                strResponse += "</tr>";
            }
            strResponse += "</table>";
            ViewBag.products = strResponse;
            return View();
        }
        [HttpGet]
        public ActionResult mantrousers()
        {
            var products = db.Products.Where(g => g.Category == "男裤");
            string strResponse = "<table style='width:100%; padding:30px;'>";
            foreach (var good in products)
            {
                strResponse += "<tr>";

                strResponse += "<td>";//一级
                //  "<img src='../../Images/Star.png' style='height:20px; width:20px;'/>";               
                strResponse += "<img src='../Images/" + good.Image.ToString() + "'/>";
                //strResponse += "<img src='../Images/1.jpg'/>";
                strResponse += "</td>";

                strResponse += "<td >";//二级

                strResponse += "<table >";//<table>

                strResponse += "<tr>";
                strResponse += "<td>";
                //strResponse += "添加到购物车:<input type='submit' id='"+good.ProductId+"'  value='"+good.ProductId+ "'  name='action' />";
                strResponse += "<a href='/Home/ProductDetails?productId=" + good.ProductId + "'>" + "查看详细信息" + "</a>";
                strResponse += "</td>";
                strResponse += "</tr>";

                strResponse += "<tr>";
                strResponse += "<td>";
                strResponse += "<h4>商品名称：" + good.ProductName + "</h4><br/>";
                strResponse += "</td>";
                strResponse += "</tr>";


                strResponse += "<tr>";
                strResponse += "<td>";
                strResponse += "<h4>价格：" + good.Price + "</h4><br/>";
                strResponse += "</td>";
                strResponse += "</tr>";

                strResponse += "<tr>";
                strResponse += "<td>";
                strResponse += "<h4>已售：" + good.Sold + "</h4><br/>";
                strResponse += "</td>";
                strResponse += "</tr>";

                strResponse += "</table>";//</table>

                strResponse += "</td>";

                strResponse += "</tr>";
            }
            strResponse += "</table>";
            ViewBag.products = strResponse;
            return View();
        }
        [HttpGet]
        public ActionResult womantrousers()
        {
            var products = db.Products.Where(g => g.Category == "女裤");
            string strResponse = "<table style='width:100%; padding:30px;'>";
            foreach (var good in products)
            {
                strResponse += "<tr>";

                strResponse += "<td>";//一级
                //  "<img src='../../Images/Star.png' style='height:20px; width:20px;'/>";               
                strResponse += "<img src='../Images/" + good.Image.ToString() + "'/>";
                //strResponse += "<img src='../Images/1.jpg'/>";
                strResponse += "</td>";

                strResponse += "<td >";//二级

                strResponse += "<table >";//<table>

                strResponse += "<tr>";
                strResponse += "<td>";
                //strResponse += "添加到购物车:<input type='submit' id='"+good.ProductId+"'  value='"+good.ProductId+ "'  name='action' />";
                strResponse += "<a href='/Home/ProductDetails?productId=" + good.ProductId + "'>" + "查看详细信息" + "</a>";
                strResponse += "</td>";
                strResponse += "</tr>";

                strResponse += "<tr>";
                strResponse += "<td>";
                strResponse += "<h4>商品名称：" + good.ProductName + "</h4><br/>";
                strResponse += "</td>";
                strResponse += "</tr>";


                strResponse += "<tr>";
                strResponse += "<td>";
                strResponse += "<h4>价格：" + good.Price + "</h4><br/>";
                strResponse += "</td>";
                strResponse += "</tr>";

                strResponse += "<tr>";
                strResponse += "<td>";
                strResponse += "<h4>已售：" + good.Sold + "</h4><br/>";
                strResponse += "</td>";
                strResponse += "</tr>";

                strResponse += "</table>";//</table>

                strResponse += "</td>";

                strResponse += "</tr>";
            }
            strResponse += "</table>";
            ViewBag.products = strResponse;
            return View();
        }

        [HttpGet]
        public ActionResult other()
        {
            var products = db.Products.Where(g => g.Category == "其他");
            string strResponse = "<table style='width:100%; padding:30px;'>";
            foreach (var good in products)
            {
                strResponse += "<tr>";

                strResponse += "<td>";//一级
                //  "<img src='../../Images/Star.png' style='height:20px; width:20px;'/>";               
                strResponse += "<img src='../Images/" + good.Image.ToString() + "'/>";
                //strResponse += "<img src='../Images/1.jpg'/>";
                strResponse += "</td>";

                strResponse += "<td >";//二级

                strResponse += "<table >";//<table>

                strResponse += "<tr>";
                strResponse += "<td>";
                //strResponse += "添加到购物车:<input type='submit' id='"+good.ProductId+"'  value='"+good.ProductId+ "'  name='action' />";
                strResponse += "<a href='/Home/ProductDetails?productId=" + good.ProductId + "'>" + "查看详细信息" + "</a>";
                strResponse += "</td>";
                strResponse += "</tr>";

                strResponse += "<tr>";
                strResponse += "<td>";
                strResponse += "<h4>商品名称：" + good.ProductName + "</h4><br/>";
                strResponse += "</td>";
                strResponse += "</tr>";


                strResponse += "<tr>";
                strResponse += "<td>";
                strResponse += "<h4>价格：" + good.Price + "</h4><br/>";
                strResponse += "</td>";
                strResponse += "</tr>";

                strResponse += "<tr>";
                strResponse += "<td>";
                strResponse += "<h4>已售：" + good.Sold + "</h4><br/>";
                strResponse += "</td>";
                strResponse += "</tr>";

                strResponse += "</table>";//</table>

                strResponse += "</td>";

                strResponse += "</tr>";
            }
            strResponse += "</table>";
            ViewBag.products = strResponse;
            return View();
        }

        [HttpGet]
        public ActionResult ProductDetails(int productId)
        {
            //ProductModel product = db.Products.Find(productId);
            var products = db.Products.Where(g => g.ProductId==productId);
            if (products.Count() == 1)
            {
                return View(products);
            }
            else
            {
                return HttpNotFound();
            }
           
            //ViewBag.Id = productId;
               
        }
        [HttpPost]
        public ActionResult ProductDetails(int? number,string submit)
        {
            if (Session["UserInfo"] == null)//判断是否登陆
            {
                return Content("<script>alert('您尚未登陆请先登录');window.open('" + Url.Content("~/UserAccount/Userlogin") + "', '_self')</script>");
            }
            else
            {
                int productId1 = (int)Session["ProductId"];
                string UserID1 = Session["UserInfo"].ToString();
                if (number.HasValue && number > 0)//判断用户的购买量是否正确
                {
                    
                    ShoppingCartModel good = new ShoppingCartModel();
                  
                    var product = db.Products.Where(g => g.ProductId ==productId1);
                    if (product.Count() == 1)
                    {
                       // int num = db.Products.Count();
                        foreach (ProductModel p in product)
                        {
                            if ((p.Sold + number) > p.Amount)//判断是否有货
                            {
                                return Content("<script>alert('库存不足');window.open('" + Url.Content("~/Home/Index") + "', '_self')</script>");
                            }                                                      
                           //所有的情况都正常的情况下保存一下此次购物车的信息
                                good.ProductId = Convert.ToInt32(Session["ProductId"]);
                                good.ProductName = p.ProductName;
                                good.UserID = Session["UserInfo"].ToString();
                                good.UnitPrice = (int)p.Price;
                                good.Amount = (int)number;
                                return RedirectToAction("Create", "ShoppingCart", good);
                            
                        }
                       
                       
                    }
                    else
                    {
                        return Content("<script>alert('数据库出现异常');window.open('" + Url.Content("~/Home/Index") + "', '_self')</script>");
                    }
                }
                else
                {
                      return Content("<script>alert('至少添加一件商品');window.open('" + Url.Content("~/Home/Index") + "', '_self')</script>");
                }
            }
            return View();
        }

       
    }
}