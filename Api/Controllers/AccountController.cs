using Api.Attributes;
using Api.Models.Accounts;
using Application.Exceptions;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _singInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _singInManager = signInManager;
        }

        [HttpPost("register")]
        [ValidateModel]
        public async Task<IActionResult> Register([FromBody] RegisterDto register)
        {
            var user = new IdentityUser()
            {
                UserName = register.UserName,
            };

            var result = await _userManager.CreateAsync(user, register.Password);
            
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).Aggregate((res, x) => res += $"{x}\n");
                throw new ConflictException(errors);
            }

            user = await _userManager.FindByNameAsync(register.UserName);
            var principal = await _singInManager.CreateUserPrincipalAsync(user);

            return SignIn(principal, CookieAuthenticationDefaults.AuthenticationScheme);
        }

        [HttpPost("login")]
        [ValidateModel]
        public async Task<IActionResult> Login(LoginDto login)
        {
            var user = await _userManager.FindByNameAsync(login.Name);
            if (user == null)
            {
                throw new BadRequestException("Неверный логин или пароль");
            }

            var result = await _singInManager.CheckPasswordSignInAsync(user, login.Password, false);
            if (!result.Succeeded)
            {
                throw new BadRequestException("Неверный логин или пароль");
            }

            var principal = await _singInManager.CreateUserPrincipalAsync(user);

            return SignIn(principal, CookieAuthenticationDefaults.AuthenticationScheme);
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }
    }
}
