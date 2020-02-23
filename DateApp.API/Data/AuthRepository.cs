using System.Threading.Tasks;
using DateApp.API.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
            
            // Guardando cambios en la DB de manera asincrona
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
      
            return user;
        }

        public async Task<User> Login(string username, string password)
        {
            // Paso 0: obtener datos de la DB
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

            // Paso 1: Obtener hash de la contraseña y corroborar si son las mismas
            if (user != null)
            {
                if(!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                {
                    return null;
                }

                return user;
            }

            return null;
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(x => x.Username == username))
            {
                return true;
            }

            return false;
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

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}