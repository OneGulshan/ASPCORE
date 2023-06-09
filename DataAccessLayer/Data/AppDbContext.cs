using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataAccessLayer
{
    public class AppDbContext : IdentityDbContext /*DbContext*/ // hamari Data Context Class bydefault DbContext se inherit hoti hai agar hamein Identity ka use karna hai Project ke ander to hamari DbContext class IdentityDbContext se inherit honi chaiye
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }//Model ko db me create karne liye model ki entry hamein context ke DbSet me daalni hoti hai
        public DbSet<ApplicationUser> ApplicationUsers { get; set; } // for using our IdentityUser Identity in DbContext we added our ApplicationUser class here
        public DbSet<Cart> Carts { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}
