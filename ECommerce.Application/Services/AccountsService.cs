using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using ECommerce.Application.RepositoryInterfaces;
using ECommerce.Application.ServiceInterfaces;

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

        public async Task<Guid> CreateAsync(AccountInWebDTO account)
        {
            account.Password = _passwordHasher.Hash(account.Password); ;
            var accId = await _accountsRepository.CreateAsync(account);
            return accId;
        }

        public async Task<Guid> EditAsync(AccountInWebDTO account)
        {
            account.Password = _passwordHasher.Hash(account.Password); ;
            var accId = await _accountsRepository.EditAsync(account);
            return accId;
        }
    }
}
