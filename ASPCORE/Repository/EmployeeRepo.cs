using ASPCORE.Data;
using ASPCORE.Infrastructure;
using ASPCORE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCORE.Repository
{
    public class EmployeeRepo : IEmployee
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public Employee AddEmployee(Employee employee)
        {
            var result = _context.Employees.Add(employee);
            _context.SaveChanges();
            return result.Entity;
        }

        public Employee DeleteEmployee(int Id)
        {
            var result = _context.Employees.Find(Id);
            _context.Employees.Remove(result);
            _context.SaveChanges();
            return result;
        }

        public List<Employee> GetAll()
        {
            return _context.Employees.ToList();
        }

        public Employee GetByID(int Id)
        {
            return _context.Employees.FirstOrDefault(x => x.Id == Id);
        }

        public Employee UpdateEmployee(Employee employee)
        {
            var result = _context.Employees.Where(x => x.Id == employee.Id).FirstOrDefault();
            result.Name = employee.Name;
            result.Email = employee.Email;            
            _context.Employees.Update(result);
            _context.SaveChanges();
            return result;
        }
    }
}
