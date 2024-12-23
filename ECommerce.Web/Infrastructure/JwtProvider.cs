using ECommerce.DAL.DTOs;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ECommerce.Application.Interfaces;

namespace ECommerce.Web.Infrastructure
{
    public class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _options;
        public JwtProvider(IOptions<JwtOptions> options)
        {
            _options = options.Value;
        }

        public string Generate(AccountInWebDTO account)
        {
            var claims = new List<Claim>()
            {
                new("AccountId", account.Id.ToString()),
                new("Role", account.Role)
            };

            var jwtCreds = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
                SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: jwtCreds,
                expires: DateTime.UtcNow.AddHours(_options.ExpiresHours)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
