using ECommerce.Core.Aggregates;
using ECommerce.Web.Infrastructure.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ECommerce.Web.Extensions
{
    public static class Extensions
    {
        public static void AddJwtAuthenticationAndAuthorization(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
                    };
                    options.Events = new JwtBearerEvents()
                    {
                        OnMessageReceived = (context) =>
                        {
                            context.Token = context.Request.Cookies[jwtOptions.CookieName];
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(AuthConstants.ManagerRolePolicy, policy =>
                {
                    policy.AddRequirements(new RoleRequirement(AccountRole.Manager));
                });

                options.AddPolicy(AuthConstants.CustomerRolePolicy, policy =>
                {
                    policy.AddRequirements(new RoleRequirement(AccountRole.Customer));
                });

                options.AddPolicy(AuthConstants.AnyRolePolicy, policy =>
                {
                    policy.AddRequirements(new RoleRequirement(AccountRole.Customer, AccountRole.Manager));
                });
            });
        }
    }
}
