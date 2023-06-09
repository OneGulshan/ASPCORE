using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CommonHelper;
using DataAccessLayer.Infrastructure.IRepository;
using Models.ViewModels;

namespace ShoppingCart.Areas.Admin.Controllers
{
    [Area("Admin")]//Agar hamein alag se saperately define karna hai controller ko in perticular area to ham aise karenge
    [Authorize(Roles = WebSiteRole.Role_Admin)]
    public class CategoryController : Controller
    {        
        private IUnitOfWork _uw;
        
        public CategoryController(IUnitOfWork uw)
        {
            _uw = uw;
        }

        CategoryVM vm = new CategoryVM();

        public IActionResult Index()
        {            
            vm.categories = _uw.Category.GetAll();
            return View(vm);
        }
        //[HttpGet]
        //public IActionResult Create()
        //{
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(Category category)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _uw.Category.Add(category);
        //        _uw.Save();
        //        TempData["success"] = "Category Created Done!";
        //        return RedirectToAction("Index");
        //    }
        //    return View(category);
        //}
        [HttpGet]//hamara data actually me id ke base par kaam karta hai agar id hamari 0 hoti hai to EF core use bydefault create kar deta hai or agar id 1 ya greater hoti hai to vo use Update kar deta hai, we does,nt required seprate methods we can use both by combined
        public IActionResult CreateUpdate(int? id)
        {
            //Category category = new Category();//yahan directly Model View ma ja rha hai agar hamne Model me kuch changes kar die to hamein View me bhi changes karne padenge to ye tightly bind ho gaya isliye hamein ViewModel banana hota hai for sepration of concirn            

            if (id == null || id == 0)
            {
                return View(vm);//here we can't bind Category by diretly we required a obj of Category here then we can bind
            }
            else
            {
                vm.Category = _uw.Category.GetT(x => x.Id == id);
                if (vm.Category == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(vm);
                }                
            }                        
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUpdate(CategoryVM vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.Category.Id == 0) 
                {
                    _uw.Category.Add(vm.Category);//vm se category me insert kara rhe hai
                    TempData["success"] = "Category Created Done!";
                }
                else
                {
                    _uw.Category.Update(vm.Category);
                    TempData["success"] = "Category Updated Done!";
                }
                _uw.Save();
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
            var category = _uw.Category.GetT(x => x.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteDate(int? id)
        {
            var category = _uw.Category.GetT(x => x.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            _uw.Category.Delete(category);
            _uw.Save();
            TempData["success"] = "Category Deleted Done!";
            return RedirectToAction("Index");
        }
    }
}
