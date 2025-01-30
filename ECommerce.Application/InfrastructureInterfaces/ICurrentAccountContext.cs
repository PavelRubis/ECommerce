using ECommerce.Core.Aggregates;

namespace ECommerce.Application.InfrastructureInterfaces
{
    public interface ICurrentAccountContext
    {
        Account? CurrentAccount { get; }
    }
}
