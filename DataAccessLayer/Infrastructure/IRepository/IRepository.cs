using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Infrastructure.IRepository
{
    public interface IRepository<T> where T : class
    {// These are Common Functions for all classes/models
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? predicate=null, string? includeProperties = null);// For Accessing this -> IEnumerable<Category> // includeProperty <- for getting extra multiple table's prop/table seprate by , for using this multiple includeProperty
        T GetT(Expression<Func<T, bool>> predicate, string? includeProperties = null);// Single func ke throw single class/model(category model/class with single records) return karane ke liye
        void Add(T entity);// Add and Delete Functions are same for all but update is differ
        void Delete(T entity);// single List delete karne ke liye in single class
        void DeleteRange(IEnumerable<T> entity);// multiple Lists delete karne ke liye in single class
    }
}
