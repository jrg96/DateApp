using System.Threading.Tasks;
using DateApp.API.Models;

namespace DateApp.API.Data
{
    public interface IAuthRepository
    {
         Task<User> InsertUser(User user, string password);
         
         Task<User> Login(string username, string password);

         Task<bool> UserExists(string username);

    }
}