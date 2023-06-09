using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Infrastructure.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; } // here we create only get prop for initializing by context all these models properties
        IProductRepository Product { get; }
        ICartRepository Cart { get; }
        IApplicationUser ApplicationUser { get; }
        IOrderHeaderRepository OrderHeader { get; }
        IOrderDetailRepository OrderDetail { get; }
        void Save(); // Here Save single method work for all types of Models, same Save method
    }
}
