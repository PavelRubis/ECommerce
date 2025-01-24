using ECommerce.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.ServiceInterfaces
{
    public interface IAccountsService
    {
        Task<Guid> CreateAsync(AccountInWebDTO account);
        Task<Guid> EditAsync(AccountInWebDTO account);
    }
}
