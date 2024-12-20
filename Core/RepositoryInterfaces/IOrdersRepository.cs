using Core.Aggregates;
using Core.Entities;
using Core.Utils;
using Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.RepositoryInterfaces
{
    public interface IOrdersRepository
    {
        Task<IDTO<Order>> GetDtoByIdAsync(Guid id, bool withItems = false);
        Task<List<IDTO<Order>>> GetDtosAsync(int page, int pageSize, bool withItems = false);
        Task<List<IDTO<Order>>> GetDtosBySpecificationAsync(ISpecification<IDTO<Order>> spec, int page, int pageSize, bool withItems = false);
        Task<Guid> CreateAsync(Order order);
        Task ChangeStatusAsync(Guid id, OrderStatus newStatus);
        Task DeleteAsync(Guid id);
    }
}
