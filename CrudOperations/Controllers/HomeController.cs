using CrudOperations.Models;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace CrudOperations.Controllers
{
    public class HomeController : Controller
    {
        private CategoryContext db = new CategoryContext();

        // GET: Home/Index
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Display"); // Redirecting to Display action after creation
            }

            return View(category);
        }

        // GET: Home/Display
        public ActionResult Display()
        {
            return View(db.Categories.ToList());
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int id)
        {
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Home/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Display");
            }
            return View(category);
        }

        // GET: Home/Details/5
        public ActionResult Details(int id)
        {
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int id)
        {
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Display");
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
