using ECommerce.Core.RepositoryInterfaces;
using Microsoft.AspNetCore.Authorization;

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
            var accIdClaim = context.User?.FindFirst(AuthConstants.AccountIdContextKey);
            if (accIdClaim == null || !Guid.TryParse(accIdClaim.Value, out var accId))
            {
                context.Fail();
                return;
            }

            using var scope = _scopeFactory.CreateScope();
            var contextAccessor = scope.ServiceProvider.GetRequiredService<IHttpContextAccessor>();
            if (contextAccessor?.HttpContext == null)
            {
                context.Fail();
            }
            var accountsRepo = scope.ServiceProvider.GetRequiredService<IAccountsRepository>();
            try
            {
                var acc = await accountsRepo.GetByIdAsDtoAsync(accId);
                if (acc == null || !requirement.PermitedRole.Contains(acc.Role))
                {
                    context.Fail();
                    return;
                }
                contextAccessor.HttpContext.Items.Add(AuthConstants.AccountContextKey, acc);
                context.Succeed(requirement);
            }
            catch (NullReferenceException ex)
            {
                context.Fail();
                return;
            }
        }
    }

}
