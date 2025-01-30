using ECommerce.Application.Interfaces;
using ECommerce.Core.Aggregates;
using ECommerce.Core.RepositoryInterfaces;
using ECommerce.Core.ServiceInterfaces;

namespace ECommerce.Application.Services
{
    public class AccountsService : IAccountsService
    {
        private readonly IAccountsRepository _accountsRepository;
        private readonly IPasswordHasher _passwordHasher;

        public AccountsService(IAccountsRepository accountsRepository, IPasswordHasher passwordHasher)
        {
            _accountsRepository = accountsRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<Guid> CreateAsync(Account account)
        {
            account.Password = _passwordHasher.Hash(account.Password);
            var accId = await _accountsRepository.CreateAsync(account);
            return accId;
        }

        public async Task<Guid> EditAsync(Account account)
        {
            account.Password = _passwordHasher.Hash(account.Password);
            var accId = await _accountsRepository.EditAsync(account);
            return accId;
        }
    }
}
