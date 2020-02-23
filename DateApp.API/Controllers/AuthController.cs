using System.Threading.Tasks;
using DateApp.API.Data;
using DateApp.API.DTO;
using DateApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DateApp.API.Controllers
{
    // base: api/auth/
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        public AuthController(IAuthRepository authRepository)
        {
            this._authRepository = authRepository;
        }

        // POST api/auth/
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDTO userForRegisterDTO)
        {
            /*
             * ---------------------------------------------------------------------------
             * ZONA DE VALIDACION
             * ---------------------------------------------------------------------------
             */
            
            // Corroborar que el username se encuentre en lower case
            userForRegisterDTO.Username = userForRegisterDTO.Username.ToLower();

            // Corroboramos si el username ya existe
            if (await _authRepository.UserExists(userForRegisterDTO.Username))
            {
                return BadRequest("Username already exists");
            }

            /*
             * --------------------------------------------------------------------------
             * ZONA DE PROCESAMIENTO DE LA PETICION
             * --------------------------------------------------------------------------
             */
            
            // Creando usuario
            var user = new User
            {
                Username = userForRegisterDTO.Username   
            };

            var userDB = await _authRepository.InsertUser(user, userForRegisterDTO.Password);
            
            return StatusCode(201);
        }


    }
}