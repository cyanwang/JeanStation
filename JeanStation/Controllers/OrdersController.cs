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

namespace JeanStation.Controllers
{
    public class OrdersController : Controller
    {
        private ProductContext db = new ProductContext();

        public ActionResult UserInfomation()
        {
            string UserId = Session["UserInfo"].ToString();
            var goods = db.ShoppingCarts.Where(g => g.UserID == UserId);
            if (goods.Count() == 0)
            {
                return Content("<script>alert('你的购物车空啦，快去选点宝贝吧');window.open('" + Url.Content("~/Home/Index") + "', '_self')</script>");
            }
            return View();
        }
        [HttpPost]
        public ActionResult UserInfomation(string Address, string Phone)
        {
            if (Address == "" || Phone == "")
            {
                return Content("<script>alert('请输入地址和号码');window.open('" + Url.Content("~/Orders/UserInfomation") + "', '_self')</script>");

            }
            else
            {
                Session["Address"] = Address;
                Session["Phone"] = Phone;
                return RedirectToAction("Create", "Orders");
            }
        }

        // GET: Orders
        public ActionResult Index()
        {
            if (Session["UserInfo"] == null)
            {
                return Content("<script>alert('请先登录');window.open('" + Url.Content("~/UserAccount/Userlogin") + "', '_self')</script>");
            }
            if (Session["UserInfo"].ToString() == "wangyan")
            {
                return View(db.Orders.ToList());
            }
            else
            {
                string UserId = Session["UserInfo"].ToString();
                //  var orders = db.Orders.Where(g=>g.UserId==UserId);
                return View(db.Orders.Where(g => g.UserId == UserId));
            }

        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            string UserID = Session["UserInfo"].ToString();
            long Phone =long.Parse((Session["Phone"]).ToString()); 
            var orders = db.ShoppingCarts.Where(g => g.UserID == UserID);
            int orderId = db.Orders.Count();
            //  Order Order1 = new Order();
            foreach (ShoppingCartModel o in orders)
            {
                Order Order1 = new Order();
                Order1.OrderId = orderId + 1;
                orderId++;
                Order1.ProductId = o.ProductId;
                Order1.Amount = o.Amount;
                Order1.Price = (o.UnitPrice * o.Amount);
                Order1.UserId = o.UserID;
                Order1.UserAddress = Session["Address"].ToString();
                Order1.UserPhone = Phone;
                Order1.Status = "买家已经付款";
                if (ModelState.IsValid)
                {
                    db.Orders.Add(Order1);
                    db.ShoppingCarts.Remove(o);
                }
                else
                {
                    return Content("<script>alert('order的create的格式有问题');window.open('" + Url.Content("~/Orders/UserInfomation") + "', '_self')</script>");
                }

            }
            db.SaveChanges();

            return Content("<script>alert('订单已经提交,购物车已经清空');window.open('" + Url.Content("~/ShoppingCart/Index") + "', '_self')</script>");
        }

        // POST: Orders/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,OrderId,ProductId,Amount,Price,UserId,UserAddress,UserPhone,Status")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,OrderId,ProductId,Amount,Price,UserId,UserAddress,UserPhone,Status")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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