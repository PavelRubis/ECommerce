using ECommerce.Application.Interfaces;
using ECommerce.Application.RepositoryInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Web.Infrastructure.Auth
{
    public class RoleRequirementHandler : AuthorizationHandler<RoleRequirement>
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public RoleRequirementHandler(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
        {
            var accIdClaim = context.User?.FindFirst(AuthConstants.AccountIdClaimType);
            if (accIdClaim == null || !Guid.TryParse(accIdClaim.Value, out var accId))
            {
                context.Fail();
                return;
            }

            using var scope = _scopeFactory.CreateScope();
            var accountsRepo = scope.ServiceProvider.GetRequiredService<IAccountsRepository>();
            try
            {
                var acc = await accountsRepo.GetByIdAsync(accId);
                if (acc == null || !requirement.PermitedRole.Contains(acc.Role))
                {
                    context.Fail();
                    return;
                }
            }
            catch (NullReferenceException ex)
            {
                context.Fail();
                return;
            }
            context.Succeed(requirement);
        }
    }

}
