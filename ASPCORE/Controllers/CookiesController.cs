using ASPCORE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCORE.Controllers
{
    public class CookiesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee emp)
        {
            CookieOptions cookies = new CookieOptions();
            cookies.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Append("Email", emp.Email, cookies);//Here stored our Email in cookies
            ViewBag.Saved = "Cookie Saved";
            return View();
        }

        public IActionResult ReadCookies()
        {
            ViewBag.Email = Request.Cookies["Email"].ToString();
            return View("Create"); //("Create") <- is tarah View par value return karne ke liye
        }
    }
}
