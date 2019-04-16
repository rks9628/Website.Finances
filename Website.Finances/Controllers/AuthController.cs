using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Threading.Tasks;
using Website.Finances.BL.Cryptography;
using Website.Finances.Domain.Interfaces;
using Website.Finances.Domain.ValueTypes;

namespace Website.Finances.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetUser()
        {
            return Ok(new User { Id = 5, Username = "test" }); 
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCredentials credentials)
        {
            var user = new User
            {
                Username = credentials.Username,
                PasswordHash = await new Argon2Hasher().HashAsync(Encoding.UTF8.GetBytes(credentials.Password), new byte[8]),
                PasswordSalt = new byte[8]
            };

            await _userRepository.InsertAsync(user);

            return Ok(user);
        }
    }
}
