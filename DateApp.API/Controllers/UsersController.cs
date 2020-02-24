using System.Threading.Tasks;
using DateApp.API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DateApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IDateRepository _dateRepository;
        
        public UsersController(IDateRepository dateRepository)
        {
            this._dateRepository = dateRepository;
        }

        // GET api/users
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _dateRepository.GetUsers();

            return Ok(users);
        }

        // POST api/users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _dateRepository.GetUser(id);

            return Ok(user);
        }
    }
}