using ASPCORE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCORE.Infrastructure
{
    public interface IEmployee
    {
        List<Employee> GetAll();
        Employee GetByID(int Id);
        Employee AddEmployee(Employee employee);
        Employee UpdateEmployee(Employee employee);
        Employee DeleteEmployee(int Id);
    }
}
