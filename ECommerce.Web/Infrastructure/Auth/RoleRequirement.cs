using ECommerce.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using ECommerce.Application.Enums;

namespace ECommerce.Web.Infrastructure.Auth
{
    public class RoleRequirement : IAuthorizationRequirement
    {
        public IEnumerable<AppRole> PermitedRole { get; }

        public RoleRequirement(params AppRole[] requiredRole)
        {
            PermitedRole = requiredRole;
        }
    }
}
