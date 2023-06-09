using ASPCORE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCORE.Controllers
{
    public class FormTagHelperController : Controller
    {
        public IActionResult Index()
        {
            var course = new List<Course>()
            {
                new Course{Id=1,Title="IT"},
                new Course{Id=2,Title="CS"}
            };
            ViewBag.CourseList = new SelectList(course, "Id", "Title", 2);
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee emp)
        {
            return View(emp);
        }
    }
}