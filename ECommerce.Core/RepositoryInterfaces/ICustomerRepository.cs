using ECommerce.Core.Entities;

namespace ECommerce.Core.RepositoryInterfaces
{
    public interface ICustomerRepository
    {
        Task<Guid> CreateAsync(Customer customer);
        Task<Guid> EditAsync(Customer customer);
    }
}
