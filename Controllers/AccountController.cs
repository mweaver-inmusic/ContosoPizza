using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ContosoPizza.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> PostAsync(Register register)
        {
            var user = new IdentityUser { Email = register.Email, UserName = register.Username };
            var result = await _userManager.CreateAsync(user, register.Password);
            if(!result.Succeeded) return BadRequest();
            return Ok(register);
        }
    }

    public class Register
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}