using AutoMapper;
using AutoMapper.QueryableExtensions;
using ECommerce.Application.DTOs;
using ECommerce.Core.Aggregates;
using ECommerce.Core.Interfaces;
using ECommerce.Core.RepositoryInterfaces;
using ECommerce.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.DAL.Repositories
{
    public class AccountsRepository : IAccountsRepository
    {
        private readonly ECommerceDbContext _dbContext;
        private readonly IMapper _mapper;

        public AccountsRepository(ECommerceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Account> GetByUsername(string username)
        {
            var accDto = await _dbContext.Accounts
                .AsNoTracking()
                .Where(a => a.Username == username && !a.IsDeleted)
                .Include(a => a.Customer)
                .ProjectTo<AccountWebDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            if (accDto == null)
            {
                return null;
            }
            return accDto.GetOriginalObject();
        }

        public async Task<Account> GetByIdAsDtoAsync(Guid id)
        {
            var accDto = await _dbContext.Accounts
                .AsNoTracking()
                .Where(a => a.Id == id && !a.IsDeleted)
                .Include(a => a.Customer)
                .ProjectTo<AccountWebDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            if (accDto == null)
            {
                return null;
            }
            return accDto.GetOriginalObject();
        }

        public async Task<IAccountProjection> GetByIdAsProjectionAsync(Guid id)
        {
            var entity = await _dbContext.Accounts
                .AsNoTracking()
                .Where(a => !a.IsDeleted && a.Id == id)
                .Include(a => a.Customer)
                .ProjectTo<AccountSafeProjection>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            if (entity == null)
            {
                return null;
            }
            return entity;
        }

        public async Task<List<IAccountProjection>> GetByPageAsProjectionAsync(int page, int pageSize)
        {
            var accounts = await _dbContext.Accounts
                .AsNoTracking()
                .Where(a => !a.IsDeleted)
                .Include(a => a.Customer)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<AccountSafeProjection>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return new List<IAccountProjection>(accounts);
        }

        public async Task<List<IAccountProjection>> GetAllAsProjectionsAsync()
        {
            var accounts = await _dbContext.Accounts
                .AsNoTracking()
                .Where(a => !a.IsDeleted)
                .Include(a => a.Customer)
                .ProjectTo<AccountSafeProjection>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return new List<IAccountProjection>(accounts);
        }

        public async Task<Guid> CreateAsync(Account account)
        {
            var entry = await _dbContext.Accounts.AddAsync(_mapper.Map<AccountEntity>(account));
            return entry.Entity.Id;
        }

        public async Task<Guid> EditAsync(Account account)
        {
            var entity = await _dbContext.Accounts
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == account.Id);
            if (entity == null)
            {
                throw new NullReferenceException("Account not found");
            }
            _dbContext.Accounts.Update(_mapper.Map<AccountEntity>(account));
            return account.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _dbContext.Accounts.Include(a => a.Customer).FirstOrDefaultAsync(i => i.Id == id);
            if (entity == null)
            {
                throw new NullReferenceException("Account not found");
            }
            entity.IsDeleted = true;
            if (entity.IsDeleted)
            {

            }
            entity.Customer.IsDeleted = true;
        }
    }
}
