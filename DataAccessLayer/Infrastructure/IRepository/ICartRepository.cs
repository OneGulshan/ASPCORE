using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Infrastructure.IRepository
{        
    public interface ICartRepository : IRepository<Cart> // ham ek baar chiz ko teyar kar lete hai fir uski copy pase se hi ham log kaam chalate hai, ye ICartRepository(repository) generic ko call karegi IRepository of T ko or T ke ander ham apni class file dalenge yani ki Cart
    {
        int IncreamentCartItem(Cart cart, int count); // jab ham dubara item add karenge to hamare pass count ki value aa jaegi
        int DecreamentCartItem(Cart cart, int count);
    }
}
