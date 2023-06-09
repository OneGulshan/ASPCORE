using JobPortal.Data;
using JobPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JobPortal.Controllers
{
    public class JobRecruiterController : Controller
    {
        private readonly AppDbContext _context;

        public JobRecruiterController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var result = _context.Companies.ToList();
            return View(result);
        }

        [HttpGet]
        public IActionResult Create(int id)
        {
            ViewBag.JobProfile = new SelectList(_context.JobProfile.ToList(), "JPId", "Name");

            if (id > 0)
            {
                var Company = _context.Companies.Where(_ => _.Id == id).FirstOrDefault();
                ViewBag.Bt = "Update";
                return View(Company);
            }
            else
            {
                ViewBag.BT = "Create";
                return View();
            }
        }

        public JsonResult GetCategory(int JPId)
        {
            var JobProfileList = _context.JobProfile.Where(_ => _.JPId == JPId).ToList();
            ViewBag.JobProfile = new SelectList(JobProfileList, "JPId", "Name");
            return Json(ViewBag.JobProfile);
        }

        [HttpPost]
        public IActionResult Create(Company company, int id)
        {
            if (id > 0)
            {
                var Company = _context.Companies.Find(id);
                Company.Id = company.Id;
                Company.Name = company.Name;
                Company.Email = company.Email;
                Company.Password = company.Password;
                Company.Comment = company.Comment;
                Company.JobProfileId = company.JobProfileId;

                _context.Companies.Update(Company);
                _context.SaveChanges();
            }
            else
            {
                _context.Companies.Add(company);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
