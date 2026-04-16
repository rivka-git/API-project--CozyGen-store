using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;
using System.Text.Json;
using Services;
using Dto;
using Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserServices userServices, ILogger<UsersController> logger)
        {
            _logger = logger;
            _userServices = userServices;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            return await _userServices.GetUsers();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DtoUserNameEmailRoleId>> Get(int id)
        {
            DtoUserNameEmailRoleId? user = await _userServices.GetUserById(id);
            if (user != null)
            {
                return Ok(user);
            }
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<DtoUserNameEmailRoleId>> Post([FromBody] DtoUserAll user)
        {
            DtoUserNameEmailRoleId res = await _userServices.AddNewUser(user);
            if (res != null)
            {
                return CreatedAtAction(nameof(Get), new { id = res.UserId }, res);
            }
            else
                return BadRequest();
        }

        [HttpPost("Login")]
        public async Task<ActionResult<DtoUserNameEmailRoleId>> Login([FromBody] DtoUserEmailPassword user)
        {
            DtoUserNameEmailRoleId? res = await _userServices.Login(user);
            if (res != null)
            {
                _logger.LogInformation($"login attempted with user name,{user.Email} and password {user.PasswordHash}");
                return Ok(res);
            }
            return NotFound();
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<DtoUserNameEmailRoleId>> Put(int id, [FromBody] DtoUserAll value)
        {
            DtoUserNameEmailRoleId res = await _userServices.Update(id, value);
            if (res != null)
            {
                return CreatedAtAction(nameof(Get), new { id = res.UserId }, res);
            }
            else
                return BadRequest();
        }
    }
}

