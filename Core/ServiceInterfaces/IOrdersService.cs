using Core.Aggregates;
using Core.Entities;
using Core.ValueObjects;
using Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ServiceInterfaces
{
    public interface IOrdersService
    {
        ISpecification<IDTO<Order>> CanBeDeletedSpec { get; }
        ISpecification<IDTO<Order>> HasSuchStatusSpec { get; }
        Task<IEnumerable<IDTO<Order>>> GetbyStatusAsync(string statusStr);
        Task SubmitShippingAsync(Guid id, long orderNumber, DateTime shipmentDate);
        Task CompleteAsync(Guid id);
    }
}
