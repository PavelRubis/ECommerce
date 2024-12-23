using ECommerce.Application.Interfaces;
using ECommerce.DAL.DTOs;
using ECommerce.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Services
{
    public class AccountsService : IAccountsService
    {
        private readonly AccountsRepository _accountsRepository;
        private readonly IPasswordHasher _passwordHasher;

        public AccountsService(AccountsRepository accountsRepository, IPasswordHasher passwordHasher)
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
