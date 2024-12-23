using ECommerce.Core.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.RepositoryInterfaces
{
    public interface ICustomerRepository
    {
        Task<Guid> CreateAsync(Customer customer);
        Task<Guid> EditAsync(Customer customer);
    }
}
