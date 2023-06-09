using ASPCORE.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ASPCORE.Data;
using ASPCORE.ViewModels;

namespace ASPCORE.Controllers
{
    public class RolesController : Controller
    {

        private readonly ILogger<RolesController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public RolesController(UserManager<ApplicationUser> userManager,
            ILogger<RolesController> logger,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string Id)
        {
            var users = _context.Users.Where(x => x.Id == Id).SingleOrDefault();
            UserRoles UserRoles = new UserRoles();
            var userInRole = _context.UserRoles.Where(x => x.UserId == Id).Select(x => x.RoleId).ToList();
            var userInClaims = _context.UserClaims.Where(x => x.UserId == Id).Select(x => x.ClaimValue).ToList();


            UserRoles.roles = await _roleManager.Roles.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id,
                Selected = userInRole.Contains(x.Id)

            }).ToListAsync();

            UserRoles.AppUser = users;
            UserRoles.claims = ClaimStore.All.Select(x => new SelectListItem()
            {
                Text = x.Type,
                Value = x.Value,
                Selected = userInClaims.Contains(x.Value)
            }).ToList();

            return View(UserRoles);
        }

        [HttpPost]
        public IActionResult Edit(UserRoles model)
        {
            //User Role
            var selectedRoleId = model.roles.Where(x => x.Selected).Select(x => x.Value);
            var AlreadyExist = _context.UserRoles.Where(x => x.UserId == model.AppUser.Id).Select(x => x.RoleId).ToList();
            var toAdd = selectedRoleId.Except(AlreadyExist);//db me jo phele se exist the unhe to chod dia or edit walon ko add kar liya, aise hi to Remove nikal liye
            var toRemove = AlreadyExist.Except(selectedRoleId);

            foreach (var item in toRemove)
            {
                _context.UserRoles.Remove(new IdentityUserRole<string>
                {
                    RoleId = item,
                    UserId = model.AppUser.Id
                });
            }
            foreach (var item in toAdd)
            {
                _context.UserRoles.Add(new IdentityUserRole<string>
                {
                    RoleId = item,
                    UserId = model.AppUser.Id
                });
            }

            //User Claims

            var selectedClaimValues = model.claims.Where(x => x.Selected).Select(x => x.Value);
            var AlreadyExistClaims = _context.UserClaims.Where(x => x.UserId == model.AppUser.Id).Select(x => x.Id.ToString()).ToList();
            var toAddClaims = selectedClaimValues.Except(AlreadyExistClaims);
            var toRemoveClaims = AlreadyExistClaims.Except(selectedClaimValues);

            foreach (var item in toRemoveClaims)
            {
                _context.UserClaims.Remove(new IdentityUserClaim<string>
                {
                    Id = Convert.ToInt32(item),
                    UserId = model.AppUser.Id
                });
            }

            foreach (var item in toAddClaims)
            {
                _context.UserClaims.Add(new IdentityUserClaim<string>
                {
                    UserId = model.AppUser.Id,
                    ClaimValue = item,
                    ClaimType = item
                });
            }


            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}