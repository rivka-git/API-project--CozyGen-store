using Dto;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PasswordController : ControllerBase
    {
        private readonly IPasswordService _passwordService;

        public PasswordController(IPasswordService passwordService, ILogger<PasswordController> logger)
        {
            _passwordService = passwordService;
        }

        [HttpPost]

        public int Post([FromBody] string password)
        {
            return _passwordService.GetStrengthByPassword(password);
        }
    }
}
