using AutoMapper;
using AutoMapper.QueryableExtensions;
using ECommerce.Core.Aggregates;
using ECommerce.Core.OtherInterfaces;
using ECommerce.DAL.DTOs;
using ECommerce.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
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

        public async Task<AccountInWebDTO> GetByUsernameWithPassword(string username)
        {
            var entity = await _dbContext.Accounts
                .AsNoTracking()
                .Include(a => a.Customer)
                .ProjectTo<AccountInWebDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(i => i.Username == username);
            if (entity == null)
            {
                throw new NullReferenceException("Account not found");
            }
            return _mapper.Map<AccountInWebDTO>(entity);
        }

        public async Task<AccountOutWebDTO> GetByIdAsync(Guid id)
        {
            var entity = await _dbContext.Accounts
                .AsNoTracking()
                .Include(a => a.Customer)
                .ProjectTo<AccountOutWebDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(i => i.Id == id);
            if (entity == null)
            {
                throw new NullReferenceException("Account not found");
            }
            return entity;
        }

        public async Task<List<AccountOutWebDTO>> GetAllAsync()
        {
            var accounts = await _dbContext.Accounts
                .AsNoTracking()
                .Include(e => e.Customer)
                .ProjectTo<AccountOutWebDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return accounts;
        }

        public async Task<List<AccountOutWebDTO>> GetByPageAsync(int page, int pageSize)
        {
            var accounts = await _dbContext.Accounts
                .AsNoTracking()
                .Include(e => e.Customer)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<AccountOutWebDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return accounts;
        }

        public async Task<Guid> CreateAsync(AccountInWebDTO account)
        {
            var entry = await _dbContext.Accounts.AddAsync(_mapper.Map<AccountEntity>(account));
            return entry.Entity.Id;
        }

        public async Task<Guid> EditAsync(AccountInWebDTO account)
        {
            var entity = await _dbContext.Accounts
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == account.Id);
            if (entity == null)
            {
                throw new NullReferenceException("Customer not found");
            }
            _dbContext.Accounts.Update(_mapper.Map<AccountEntity>(account));
            return account.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _dbContext.Accounts.FirstOrDefaultAsync(i => i.Id == id);
            if (entity == null)
            {
                throw new NullReferenceException("Customer not found");
            }
            _dbContext.Accounts.Remove(entity);
        }
    }
}
