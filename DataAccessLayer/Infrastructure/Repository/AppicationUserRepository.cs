
using DataAccessLayer.Infrastructure.IRepository;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Infrastructure.Repository
{
    public class AppicationUserRepository : Repository<ApplicationUser>, IApplicationUser
    {
        private readonly AppDbContext _context;
        public AppicationUserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }        
    }
}
