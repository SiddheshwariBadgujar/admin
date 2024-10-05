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
        private const int PageSize = 10; // Set the desired page size

        // GET: Product/Create
        public ActionResult Create()
        {
            // Correctly creating the SelectList
            ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "CId", "CName");
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Display", "Product"); // Redirect to the Display action
            }

            // Ensure the SelectList is populated again in case of an error
            ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "CId", "CName", product.CategoryId);
            return View(product);
        }

        // GET: Product/Display
        // GET: Product/Display
        public ActionResult Display(int page = 1)
        {
            var totalProducts = db.Products.Count(); // Get the total count of products
            var totalPages = (int)Math.Ceiling((double)totalProducts / PageSize); // Calculate total pages

            // Fetch products for the current page
            var products = db.Products
                .Include(p => p.Category)
                .OrderBy(p => p.ProductId)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            ViewBag.TotalPages = totalPages; // Pass total pages to the view
            ViewBag.CurrentPage = page; // Pass current page to the view

            return View(products);
        }


        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            var product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            // Populate categories for the dropdown
            ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "CId", "CName", product.CategoryId);
            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Display", "Product"); // Redirect to Display after editing
            }

            // Ensure the SelectList is populated again in case of an error
            ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "CId", "CName", product.CategoryId);
            return View(product);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            var product = db.Products.Include(p => p.Category).SingleOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            var product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Display", "Product"); // Redirect to Display after deletion
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
