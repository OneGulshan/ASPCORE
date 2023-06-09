using ASPCORE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCORE.Controllers
{
    public class CachingController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMemoryCache MemoryCache;
        public CachingController(ILogger<HomeController> logger, IMemoryCache memoryCache)
        {
            _logger = logger;
            MemoryCache = memoryCache;
        }

        public IActionResult Index()
        {
            DateTime CurrentTime;
            bool value = MemoryCache.TryGetValue("CachedTime", out CurrentTime);
            if (!value)
            {
                CurrentTime = DateTime.Now;
                var cacheEnteryOption = new MemoryCacheEntryOptions().
                    SetSlidingExpiration(TimeSpan.FromSeconds(30));
                MemoryCache.Set("CachedTime", CurrentTime, cacheEnteryOption);
            }
            return View(CurrentTime);
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
