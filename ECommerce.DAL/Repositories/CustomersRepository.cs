using ECommerce.Core.Aggregates;
using ECommerce.DAL.Models;
using Microsoft.EntityFrameworkCore;
using ECommerce.Core.RepositoryInterfaces;
using ECommerce.Core.Utils;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ECommerce.DAL.DTOs;

namespace ECommerce.DAL.Repositories
{
    public class CustomersRepository : ICRUDRepository<Customer>
    {
        private readonly ECommerceDbContext _dbContext;
        private readonly IMapper _mapper;

        public CustomersRepository(ECommerceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IDTO<Customer>> GetDtoByIdAsync(Guid id)
        {
            var entity = await _dbContext.Customers
                .AsNoTracking()
                .Include(e => e.Account)
                .ProjectTo<CustomerWebDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(i => i.Id == id);
            if (entity == null)
            {
                throw new NullReferenceException("Customer not found");
            }
            return entity;
        }

        public async Task<List<IDTO<Customer>>> GetAllDtosAsync()
        {
            var customers = await _dbContext.Customers
                .AsNoTracking()
                .Include(e => e.Account)
                .ProjectTo<CustomerWebDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return new List<IDTO<Customer>>(customers);
        }

        public async Task<List<IDTO<Customer>>> GetDtosByPageAsync(int page, int pageSize)
        {
            var customers = await _dbContext.Customers
                .AsNoTracking()
                .Include(e => e.Account)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<CustomerWebDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return new List<IDTO<Customer>>(customers);
        }

        public async Task<Guid> CreateAsync(Customer customer)
        {
            var entry = await _dbContext.Customers.AddAsync(_mapper.Map<CustomerEntity>(customer));
            return entry.Entity.Id;
        }

        public async Task<Guid> EditAsync(Customer customer)
        {
            var entity = await _dbContext.Customers.AsNoTracking().FirstOrDefaultAsync(i => i.Id == customer.Id);
            if (entity == null)
            {
                throw new NullReferenceException("Customer not found");
            }
            _dbContext.Customers.Update(_mapper.Map<CustomerEntity>(customer));
            return customer.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _dbContext.Customers.FirstOrDefaultAsync(i => i.Id == id);
            if (entity == null)
            {
                throw new NullReferenceException("Customer not found");
            }
            _dbContext.Customers.Remove(entity);
        }
    }
}