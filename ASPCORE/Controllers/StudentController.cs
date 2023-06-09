using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASPCORE.Data;
using ASPCORE.Models;
using ASPCORE.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace ASPCORE.Controllers
{
    [Authorize(Roles = "Student,Administrator")]
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Student,Administrator")]
        // GET: Student
        public async Task<IActionResult> Index()
        {
            return View(await _context.Students.ToListAsync());
        }

        // GET: Student/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return View();
            }

            var student = await _context.Students.Include(a=>a.Enrollment).ThenInclude(b=>b.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return View("notfound", id);
            }

            return View(student);
        }

        // GET: Student/Create
        public IActionResult Create()
        {
            var courses = _context.Courses.Select(x => new SelectListItem()
            {
                Text = x.Title,
                Value = x.Id.ToString()
            }).ToList();

            CreateStudentViewModel vm = new CreateStudentViewModel();
            vm.Courses = courses;

            return View(vm);
        }

        // POST: Student/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]//Here we used ValidateAntiForgeryToken for validate from other users/Heackers
        public async Task<IActionResult> Create(/*[Bind("Id,Name,ProfileImage,Enrolled")]*/ CreateStudentViewModel vm)
        {
            var student = new Student()
            {
                Name = vm.Name,
                Enrolled = vm.Enrolled
            };
            var SelectedCourse = vm.Courses.Where(x => x.Selected).Select(y => y.Value).ToList();
            foreach (var item in SelectedCourse)
            {
                student.Enrollment.Add(new StudentCourse()
                {
                    CourseId = int.Parse(item)
                });
            }

            if (ModelState.IsValid)
            {
                await _context.Students.AddAsync(student);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Student/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.Include(x=>x.Enrollment).Where(y => y.Id == id).FirstOrDefaultAsync();
            if (student == null)
            {
                return NotFound();
            }
            var selectedIds = student.Enrollment.Select(x => x.CourseId).ToList();
            var items = _context.Courses.Select(x => new SelectListItem()
            {
                Text = x.Title,
                Value = x.Id.ToString(),
                Selected = selectedIds.Contains(x.Id)
            }).ToList();
            CreateStudentViewModel vm = new CreateStudentViewModel();
            vm.Name = student.Name;
            vm.Enrolled = student.Enrolled;
            vm.Courses = items;
            
            return View(vm);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CreateStudentViewModel vm)
        {
            var student = _context.Students.Find(vm.Id);
            student.Name = vm.Name;
            student.Enrolled = vm.Enrolled;
            var studentById = _context.Students.Include(x => x.Enrollment).FirstOrDefault(y => y.Id == vm.Id);
            var existingIds = studentById.Enrollment.Select(x => x.CourseId).ToList();
            var selectedIds = vm.Courses.Where(x => x.Selected).Select(y => y.Value).Select(int.Parse).ToList();
            var toAdd = selectedIds.Except(existingIds).ToList();
            var toRemove = existingIds.Except(selectedIds).ToList();
            student.Enrollment = student.Enrollment.Where(x => !toRemove.Contains(x.CourseId)).ToList();

            foreach (var item in toAdd)
            {
                student.Enrollment.Add(new StudentCourse()
                {
                    CourseId = item
                });
            }
            _context.Students.Update(student);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: Student/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}
