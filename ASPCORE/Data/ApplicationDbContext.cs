using ASPCORE.Models;
using ASPCORE.ViewModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPCORE.ViewModels;

namespace ASPCORE.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)//apni ApplicationDbContext class ko DbContextOptions jo ki DbContext class ki ek prop hai iske throw apni ApplicationDbContext ko DbContext me Convert karke use kar liya with the help of base keyword
        {

        }
        public DbSet<Student> Students { get; set; }//DbSet Class is for Connecting with Database
        public DbSet<StudentCourse> Emrollments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<RoleStore> RoleStore { get; set; }
        public DbSet<AppUserViewModel> AppUserViewModel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 6, Name = "Akash", ProfileImage = "abc.png" },
                new Student { Id = 7, Name = "Vishal", ProfileImage = "xyz.jpg" }
                );
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Product> Products { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Student>().ToTable("Student_Table");
        //    modelBuilder.Entity<StudentCourse>().ToTable("StudentCourse_Table");
        //    modelBuilder.Entity<Course>().ToTable("Course_Table");
        //}
    }
}
