using ASPCORE.Data;
using ASPCORE.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCORE.Controllers
{
    public class CourseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CourseController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Curd Operations
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index(string sortOrder)
        {
            ViewData["TitleSort"] = string.IsNullOrEmpty(sortOrder) ? "title_desc" : "";

            var courses = _context.Courses.ToList();
            switch (sortOrder)
            {
                case "title_desc":
                    courses = courses.OrderByDescending(s => s.Title).ToList();
                    break;

                default:
                    courses = courses.OrderBy(s => s.Title).ToList();
                    break;
            }

            //var course = _context.Courses.ToList();
            return View(courses);
        }

        public IActionResult Details(int id)
        {
            var course = _context.Courses.FirstOrDefault(x => x.Id == id);
            ViewBag.courseView = course;
            //ViewData["course"] = course;//ViewData/ViewBag loosly type se kisi bhi string type ki value ko ham view par directly sent kar sakte hai
            ViewData["Title"] = "Detailed Section";
            return View(course);
        }

        public IActionResult Delete(int Id)
        {
            var result = _context.Courses.Where(x => x.Id == Id).FirstOrDefault();
            return View(result);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int Id)
        {
            var result = _context.Courses.Where(x => x.Id == Id).FirstOrDefault();
            _context.Courses.Remove(result);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create(int Id1 = 0)
        {
            if (Id1 > 0)
            {
                var result = _context.Courses.Where(x => x.Id == Id1).FirstOrDefault();
                return View(result);
            }
            return View();
        }
        [HttpPost]
        public IActionResult Create(Course course)
        {
            if (course.Id > 0)
            {
                var result = _context.Courses.Where(x => x.Id == course.Id).FirstOrDefault();
                result.Code = course.Code;
                result.Title = course.Title;
                _context.Courses.Update(result);                
            }
            else if (course.Id == 0)
            {
                _context.Courses.Add(course);
            }
            _context.SaveChanges();
            return RedirectToAction("Index");                        
        }
    }
}
