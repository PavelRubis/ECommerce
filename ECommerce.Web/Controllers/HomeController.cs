using ECommerce.Application.Interfaces;
using ECommerce.Application.RepositoryInterfaces;
using ECommerce.Web.DTOs;
using ECommerce.Web.Infrastructure.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Authentication;

namespace ECommerce.Web.Controllers
{
    [Route("api/home")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IAccountsRepository _accountsRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;
        private readonly JwtOptions _options;

        public HomeController(IAccountsRepository accountsRepository, IPasswordHasher passwordHasher, IJwtProvider jwtProvider, IOptions<JwtOptions> options)
        {
            _accountsRepository = accountsRepository;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
            _options = options.Value;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var accDto = await _accountsRepository.GetByUsernameWithPassword(loginDTO.Username);
            var result = _passwordHasher.Verify(loginDTO.Password, accDto?.Password ?? string.Empty);

            if (!result)
            {
                throw new AuthenticationException("Invalid credentials");
            }

            var token = _jwtProvider.Generate(accDto);
            HttpContext.Response.Cookies.Append(_options.CookieName, token);
            return Ok(new { id = accDto.Id, customerId = accDto?.Customer?.Id, role = accDto.Role });
        }

        [HttpPost("Logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Response.Cookies.Append(_options.CookieName, string.Empty);
            return Ok();
        }
    }
}
