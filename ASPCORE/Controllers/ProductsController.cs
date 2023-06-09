using ASPCORE.Data;
using ASPCORE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPCORE.Helper;

namespace ASPCORE.Controllers
{
    public class ProductsController : Controller
    {
        public readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int pageNumber = 1, string searchProductName=null)
        {
            var searchResult = _context.Products.AsNoTracking();
            if (!String.IsNullOrEmpty(searchProductName))
            {
                searchResult = _context.Products.Where(x => x.Title.Contains(searchProductName)).AsNoTracking();
                if (searchResult.Count() == 0)
                {
                    ViewBag.products = "Search Product Not Found";
                    return View();
                }                
            }


            int pageSize = 3;//pageSize = Total No of records
            return View(await PaginatedList<Product>.CreateAsync(searchResult, pageNumber, pageSize));
        }
    }
}