
using DataAccessLayer.Infrastructure.IRepository;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Infrastructure.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private AppDbContext _db;
        public ProductRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product product)
        {
            var productDB = _db.Products.FirstOrDefault(x => x.Id == product.Id);
            if (productDB != null)
            {
                productDB.Name = product.Name;
                productDB.Description = product.Description;
                productDB.Price = product.Price;
                productDB.CategoryId = product.CategoryId;//Cateory is depended on CategoryId so work by CategoryId
                if (product.ImageUrl != null)//ImageUrl external koi bhi daal sakta tha but ab nahi daal paega, kunki jab image se uska url match karga tabhi ab database me ImageUrl ki entery hogi
                {
                    productDB.ImageUrl = product.ImageUrl;
                }
            }
        }
    }
}
