using ECommerce.Application.Interfaces;
using ECommerce.Application.RepositoryInterfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ECommerce.Web.Infrastructure.Auth
{
    public class JwtAuthenticationEvents : JwtBearerEvents
    {
        public JwtOptions JwtOptions { get; }

        public JwtAuthenticationEvents(JwtOptions jwtOptions)
            : base()
        {
            this.JwtOptions = jwtOptions;
        }

        public override Task MessageReceived(MessageReceivedContext context)
        {
            context.Token = context.Request.Cookies[this.JwtOptions.CookieName];
            return Task.CompletedTask;
        }

        public override async Task TokenValidated(TokenValidatedContext context)
        {
            var accIdClaim = context.Principal?.FindFirst(this.JwtOptions.AccountIdClaimName);
            if (accIdClaim == null || !Guid.TryParse(accIdClaim.Value, out var accId))
            {
                context.Fail("Account not found");
                return;
            }

            var accountsRepo = context.HttpContext.RequestServices.GetRequiredService<IAccountsRepository>();

            try
            {
                var acc = await accountsRepo.GetByIdAsync(accId);
                context.Success();
            }
            catch (NullReferenceException ex)
            {
                context.Fail(ex.Message);
            }
        }
    }
}
