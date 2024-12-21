using ECommerce.Core.Aggregates;
using ECommerce.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DAL.Repositories
{
    public class AccountsRepository
    {
        private readonly ECommerceDbContext _dbContext;

        public AccountsRepository(ECommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> CreateAsync(AccountEntity account)
        {
            var entry = await _dbContext.Accounts.AddAsync(account);
            return entry.Entity.Id;
        }

        public async Task<Guid> EditAsync(AccountEntity account)
        {
            var entity = await _dbContext.Accounts.FirstOrDefaultAsync(i => i.Id == account.Id);
            if (entity == null)
            {
                throw new NullReferenceException("Customer not found");
            }
            _dbContext.Accounts.Update(entity);
            return entity.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _dbContext.Accounts.FirstOrDefaultAsync(i => i.Id == id);
            if (entity != null)
            {
                throw new NullReferenceException("Customer not found");
            }
            _dbContext.Accounts.Remove(entity);
        }
    }
}
