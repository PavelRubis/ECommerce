using ECommerce.Core.Aggregates;
using ECommerce.Core.Interfaces;

namespace ECommerce.Core.RepositoryInterfaces
{
    public interface IAccountsRepository
    {
        Task<Account> GetByUsername(string username);
        Task<Account> GetByIdAsDtoAsync(Guid id);
        Task<IAccountProjection> GetByIdAsProjectionAsync(Guid id);
        Task<List<IAccountProjection>> GetByPageAsProjectionAsync(int page, int pageSize);
        Task<List<IAccountProjection>> GetAllAsProjectionsAsync();
        Task<Guid> CreateAsync(Account item);
        Task<Guid> EditAsync(Account item);
        Task DeleteAsync(Guid id);
    }
}
