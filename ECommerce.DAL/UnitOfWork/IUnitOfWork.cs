using ECommerce.Core.Aggregates;
using ECommerce.Core.RepositoryInterfaces;
using ECommerce.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IOrdersRepository OrdersRepository { get; }
        ICRUDRepository<Item> ItemsRepository { get; }
        ICRUDRepository<Customer> CustomersRepository { get; }
        AccountsRepository AccountsRepository { get; }
        void Commit();
        void Rollback();
    }
}
