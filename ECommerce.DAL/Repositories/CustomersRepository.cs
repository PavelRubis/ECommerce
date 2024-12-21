using ECommerce.Core.Aggregates;
using ECommerce.DAL.Models;
using Microsoft.EntityFrameworkCore;
using ECommerce.Core.RepositoryInterfaces;
using ECommerce.Core.Utils;

namespace ECommerce.DAL.Repositories
{
    public class CustomersRepository : ICRUDRepository<Customer>
    {
        private readonly ECommerceDbContext _dbContext;

        public CustomersRepository(ECommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IDTO<Customer>> GetDtoByIdAsync(Guid id)
        {
            var entity = await _dbContext.Customers
                .AsNoTracking()
                .Include(e => e.Account)
                .FirstOrDefaultAsync(i => i.Id == id);
            if (entity == null)
            {
                throw new NullReferenceException("Customer not found");
            }
            return entity;
        }

        public async Task<List<IDTO<Customer>>> GetAllDtosAsync()
        {
            return await _dbContext.Customers
                .AsNoTracking()
                .Include(e => e.Account)
                .Select(e => e as IDTO<Customer>)
                .ToListAsync();
        }

        public async Task<List<IDTO<Customer>>> GetDtosByPageAsync(int page, int pageSize)
        {
            return await _dbContext.Customers
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Include(e => e.Account)
                .Select(e => e as IDTO<Customer>)
                .ToListAsync();
        }

        public async Task<Guid> CreateAsync(Customer customer)
        {
            var customerEntity = new CustomerEntity();
            customerEntity.SetDataFromObject(customer);
            var entry = await _dbContext.Customers.AddAsync(customerEntity);
            return entry.Entity.Id;
        }

        public async Task<Guid> EditAsync(Customer customer)
        {
            var entity = await _dbContext.Customers.FirstOrDefaultAsync(i => i.Id == customer.Id);
            if (entity == null)
            {
                throw new NullReferenceException("Customer not found");
            }
            entity.SetDataFromObject(customer);
            _dbContext.Customers.Update(entity);
            return entity.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _dbContext.Customers.FirstOrDefaultAsync(i => i.Id == id);
            if (entity != null)
            {
                throw new NullReferenceException("Customer not found");
            }
            _dbContext.Customers.Remove(entity);
        }
    }
}