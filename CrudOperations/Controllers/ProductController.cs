using CrudOperations.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace CrudOperations.Controllers
{
    public class ProductController : Controller
    {
        private ProductContext db = new ProductContext();
        private const int PageSize = 10; 

       
        public ActionResult Create()
        {
            
            ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "CId", "CName");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Display", "Product"); 
            }

            
            ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "CId", "CName", product.CategoryId);
            return View(product);
        }

       
        public ActionResult Display(int page = 1)
        {
            var totalProducts = db.Products.Count(); 
            var totalPages = (int)Math.Ceiling((double)totalProducts / PageSize); 

           
            var products = db.Products
                .Include(p => p.Category)
                .OrderBy(p => p.ProductId)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            ViewBag.TotalPages = totalPages; 
            ViewBag.CurrentPage = page; 

            return View(products);
        }


       
        public ActionResult Edit(int id)
        {
            var product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

   
            ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "CId", "CName", product.CategoryId);
            return View(product);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Display", "Product"); 
            }

           
            ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "CId", "CName", product.CategoryId);
            return View(product);
        }

        public ActionResult Details(int id)
        {
            var product = db.Products.Include(p => p.Category).SingleOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

       
        public ActionResult Delete(int id)
        {
            var product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Display", "Product"); 
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
