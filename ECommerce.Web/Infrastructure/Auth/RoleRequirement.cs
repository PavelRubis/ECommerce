using ECommerce.Core.Aggregates;
using Microsoft.AspNetCore.Authorization;

namespace ECommerce.Web.Infrastructure.Auth
{
    public class RoleRequirement : IAuthorizationRequirement
    {
        public IEnumerable<AccountRole> PermitedRole { get; }

        public RoleRequirement(params AccountRole[] requiredRole)
        {
            PermitedRole = requiredRole;
        }
    }
}
