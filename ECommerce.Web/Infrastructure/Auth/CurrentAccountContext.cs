using ECommerce.Application.InfrastructureInterfaces;
using ECommerce.Core.Aggregates;

namespace ECommerce.Web.Infrastructure.Auth
{
    public class CurrentAccountContext : ICurrentAccountContext
    {
        private Account? _acc;

        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentAccountContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Account? CurrentAccount
        {
            get
            {
                if (_acc != null)
                {
                    return _acc;
                }
                var hasAcc = _httpContextAccessor?.HttpContext?.Items?.ContainsKey(AuthConstants.AccountContextKey) == true;
                if (hasAcc && _httpContextAccessor?.HttpContext?.Items[AuthConstants.AccountContextKey] is Account account)
                {
                    _acc = account;
                }
                return _acc;
            }
        }
    }
}
