using ECommerce.Core.Aggregates;
using ECommerce.Core.Entities;
using ECommerce.Core.ValueObjects;
using ECommerce.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.ServiceInterfaces
{
    public interface IOrdersService
    {
        IOrderSpecification ByStatusSpec(string statusStr);
        Task<List<IDTO<Order>>> GetbyStatusAsync(string statusStr, int page, int pageSize, bool withItems = false);
        Task SubmitShippingAsync(Guid id, DateTime shipmentDate);
        Task CompleteAsync(Guid id);
        Task DeleteAsync(Guid id);
    }
}
