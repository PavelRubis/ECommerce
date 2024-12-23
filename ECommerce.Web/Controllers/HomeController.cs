using AutoMapper;
using ECommerce.Application.Interfaces;
using ECommerce.DAL.UnitOfWork;
using ECommerce.Web.DTOs;
using ECommerce.Web.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ECommerce.Web.Controllers
{
    [Route("api/home")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly JwtOptions _options;
        public HomeController(IAuthService authService, IOptions<JwtOptions> options)
        {
            _authService = authService;
            _options = options.Value;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var token = await _authService.LoginAsync(loginDTO.Username, loginDTO.Password);
            HttpContext.Response.Cookies.Append(_options.CookieName, token);
            return Ok();
        }
    }
}
