using ECommerce.Core.Aggregates;
using ECommerce.Core.Entities;
using ECommerce.Core.Utils;
using ECommerce.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.RepositoryInterfaces
{
    public interface IOrdersRepository
    {
        Task<IDTO<Order>> GetDtoByIdAsync(Guid id, bool withItems = false);
        Task<List<IDTO<Order>>> GetDtosAsync(int page, int pageSize, bool withItems = false);
        Task<List<IDTO<Order>>> GetDtosBySpecificationAsync(IOrderSpecification spec, int page, int pageSize, bool withItems = false);
        Task<Guid> CreateAsync(Order order);
        Guid EditAsync(Order order);
        Task DeleteAsync(Guid id);
    }
}
