using ASPCORE.Infrastructure;
using ASPCORE.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCORE.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployee _repo;

        public EmployeeController(IEmployee repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            var item = _repo.GetAll();
            return View(item);
        }

        public IActionResult Details(int Id)
        {
            var item = _repo.GetByID(Id);
            return View(item);
        }

        public IActionResult Delete(int Id)
        {
            _repo.DeleteEmployee(Id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create(int id = 0)
        {
            if (id > 0)
            {
                var result = _repo.GetByID(id);
                return View(result);
            }
            return View();
        }
        [HttpPost]
        public IActionResult Create(int id,Employee employee)
        {
            if (id > 0)
            {
                _repo.UpdateEmployee(employee);
                return RedirectToAction("Index");
            }
            else if (employee!=null)
            {
                _repo.AddEmployee(employee);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
