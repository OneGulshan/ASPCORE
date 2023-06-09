using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCORE.GenericRepo
{
    public interface IRepo<T> where T : class //here T is a class
    {
        IEnumerable<T> GetAll();
        T GetById(int Id);
    }
}
