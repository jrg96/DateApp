using System.Threading.Tasks;
using DateApp.API.Models;
using System.Linq;

namespace DateApp.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        
        public AuthRepository(DataContext context)
        {
            this._context = context;

        }

        public async Task<User> InsertUser(User user, string password)
        {
            byte[] passwordHash;
            byte[] passwordSalt;

            this.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public Task<User> Login(string username, string password)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UserExists(string username)
        {
            throw new System.NotImplementedException();
        }


        /*
         * ---------------------------------------------------------------------------- 
         * METODOS PROPIOS DEL REPOSITORIO (NO EXPUESTOS)
         * ---------------------------------------------------------------------------
         */
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {

            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

        }
    }
}