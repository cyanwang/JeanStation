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
using System.IO;

namespace JeanStation.Controllers
{
    public class ProductController : Controller
    {
        private ProductContext db = new ProductContext();

        // GET: Product
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        // GET: Product/Details/5
        public ActionResult Details(int? id)
        {
           // ProductContext db = new ProductContext();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductModel productModel = db.Products.Find(id);
            if (productModel == null)
            {
                return HttpNotFound();
            } 
            return View(productModel);
        }

        // GET: Product/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        
        // POST: Product/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProductId,ProductName,Category,Detail,Image,Price,Amount,Sold")] ProductModel productModel, HttpPostedFileBase file)
        {
          
            List <ProductModel> models = db.Products.Where(m => m.ProductId == productModel.ProductId).ToList();
            if (models.Count==0)
            {
                if (ModelState.IsValid)
                {
                    if (file == null) { return Content("<script>alert('请为你的宝贝选一张图片');window.open('" + Url.Content("~/Product/Create") + "', '_self')</script>"); }
                    var fileName = Path.Combine(Request.MapPath("~/Images"), Path.GetFileName(file.FileName));
                     try
                        {
                        file.SaveAs(fileName);  //把在电脑里面选择的图片保存在当前程序的Images文件目录下                   
                                                //  productModel.Image = "../Images/" + Path.GetFileName(file.FileName); 
                        productModel.Image = file.FileName;//仅仅保存图片的名字

                        db.Products.Add(productModel);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                        //return Content("上传成功！", "text/plain");
                        //  return RedirectToAction("Show", tm);
                    }
                    catch
                    {
                        return Content("<script>alert('上传异常');window.open('" + Url.Content("~/Product/Create") + "', '_self')</script>");
                    }
                    //db.Products.Add(productModel);
                    //db.SaveChanges();
                    //return RedirectToAction("Index");
                }
                else
                {
                    return Content("<script>alert('输入商品的格式有错误');window.open('" + Url.Content("~/Product/Create") + "', '_self')</script>");
                }
            }
            else
            {
                return Content("<script>alert('该编号已经存在');window.open('" + Url.Content("~/Product/Create") + "', '_self')</script>");
            }
            //if (ModelState.IsValid)
            //{
            //    db.Products.Add(productModel);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            //return View(productModel);
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductModel productModel = db.Products.Find(id);
            if (productModel == null)
            {
                return HttpNotFound();
            }
        
            return View(productModel);
        }

        // POST: Product/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProductId,ProductName,Category,Detail,Image,Price,Amount,Sold")] ProductModel productModel, HttpPostedFileBase file)
        {
            var fileName = Path.Combine(Request.MapPath("~/Images"), Path.GetFileName(file.FileName));
            if (ModelState.IsValid)
            {
                if (file == null)
                {
                    db.Entry(productModel).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else {
                    file.SaveAs(fileName);
                    productModel.Image = file.FileName;
                    db.Entry(productModel).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            
            return View(productModel);
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductModel productModel = db.Products.Find(id);
            if (productModel == null)
            {
                return HttpNotFound();
            }
            return View(productModel);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductModel productModel = db.Products.Find(id);
            db.Products.Remove(productModel);
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
