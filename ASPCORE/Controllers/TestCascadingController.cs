using ASPCORE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCORE.Controllers
{
    public class TestCascadingController : Controller
    {
        private readonly ILogger<TestCascadingController> _logger;

        public TestCascadingController(ILogger<TestCascadingController> logger)
        {
            _logger = logger;
        }
        
        public IActionResult Index()
        {
            Repositorys repo = new Repositorys();
            ViewBag.countries = new SelectList(repo.Countries, "Id", "Name");
            return View();
        }
        
        [HttpPost]
        public JsonResult GetState(string CountryId)
        {
            Repositorys repo = new Repositorys();
            var StateList = repo.States.Where(z => z.CountryId == Convert.ToInt32(CountryId));
            return Json(new SelectList(StateList, "Id", "Name"));
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
