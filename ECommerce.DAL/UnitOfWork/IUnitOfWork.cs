using ECommerce.Core.Aggregates;
using ECommerce.Core.RepositoryInterfaces;

namespace ECommerce.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IOrdersRepository OrdersRepository { get; }
        ICRUDRepository<Item> ItemsRepository { get; }
        IAccountsRepository AccountsRepository { get; }
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
