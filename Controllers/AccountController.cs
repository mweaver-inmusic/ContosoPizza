using System.Collections.Generic;
using System.Threading.Tasks;
using ContosoPizza.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ITokenService _tokenService;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Authorize]
        [HttpGet("users")]
        public async Task<List<IdentityUser>> GetAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        [HttpPost("login")]
        public async Task<IActionResult> PostAsync(Login login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user == null) return BadRequest("User with that email does not exist");

            var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);
            if (!result.Succeeded) return BadRequest("Incorrect password");

            return Ok(new Jwt{ Token = _tokenService.CreateToken(user) });
        }

        [HttpPost("register")]
        public async Task<IActionResult> PostAsync(Register register)
        {
            var user = await _userManager.FindByEmailAsync(register.Email);
            if (user != null) return BadRequest("User already in system");

            user = new IdentityUser { Email = register.Email, UserName = register.Email };
            var result = await _userManager.CreateAsync(user, register.Password);

            if (!result.Succeeded) return BadRequest();
            return Ok(register);
        }

        [HttpGet("signout")]
        public async Task Signout()
        {
            await _signInManager.SignOutAsync();
        }
    }

    public class Login
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class Register
    {
        // public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class Jwt
    {
        public string Token { get; set; }
    }
}