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
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ECommerceDbContext _context;
        private IOrdersRepository _ordersRepository;
        private ICRUDRepository<Item> _itemsRepository;
        private ICRUDRepository<Customer> _customersRepository;
        private AccountsRepository _accountsRepository;

        public UnitOfWork(ECommerceDbContext context)
        {
            _context = context;
        }

        public IOrdersRepository OrdersRepository
        {
            get { return _ordersRepository ??= new OrdersRepository(_context); }
        }

        public ICRUDRepository<Item> ItemsRepository
        {
            get { return _itemsRepository ??= new ItemsRepository(_context); }
        }
        public ICRUDRepository<Customer> CustomersRepository
        {
            get { return _customersRepository ??= new CustomersRepository(_context); }
        }
        public AccountsRepository AccountsRepository
        {
            get { return _accountsRepository ??= new AccountsRepository(_context); }
        }

        public void Commit()
        {
            _context.SaveChanges();
            _context.CommitTransaction();
        }

        public void Rollback()
        {
            _context.RollbackTransaction();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
