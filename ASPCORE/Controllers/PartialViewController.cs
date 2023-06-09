using ASPCORE.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCORE.Controllers
{
    public class PartialViewController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PartialViewController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var courses = _context.Courses.ToList();
            return View(courses);
        }
    }
}
