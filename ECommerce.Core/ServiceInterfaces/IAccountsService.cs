using ECommerce.Core.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.ServiceInterfaces
{
    public interface IAccountsService
    {
        Task<Guid> CreateAsync(Account account);
        Task<Guid> EditAsync(Account account);
    }
}
