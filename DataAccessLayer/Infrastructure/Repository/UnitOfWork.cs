
using DataAccessLayer.Infrastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Infrastructure.Repository
{ // this is a small jall using UnitOfWork, create generic class then model class then UnitOfWork
    public class UnitOfWork : IUnitOfWork // UnitOfWork <- this is known as a Wrapper class  // Hamne yahan UnitOfWork ka use karke ICategoryRepository ke variable ko Context de dia hai, simply for using CategoryRepository
    {
        private AppDbContext _db;
        public ICategoryRepository Category { get; private set; } // here UnitOfWork can call Privately set Category because its maked also public
        public IProductRepository Product { get; private set; }
        public ICartRepository Cart { get; private set; }
        public IApplicationUser ApplicationUser { get; private set; }
        public IOrderHeaderRepository OrderHeader { get; private set; }
        public IOrderDetailRepository OrderDetail { get; private set; }

        public UnitOfWork(AppDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(db); // Privately Set ka matlab hai constructor ke ander set karna, jab ham UnitOfWork ko call karene tab hamari Category initialize  honi chaiye
            Product = new ProductRepository(db);
            Cart = new CartRepository(db);
            ApplicationUser = new AppicationUserRepository(db);
            OrderHeader = new OrderHeaderRepository(db);
            OrderDetail = new OrderDetailRepository(db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
