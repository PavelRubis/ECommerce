using ECommerce.Core.Aggregates;
using ECommerce.Core.ServiceInterfaces;

namespace ECommerce.Core.Interfaces
{
    public interface IAccountProjection : IProjection<Account>
    {
        public AccountRole Role { get; set; }
    }
}
