using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace ShoppingCart.Areas.Admin.Controllers
{
    public class CategorysController : Controller
    {
        private AppDbContext _db;

        public CategorysController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> categories = _db.Categories;
            return View(categories);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]//jab hamara form submit hota to vo actualy me ek token generate karta hai tab ye tocken hamara xss attack se hamein prevent karta hai -> ex. jab ham source code get karke form bana kar post karenge tab antiforgery hamein prevent karega hamara form post nahi hoga
        public IActionResult Create(Category category)
        {//ValidateAntiForgeryToken work with model state if model all states is validate IsValid then our conditional code execute
            if (ModelState.IsValid)
            {
                _db.Categories.Add(category);
                _db.SaveChanges();
                TempData["success"] = "Category Created Done!";//TempData works for only on single request after single request TempData dismiss
                return RedirectToAction("Index");
            }
            return View(category);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var category = _db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(category);//EF Update/Add etc methods works automatically
                _db.SaveChanges();
                TempData["success"] = "Category Updated Done!";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var category = _db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteDate(int? id)//here method name changed but called Delete ActionName declared above
        {
            var category = _db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(category);//yahan ham categories me se single category remove kara rahe hain
            _db.SaveChanges();
            TempData["success"] = "Category Deleted Done!";
            return RedirectToAction("Index");
        }
    }
}
