using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using CommonHelper;
using DataAccessLayer.Infrastructure.IRepository;
using Models.ViewModels;

namespace ShoppingCart.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = WebSiteRole.Role_Admin)]
    public class ProductController : Controller
    {        
        private IUnitOfWork _uw;
        // IWebHostEnvironment's prop webroot helping us to reaching in wwwroot folder
        private IWebHostEnvironment _hostEnvironment;// this reach us to wwwroot folder for picking files from ProductImage folder, this use by DI        
        public ProductController(IUnitOfWork uw, IWebHostEnvironment hostEnvironment)
        {
            _uw = uw;
            _hostEnvironment = hostEnvironment;
        }
        /*jQuery use karne ke liye hamne yahan ek API ka region banaya hai*/
        #region APICALL
        public IActionResult AllProducts()// hamein yahan View ke ander direct VM na bhej kar json result bhejna hoga taaki ham js ke throw us json formate ko read kar saken
        {
            var products = _uw.Product.GetAll(includeProperties: "Category");// yahan hamye Category get kar li by Model name, char array type data show hoga yahan hamein category ka in json result
            return Json(new { data = products });// here in Json our Product data is init in arry type of data obj, using js ajax method we can easily read this Json data formate
        }
        #endregion

        public IActionResult Index()
        {
            // ProductVM vm = new ProductVM();
            // vm.Products = _uw.Product.GetAll();
            return View();
        }        
        [HttpGet]
        public IActionResult CreateUpdate(int? id)
        {
            ProductVM vm = new ProductVM()
            {
                Product = new(),// Product init here(from 0/1 some values here)
                Categories = _uw.Category.GetAll().Select(x=>
                new SelectListItem()// here sari categories nakal ke, select kar SelectListItem me bind kari hai, using its Text and value properties, Selected prop is also its for selecting categories, bydefault also here we can by selected prop
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
            };
            if (id == null || id == 0)
            {
                return View(vm);
            }
            else
            {
                vm.Product = _uw.Product.GetT(x => x.Id == id);
                if (vm.Product == null)
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
        public IActionResult CreateUpdate(ProductVM vm, IFormFile? file)// this file name is req same as image input control name prop, because html control is known as its name prop
        {
            if (ModelState.IsValid)
            {
                string fileName = string.Empty; // string fileName = ""; <- we can also write this type
                if (file != null)// agar hamari file null nahi hai to vo simply upload honi chaiye
                {
                    string uploadDir = Path.Combine(_hostEnvironment.WebRootPath, "ProductImage");// uploadDir me hamare pass puri deirectory ka path aa gaya, Path.Combine se hamne ProductImage and WebRootPath ko combine kar dia, and _hostEnvironment se ham WebRootPath tak pahuch pae
                    fileName = Guid.NewGuid().ToString() + "-" + file.FileName;// fileName hamare pass kabhi bhi duplicate nahi hona chaiye isliye iske sath ham Guid jod dete hain
                    string filePath = Path.Combine(uploadDir, fileName);// this is full file path, with file name and directory, isi ke throw hamari file save hogi

                    if (vm.Product.ImageUrl != null)//this is a update code
                    {
                        var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, vm.Product.ImageUrl.TrimStart('\\'));//old image with new image in constructor
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStream = new FileStream(filePath, FileMode.Create))// isse hamari file create hogi, by coping file in FileStream
                    {
                        file.CopyTo(fileStream);
                    };
                    // vm.Product.ImageUrl = fileName;// this is one correct only
                    // vm.Product.ImageUrl = filePath;// this is also wrong because with filename directory path also goes them here
                    vm.Product.ImageUrl = @"\ProductImage\" + fileName; // here we difined filepath explicitly, but this is wrong \ProductImage\ text is goes to with imageurl here
                }
                if (vm.Product.Id == 0)
                {
                    _uw.Product.Add(vm.Product);
                    TempData["success"] = "Product Created Done!";
                }
                else
                {
                    _uw.Product.Update(vm.Product);
                    TempData["success"] = "Product Updated Done!";
                }
                _uw.Save();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        //[HttpGet]
        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    var category = _uw.Category.GetT(x => x.Id == id);
        //    if (category == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(category);
        //}
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public IActionResult DeleteDate(int? id)
        //{
        //    var category = _uw.Category.GetT(x => x.Id == id);
        //    if (category == null)
        //    { 
        //        return NotFound();
        //    }
        //    _uw.Category.Delete(category);
        //    _uw.Save();
        //    TempData["success"] = "Category Deleted Done!";
        //    return RedirectToAction("Index");
        //}

        /*here we use API for deleting data of Product*/
        //#region DeleteAPICALL
        //[HttpDelete]
        //public IActionResult Delete(int? id)
        //{
        //    var product = _uw.Product.GetT(x => x.Id == id);
        //    if (product == null)
        //    {
        //        return Json(new { success = false, message = "Error in Fetching Data" });
        //    }
        //    else
        //    {
        //        var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, product.ImageUrl.TrimStart('\\'));//old image with new image in constructor
        //        if (System.IO.File.Exists(oldImagePath))
        //        {
        //            System.IO.File.Delete(oldImagePath);
        //        }
        //        _uw.Product.Delete(product);
        //        _uw.Save();
        //        return Json(new { success = false, message = "Product Deleted" });
        //    }            
        //}
        //#endregion       

        #region DeleteAPICALL
        [HttpDelete]//yahan ham ek API bana rahe hain
        public IActionResult Delete(int? id)
        {
            var product = _uw.Product.GetT(x => x.Id == id);
            if (product == null)
            {
                return Json(new { success = false, message = "Error in Fething Data" });
            }
            else
            {
                var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, product.ImageUrl.TrimStart('\\'));

                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
                _uw.Product.Delete(product);
                _uw.Save();
                return Json(new { success = true, message = "Product Deleted" });
            }
        }
        #endregion
    }
}
