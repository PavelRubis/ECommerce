using ECommerce.Core.Aggregates;
using ECommerce.Core.Entities;
using ECommerce.Core.OtherInterfaces;
using ECommerce.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.RepositoryInterfaces
{
    public interface IOrdersRepository
    {
        Task<IOrderDTO> GetDtoByIdAsync(Guid id, bool withItems = false);
        Task<List<IOrderDTO>> GetDtosByStatusAsync(string starusStr, int page, int pageSize, bool withItems = false);
        Task<Guid> CreateAsync(Order order);
        Task<Guid> ChangeStatusAsync(Guid id, OrderStatus orderStatus);
        Task DeleteAsync(Guid id);
    }
}
