using System.Collections.Generic;
using System.Threading.Tasks;
using DateApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DateApp.API.Data
{
    public class DateRepository : IDateRepository
    {
        private readonly DataContext _context;

        public DateRepository(DataContext context)
        {
            this._context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            this._context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            this._context.Remove(entity);
        }

        public async Task<User> GetUser(int id)
        {
            return await this._context.Users.Include(x => x.Photos).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await this._context.Users.Include(x => x.Photos).ToListAsync();
        }

        public async Task<bool> SaveAll()
        {
            return await this._context.SaveChangesAsync() > 0;
        }
    }
}