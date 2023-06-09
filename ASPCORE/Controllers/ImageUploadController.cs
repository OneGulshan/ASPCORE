using ASPCORE.Data;
using ASPCORE.Models;
using ASPCORE.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCORE.Controllers
{
    public class ImageUploadController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _WebHostEnvironment;

        public ImageUploadController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _WebHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var items = _context.Students.ToList();
            return View(items);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(StudentViewModel vm)
        {
            string stringFileName = UploadFile(vm);
            var student = new Student
            {
                Name = vm.Name,
                ProfileImage = stringFileName
            };
            _context.Students.Add(student);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public string UploadFile(StudentViewModel vm)
        {
            string fileName = null;
            if (vm.ProfileImage != null)
            {
                string uplodDir = Path.Combine(_WebHostEnvironment.WebRootPath, "Images");
                fileName = Guid.NewGuid().ToString() + "_" + vm.ProfileImage.FileName;
                string filePath = Path.Combine(uplodDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    vm.ProfileImage.CopyTo(fileStream);
                }

            }
            return fileName;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
