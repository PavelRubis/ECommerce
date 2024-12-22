using AutoMapper;
using ECommerce.Core.Aggregates;
using ECommerce.DAL.DTOs;
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
        private readonly IMapper _mapper;

        public AccountsRepository(ECommerceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Guid> CreateAsync(AccountWebDTO account)
        {
            var entry = await _dbContext.Accounts.AddAsync(_mapper.Map<AccountEntity>(account));
            return entry.Entity.Id;
        }

        public async Task<Guid> EditAsync(AccountWebDTO account)
        {
            var entity = await _dbContext.Accounts.AsNoTracking().FirstOrDefaultAsync(i => i.Id == account.Id);
            if (entity == null)
            {
                throw new NullReferenceException("Customer not found");
            }
            _dbContext.Accounts.Update(_mapper.Map<AccountEntity>(account));
            return account.Id;
        }
    }
}
