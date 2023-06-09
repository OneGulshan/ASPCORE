using ASPCORE.ViewModels;
using ASPCORE.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCORE.Controllers
{
    [Route("[controller]/[action]")]//token Based Routing
    public class TestController : Controller
    {
        private readonly ILogger<TestController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;

        public TestController(ILogger<TestController> logger, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _roleManager = roleManager;
        }
        [Route("~/Test")]
        [Route("~/Test/Empty")]
        [Route("~/")]
        public IActionResult Empty()
        {
            return View("~/Views/Test/Index.cshtml");
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleStore role)
        {
            var roleExist = await _roleManager.RoleExistsAsync(role.RoleName);
            if (!roleExist)
            {
                await _roleManager.CreateAsync(new IdentityRole(role.RoleName));
            }
            return RedirectToAction("Index");
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