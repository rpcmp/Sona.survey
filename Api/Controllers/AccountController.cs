using Api.Models.Accounts;
using Application.Exceptions;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
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
        public async Task<IActionResult> Register([FromBody] RegisterDto register)
        {
            if (string.IsNullOrEmpty(register.UserName))
            {
                throw new BadRequestException("Необходимо указать имя пользователя");
            }

            if (string.IsNullOrEmpty(register.Password))
            {
                throw new BadRequestException("Необходимо указать пароль");
            }

            if (string.IsNullOrEmpty(register.ConfirmPassword))
            {
                throw new BadRequestException("Необходимо подтвердить пароль");
            }

            if (!string.Equals(register.Password, register.ConfirmPassword))
            {
                throw new BadRequestException("Пароли не совпадают");
            }

            var existsUser = await _userManager.FindByNameAsync(register.UserName);
            if (existsUser != null)
            {
                throw new BadRequestException("Пользователь с таким именем уже существует");
            }

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
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            if (string.IsNullOrEmpty(login.Name))
            {
                throw new BadRequestException("Необходимо указать имя пользователя");
            }

            if (string.IsNullOrEmpty(login.Password))
            {
                throw new BadRequestException("Необходимо указать пароль");
            }

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
            return NoContent();
        }
    }
}
