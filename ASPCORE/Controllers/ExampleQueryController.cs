using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GettingStarted.Controllers
{
    public class ExampleQueryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetString(string first)
        {
            return View();
        }
    }
}