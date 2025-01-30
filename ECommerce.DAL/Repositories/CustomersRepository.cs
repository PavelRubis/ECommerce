using AutoMapper;
using ECommerce.Core.Entities;
using ECommerce.Core.RepositoryInterfaces;
using ECommerce.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.DAL.Repositories
{
    public class CustomersRepository : ICustomerRepository
    {
        private readonly ECommerceDbContext _dbContext;
        private readonly IMapper _mapper;

        public CustomersRepository(ECommerceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Guid> CreateAsync(Customer customer)
        {
            var entry = await _dbContext.Customers.AddAsync(_mapper.Map<CustomerEntity>(customer));
            return entry.Entity.Id;
        }

        public async Task<Guid> CreateAsync(Customer customer, AccountEntity accountEntity)
        {
            var entity = _mapper.Map<CustomerEntity>(customer);
            entity.AccountId = accountEntity.Id;
            entity.Account = accountEntity;
            var entry = await _dbContext.Customers.AddAsync(entity);
            return entry.Entity.Id;
        }

        public async Task<Guid> EditAsync(Customer customer)
        {
            var entity = await _dbContext.Customers.FirstOrDefaultAsync(i => i.Id == customer.Id);
            if (entity == null)
            {
                throw new NullReferenceException("Customer not found");
            }
            entity = _mapper.Map<CustomerEntity>(customer);
            return customer.Id;
        }
    }
}