using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DbInitializer
{
    public interface IDbInitializer
    {
        void Initialize();//is method me ham kuch chijen fix karenge like migration is compleated or pending migrations etc
    }
}
