using Microsoft.EntityFrameworkCore;

using DataAccessLayer.Infrastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        // Ab yahan samajhne wali baat hai ki Generic Repository directly DataAccessLayer se call karti hai
        private readonly AppDbContext _db;
        private DbSet<T> _dbSet;// ab ham yahan DbSet ko directly access karenge without calling Context Class
        public Repository(AppDbContext db)
        {
            _db = db;
            //_db.Products.Include(x => x.Category);// ek EF core ki prop hoti hai agar hamein ek table se dusri table tak jana hai to hamein navigation prop ko include karna hota hai like Include
            _dbSet = _db.Set<T>();// this means -> _dbSet = _db.categories
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);// Remove fuction in EF for removing single list
        }

        public void DeleteRange(IEnumerable<T> entity)
        {
            _dbSet.RemoveRange(entity);// RemoveRange fuction in EF for removing multiple lists
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? predicate=null, string? includeProperties = null)
        {
            IQueryable<T> query = _dbSet;// yahan hamne _dbSet se model mach kara liye T query se fir unhe include kar return kara liya simple yahai ho rha bas yahan
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (includeProperties != null)
            {
                foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))// here multiple includeProperties is seprated by Split func by comma put in char using StringSplitOptions enum var RemoveEmptyEntries remove all empty properties/spaces.
                {
                    query = query.Include(item);// yhan item se query ke throw one by one includeProperty nikal kar query me rakh kar return kar denge.
                }
            }
            return query.ToList();// here Category Lists return with products, or bhi kuch chiz hogi to vo yhan handel ho jaegi return ho jaegi like many to many relationship
        }

        public T GetT(Expression<Func<T, bool>> predicate, string? includeProperties = null)
        {
            IQueryable<T> query = _dbSet;
            query = query.Where(predicate);// here filter by where
            if (includeProperties != null)
            {
                foreach (var item in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))// here multiple includeProperty is seprated by Split func by comma put in char using StringSplitOptions enum var RemoveEmptyEntries remove all empty properties/spaces.
                {
                    query = query.Include(item);// yhan item se query ke throw one by one includeProperty nikal kar query me rakh kar return kar denge.
                }
            }
            // return _dbSet.Where(predicate).FirstOrDefault();// here predicate(class) first record return with bool value using where clause
            return query.FirstOrDefault();// here 1 record is retrive with product + category
        }
    }
}
