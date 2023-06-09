using ASPCORE.Data;
using ASPCORE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCORE.Controllers
{    
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("Home")]//Routing is mendatory in attribute routing
        [Route("Home/Index")]
        public IActionResult Index()
        {
            //throw new Exception("Error in Detail");
            ViewBag.courses = new SelectList(_context.Courses, "Id", "Title");
            return View();
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() //In Production Environment Global Exception Handling by Error Page
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
