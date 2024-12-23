using ECommerce.Application.Interfaces;
using ECommerce.DAL.DTOs;
using ECommerce.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly AccountsRepository _accountsRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;

        public AuthService(AccountsRepository accountsRepository, IPasswordHasher passwordHasher, IJwtProvider jwtProvider)
        {
            _accountsRepository = accountsRepository;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
        }

        public async Task<string> LoginAsync(string username, string password)
        {
            var accDto = await _accountsRepository.GetByUsernameWithPassword(username);
            var result = _passwordHasher.Verify(password, accDto.Password);
            
            if (!result)
            {
                throw new AuthenticationException("Invalid credentials");
            }

            return _jwtProvider.Generate(accDto);
        }
    }
}
