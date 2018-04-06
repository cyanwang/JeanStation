using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JeanStation.Context;
using JeanStation.Models;
using System.Xml.Linq;
using System.IO;

namespace JeanStation.Controllers
{
    public class ShoppingCartController : Controller
    {
        private ProductContext db = new ProductContext();

        // GET: ShoppingCart
        public ActionResult Index()
        {
            if (Session["UserInfo"] == null)
            {
                return Content("<script>alert('请先登录');window.open('" + Url.Content("~/UserAccount/Userlogin") + "', '_self')</script>");
            }
            else
            {
                string UserID = Session["UserInfo"].ToString();
                var goods = db.ShoppingCarts.Where(g => g.UserID == UserID);
                //读总价
                int totalPrice =0;
                float finalPrice=0;
                float discount = 1;
                foreach (ShoppingCartModel s in goods)
                {
                    totalPrice += (s.UnitPrice * s.Amount);
                }
                ViewBag.TotalPrice = totalPrice;
                //读折扣       
                var path = Path.Combine(Request.MapPath("~/Data"), "discount.xml");
                XDocument xmlDoc = XDocument.Load(path);      ;
                foreach (var ele in xmlDoc.Descendants("condition"))
                {
                    //dic.Add((int)ele.Element("price"), (float)ele.Element("discount"));
                    int price=(int)ele.Element("price");
                    if (totalPrice >= price)
                    {
                        discount = (float)ele.Element("discount");
                    }
                }
                finalPrice =(discount * totalPrice);
                ViewBag.FinalPrice = finalPrice;

                //折扣结束
                return View(goods.ToList());
            }
        }

        // GET: ShoppingCart/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingCartModel shoppingCartModel = db.ShoppingCarts.Find(id);
            if (shoppingCartModel == null)
            {
                return HttpNotFound();
            }

            int productId = shoppingCartModel.ProductId;//读一下商品的图片
            var products = db.Products.Where(g => g.ProductId == productId);
            if (products.Count() == 1)
            {
                foreach (ProductModel p in products)
                {
                    //Session["picture"] = p.Image;
                    ViewBag.Image = p.Image;
                    ViewBag.Amount = p.Amount-p.Sold;
                }
            }
            return View(shoppingCartModel);
        }

        // GET: ShoppingCart/Create
        public ActionResult Create(ShoppingCartModel good)//仅仅可以被Home的details调用
        {

            if (ModelState.IsValid)
            {
                string UserID = Session["UserInfo"].ToString();
                int productId = good.ProductId;
                var products = db.ShoppingCarts.Where(g => g.UserID == UserID).Where(k => k.ProductId == productId);
                if (products.Count() == 1)//证明此类商品之前已经加入购物车
                {
                    var product2 = db.Products.Where(g=>g.ProductId==productId);//判断再加是否库存足够
                    int allmount = 0;
                    if (product2.Count() == 1)
                    {
                        foreach (ProductModel p in product2)
                        {
                            allmount=p.Amount-p.Sold;//读取当前库存
                        }
                    }
                    foreach (ShoppingCartModel s in products)
                    {
                         s.Amount += good.Amount;
                        if (s.Amount > allmount)
                        {
                            return Content("<script>alert('库存不足添加失败');window.open('" + Url.Content("~/ShoppingCart/Index") + "', '_self')</script>");
                        }
                        else
                        {
                            db.Entry(s).State = EntityState.Modified;
                        }


                        //db.Entry(s).State = EntityState.Modified;
                        //db.SaveChanges();
                        //return RedirectToAction("Index");
                    }
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                else//此类商品之前没有被该用户加入过购物车
                {
                    db.ShoppingCarts.Add(good);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }

            return View();
        }

        // POST: ShoppingCart/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,ProductId,ProductName,UserID,UnitPrice,Amount")] ShoppingCartModel shoppingCartModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.ShoppingCarts.Add(shoppingCartModel);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(shoppingCartModel);
        //}

        // GET: ShoppingCart/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingCartModel shoppingCartModel = db.ShoppingCarts.Find(id);
            if (shoppingCartModel == null)
            {
                return HttpNotFound();
            }
            return View(shoppingCartModel);
        }

        // POST: ShoppingCart/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProductId,ProductName,UserID,UnitPrice,Amount")] ShoppingCartModel shoppingCartModel)
        {
            if (ModelState.IsValid)
            {
                int productId = shoppingCartModel.ProductId;//判断再加是否库存足够
                var product2 = db.Products.Where(g => g.ProductId == productId);
                int allmount = 0;
                if (product2.Count() == 1)
                {
                    foreach (ProductModel p in product2)
                    {
                        allmount = p.Amount - p.Sold;//读取当前库存
                    }
                }

                if (shoppingCartModel.Amount<=0)
                {
                    return Content("<script>alert('请输入正确的数量');window.open('" + Url.Content("~/ShoppingCart/Index") + "', '_self')</script>");
                }
                if (shoppingCartModel.Amount > allmount)
                {
                    return Content("<script>alert('库存不足编辑购物车失败');window.open('" + Url.Content("~/ShoppingCart/Index") + "', '_self')</script>");
                }
                else
                {
                    db.Entry(shoppingCartModel).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                //db.Entry(shoppingCartModel).State = EntityState.Modified;
                //db.SaveChanges();
                //return RedirectToAction("Index");
            }
            return View(shoppingCartModel);
        }

        // GET: ShoppingCart/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingCartModel shoppingCartModel = db.ShoppingCarts.Find(id);
            if (shoppingCartModel == null)
            {
                return HttpNotFound();
            }

            int productId = shoppingCartModel.ProductId;//读一下商品的图片
            var products = db.Products.Where(g => g.ProductId == productId);
            if (products.Count() == 1)
            {
                foreach (ProductModel p in products)
                {
                    //Session["picture"] = p.Image;
                    ViewBag.Image = p.Image;
                    ViewBag.Amount = p.Amount - p.Sold;
                }
            }
            return View(shoppingCartModel);
        }

        // POST: ShoppingCart/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ShoppingCartModel shoppingCartModel = db.ShoppingCarts.Find(id);
            db.ShoppingCarts.Remove(shoppingCartModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
