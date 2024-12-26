using AutoMapper;
using ECommerce.Application.Interfaces;
using ECommerce.Core.Aggregates;
using ECommerce.DAL.Repositories;
using ECommerce.DAL.UnitOfWork;
using ECommerce.Web.DTOs;
using ECommerce.Web.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Data;
using System.Security.Authentication;
using Web;

namespace ECommerce.Web.Controllers
{
    [Route("api/home")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly AccountsRepository _accountsRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;
        private readonly JwtOptions _options;

        public HomeController(AccountsRepository accountsRepository, IPasswordHasher passwordHasher, IJwtProvider jwtProvider, IOptions<JwtOptions> options)
        {
            _accountsRepository = accountsRepository;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
            _options = options.Value;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var accDto = await _accountsRepository.GetByUsernameWithPassword(loginDTO.Username);
            var result = _passwordHasher.Verify(loginDTO.Password, accDto.Password);

            if (!result)
            {
                throw new AuthenticationException("Invalid credentials");
            }

            var token = _jwtProvider.Generate(accDto);
            HttpContext.Response.Cookies.Append(_options.CookieName, token);
            return Ok(new { id = accDto.Id, customerId = accDto?.Customer?.Id, role = accDto.Role });
        }

        [Authorize]
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Response.Cookies.Append(_options.CookieName, string.Empty);
            return Ok();
        }
    }
}
