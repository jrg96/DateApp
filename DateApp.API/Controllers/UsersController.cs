using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DateApp.API.Data;
using DateApp.API.DTO;
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
        private readonly IMapper _mapper;

        public UsersController(IDateRepository dateRepository, IMapper mapper)
        {
            this._mapper = mapper;
            this._dateRepository = dateRepository;
        }

        // GET api/users
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _dateRepository.GetUsers();
            var usersToReturn = _mapper.Map<IEnumerable<UserForListDTO>>(users);

            return Ok(usersToReturn);
        }

        // POST api/users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _dateRepository.GetUser(id);
            var userToReturn = _mapper.Map<UserForDetailDTO>(user);

            return Ok(userToReturn);
        }
    }
}