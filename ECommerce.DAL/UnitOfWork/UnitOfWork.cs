using AutoMapper;
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
        private IMapper _mapper;

        public UnitOfWork(ECommerceDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
            get { return _customersRepository ??= new CustomersRepository(_context, _mapper); }
        }

        public AccountsRepository AccountsRepository
        {
            get { return _accountsRepository ??= new AccountsRepository(_context, _mapper); }
        }
        public void BeginTransaction()
        {
            _context.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _context.SaveChanges();
            _context.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            _context.RollbackTransaction();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
