using ASPCORE.Data;
using ASPCORE.GenericRepo;
using ASPCORE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;


namespace ASPCORE.Controllers
{
    public class GenericRepositoryController : Controller
    {
        private readonly ILogger<GenericRepositoryController> _logger;
        private readonly IRepo<RoomType> _repo;
        private readonly ApplicationDbContext _context;

        public GenericRepositoryController(ILogger<GenericRepositoryController> logger, IRepo<RoomType> repo, ApplicationDbContext context)
        {
            _logger = logger;
            _repo = repo;
            _context = context;
        }

        public IActionResult Index()
        {
            var listofRoomType = _repo.GetAll();
            return View(listofRoomType);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(RoomType roomt)
        {
            _context.Add(roomt);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Details(int Id)
        {
            var Data = _context.RoomTypes.Where(x => x.Id == Id).FirstOrDefault();
            return View(Data);
        }

        public IActionResult Delete(int Id)
        {
            var Data = _context.RoomTypes.Where(x => x.Id == Id).FirstOrDefault();
            return View(Data);
        }
        [HttpPost]
        public IActionResult Delete(RoomType roomt)
        {
            _context.RoomTypes.Remove(roomt);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int Id)
        {
            var Data = _context.RoomTypes.Where(x => x.Id == Id).FirstOrDefault();
            return View(Data);
        }
        [HttpPost]
        public IActionResult Edit(RoomType roomt)
        {
            _context.RoomTypes.Update(roomt);
            _context.SaveChanges();
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