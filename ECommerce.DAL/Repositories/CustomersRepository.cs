using ECommerce.Core.Aggregates;
using ECommerce.DAL.Models;
using Microsoft.EntityFrameworkCore;
using ECommerce.Core.RepositoryInterfaces;
using ECommerce.Core.OtherInterfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ECommerce.DAL.DTOs;

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