
using DataAccessLayer.Infrastructure.IRepository;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Infrastructure.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private AppDbContext _db;//here context declared for initializing update method
        public CategoryRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Category category)
        {
            //_db.Categories.Update(category); //ham yhan directly apni category ko update nahi karte, phele 1 category nikalte hai fir uske members me data init kar update karenge
            var categoryDB = _db.Categories.FirstOrDefault(x => x.Id == category.Id);
            if(categoryDB != null)
            {
                categoryDB.Name = category.Name;
                categoryDB.DisplayOrder = category.DisplayOrder;                
            }
        }
    }
}
