using ASPCORE.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCORE.GenericRepo
{
    public class Repository<T> : IRepo<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _entities;//yahan ham DbSet kam me le rahe hai

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _entities = _context.Set<T>();

        }

        public IEnumerable<T> GetAll()
        {
            return _entities.AsEnumerable();//Every type class is accessible here by AsEnumerable()
        }

        public T GetById(int Id)
        {
            return _entities.Find(Id);
        }        
    }
}
//Here Generic repo is a type of template. with the help of this template we can create a performa. this performa used for executing common functions like classes(Room, RoomFacility, RoomType)
//Means 3 class common functions used on single space