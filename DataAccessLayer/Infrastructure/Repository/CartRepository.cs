
using DataAccessLayer.Infrastructure.IRepository;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Infrastructure.Repository
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        private readonly AppDbContext _context;
        public CartRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }        

        public int IncreamentCartItem(Cart cart, int count)
        {
            cart.Count += count;//cart hamare pass database se aa rha hai vo update ho jae jo count hamare pass age se aaya hai usse
            return cart.Count;//int type ko hamne isliye return kia hai ki aggar iski value vhan par updated hai to vo dhikha dega ki cart item me total values kitni ho gai hai, taaki ham view me ye cheejen bhej saken
        }

        public int DecreamentCartItem(Cart cart, int count)
        {
            cart.Count -= count;
            return cart.Count;
        }
    }
}
