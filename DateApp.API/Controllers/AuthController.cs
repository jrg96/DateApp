using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DateApp.API.Data;
using DateApp.API.DTO;
using DateApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DateApp.API.Controllers
{
    // base: api/auth/
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _config;
        public AuthController(IAuthRepository authRepository, IConfiguration config)
        {
            this._authRepository = authRepository;
            this._config = config;
        }

        // POST api/auth/register
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


        // POST api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDTO userForLoginDTO)
        {
            /*
             * ---------------------------------------------------------------------------
             * ZONA DE VALIDACION
             * ---------------------------------------------------------------------------
             */

            // Comprobamos si existe un usuario en la DB con esos credenciales
            var user = await _authRepository.Login(userForLoginDTO.Username.ToLower(), userForLoginDTO.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            /*
             * --------------------------------------------------------------------------
             * ZONA DE PROCESAMIENTO DE LA PETICION
             * --------------------------------------------------------------------------
             */
            
            // Creando los claims del token JWT
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Retornando la respuesta
            return Ok(new {
                token = tokenHandler.WriteToken(token)
            });
        }
        
    }
}