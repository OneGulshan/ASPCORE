using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CommonHelper;
using DataAccessLayer;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DbInitializer
{
    public class DbInitialize : IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;//so in sabke liye hamein in 3n chijon ki jarurat hai
        private readonly RoleManager<IdentityRole> _roleManager;//we can also initialize all things in a single selection in constructor
        private readonly AppDbContext _db;

        public DbInitialize(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            AppDbContext db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
        }

        public void Initialize()//isko hamin apni startup file me call karana hai as a dependency //pending migrations ko ham isse migrate kar sakte hain, after mig we create some roles of reg page, after we create admin these all things done by when our project is run, usse phele ham apne database ko delete kar denge kunki hamein karna padega admin or users create karne ke liye or create a new database from name of no. 2, iske liye ham apne database ke aage 2 laga denge takki hamara database 2sra ban jae
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)//GetPendingMigrations <- ye entity framework ke ander hota hai iska use karke Database db se migrations ke bare me chk kar liya
                {
                    _db.Database.Migrate();//isse migration apply ho jaega in database
                }
            }
            catch (Exception)
            {

                throw;
            }

            if (!_roleManager.RoleExistsAsync(WebSiteRole.Role_Admin).GetAwaiter().GetResult())//role manager class check karta hai ki role exist karta hai ke nahi karta hai, WebSiteRole me hamare roles hai or ye static class hai to hamein directly apni properties ke naam likhna hai, GetAwaiter().GetResult() <- isse ham check kar rahe hai ki Admin ka jo role hai vo default me add hai ke nahi hai, agar nahi hoga to 3no role create ho jaenge hamare jo declared hai in if condition
            {
                _roleManager.CreateAsync(new IdentityRole(WebSiteRole.Role_Admin)).GetAwaiter().GetResult();//role exist nahi hai to ye role create ho jaenge nahi to nahi hoge
                _roleManager.CreateAsync(new IdentityRole(WebSiteRole.Role_User)).GetAwaiter().GetResult();//role create hone ke baad ham ek naya user banaenge, or usko role assign karenge as a admin
                _roleManager.CreateAsync(new IdentityRole(WebSiteRole.Role_Employee)).GetAwaiter().GetResult();

                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    Name = "Admin",
                    PhoneNumber = "1234567898",
                    Address = "xyz",
                    City = "xyz",
                    State = "xyz",
                    PinCode = "333011"
                }, "Admin@123").GetAwaiter().GetResult();//last me hamein password define karna padta hai jab ham create user karte hai tab hamein user me application user define karna padta hai/identity user define karna padta hai uske baad hamein password define karna padta hai.
                ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(x => x.Email == "admin@gmail.com");//ab is application user ko hamein role assign karna padega, user nikalne ke baad jo hamne create kia hai
                _userManager.AddToRoleAsync(user, WebSiteRole.Role_Admin).GetAwaiter().GetResult();//is user ko role assign karna ke liye, yahan hamne role asign kia hai isliye hamein yahan GetAwaiter nad GetResult method ke use karna pada hai
            }
            return;
        }
    }
}
