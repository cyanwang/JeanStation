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
    public class UserAccountController : Controller
    {
        private ProductContext db = new ProductContext();

        public ActionResult Logout()//登出清除session信息
        {
            Session.Remove("UserInfo");
            return Content("<script>alert('登出成功');window.open('" + Url.Content("~/Home/Index") + "', '_self')</script>");
        }
        [HttpGet]
        public ActionResult Userlogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Userlogin(string account,string pwd)
        {
            var users = db.UserAccounts.Where(g=>g.Account==account);
            if (users.Count() ==1)
            {
                foreach (var u in users)
                {
                    if (u.Password == pwd)
                    { 
                        Session["UserInfo"] = account;
                        return Content("<script>alert('登陆成功');window.open('" + Url.Content("~/Home/Index") + "', '_self')</script>");
                    }
                    else
                    {
                        return Content("<script>alert('密码错误');window.open('" + Url.Content("~/UserAccount/Userlogin") + "', '_self')</script>");
                    }
                }
            }
            else
            {
                return Content("<script>alert('该用户不存在');window.open('" + Url.Content("~/UserAccount/Userlogin") + "', '_self')</script>");
            }
             
                
                
            return View();
        }

        // GET: UserAccount
        public ActionResult Index()
        {
            return View(db.UserAccounts.ToList());
        }

        // GET: UserAccount/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAccountModel userAccountModel = db.UserAccounts.Find(id);
            if (userAccountModel == null)
            {
                return HttpNotFound();
            }
            return View(userAccountModel);
        }

        // GET: UserAccount/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserAccount/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Account,Password,ConfirmPassword")] UserAccountModel userAccountModel)
        {
            var users= db.UserAccounts.Where(m => m.Account == userAccountModel.Account);
            if (users.Count() == 0)
            {
                if (ModelState.IsValid)
                {
                    db.UserAccounts.Add(userAccountModel);
                    db.SaveChanges();
                    Session["UserInfo"] = userAccountModel.Account;
                    return Content("<script>alert('注册成功');window.open('" + Url.Content("~/Home/Index") + "', '_self')</script>");
                }
            }
            else
            {
                return Content("<script>alert('该用户已经存在');window.open('" + Url.Content("~/UserAccount/Create") + "', '_self')</script>");
            }

            return View(userAccountModel);
        }

        // GET: UserAccount/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAccountModel userAccountModel = db.UserAccounts.Find(id);
            if (userAccountModel == null)
            {
                return HttpNotFound();
            }
            return View(userAccountModel);
        }

        // POST: UserAccount/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Account,Password,ConfirmPassword")] UserAccountModel userAccountModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userAccountModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userAccountModel);
        }

        // GET: UserAccount/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAccountModel userAccountModel = db.UserAccounts.Find(id);
            if (userAccountModel == null)
            {
                return HttpNotFound();
            }
            return View(userAccountModel);
        }

        // POST: UserAccount/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserAccountModel userAccountModel = db.UserAccounts.Find(id);
            db.UserAccounts.Remove(userAccountModel);
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
