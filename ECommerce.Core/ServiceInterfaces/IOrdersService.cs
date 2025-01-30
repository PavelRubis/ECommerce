using ECommerce.Core.Aggregates;
using ECommerce.Core.Entities;
using ECommerce.Core.ValueObjects;
using ECommerce.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.ServiceInterfaces
{
    public interface IOrdersService
    {
        Task<IDTO<Order>> Get(Guid id);
        Task<List<IDTO<Order>>> GetDtosAsync(bool withItems = false);
        Task<List<IDTO<Order>>> GetDtosByStatusAsync(string statusStr, int page, int pageSize, bool withItems = false);
        Task SubmitShippingAsync(Guid id, DateTime shipmentDate);
        Task CompleteAsync(Guid id);
        Task DeleteOwnAsync(Guid id);
    }
}
