using ECommerce.Core.Aggregates;
using ECommerce.Core.Entities;
using ECommerce.Core.Interfaces;
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
        Task<IDTO<Order>> GetOwnDtoByIdAsync(Customer customer, Guid id, bool withItems = false);
        Task<List<IDTO<Order>>> GetOwnDtosAsync(Customer customer, bool withItems = false);
        Task<List<IDTO<Order>>> GetOwnDtosByStatusAsync(Customer customer, string starusStr, int page, int pageSize, bool withItems = false);
        Task<IDTO<Order>> GetDtoByIdAsync(Guid id, bool withItems = false);
        Task<List<IDTO<Order>>> GetDtosAsync(bool withItems = false);
        Task<List<IDTO<Order>>> GetDtosByStatusAsync(string starusStr, int page, int pageSize, bool withItems = false);
        Task<Guid> CreateAsync(Order order);
        Task<Guid> ChangeStatusAsync(Guid id, OrderStatus orderStatus);
        Task DeleteAsync(Guid id);
    }
}
