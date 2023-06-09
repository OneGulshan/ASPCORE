using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GettingStarted2.Controllers
{
    [Route("Error/{statusCode}")]
    public class ErrorController : Controller
    {
        public IActionResult Index(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMsg = "Page Not Found";
                    break;
                default:
                    break;
            }
            return View("NotFound");
        }
    }
}
//Project ke kuch Environment variables jinhe hamein set karna hota hai