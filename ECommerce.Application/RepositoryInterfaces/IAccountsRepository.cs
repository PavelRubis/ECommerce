using ECommerce.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.RepositoryInterfaces
{
    public interface IAccountsRepository
    {
        Task<AccountInWebDTO> GetByUsernameWithPassword(string username);
        Task<AccountOutWebDTO> GetByIdAsync(Guid id);
        Task<List<AccountOutWebDTO>> GetAllAsync();
        Task<List<AccountOutWebDTO>> GetByPageAsync(int page, int pageSize);
        Task<Guid> CreateAsync(AccountInWebDTO account);
        Task<Guid> EditAsync(AccountInWebDTO account);
        Task DeleteAsync(Guid id);
    }
}
