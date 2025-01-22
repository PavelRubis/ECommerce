using ECommerce.Core.Aggregates;
using ECommerce.Core.Entities;
using ECommerce.Core.ValueObjects;
using ECommerce.Core.DTOsInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.ServiceInterfaces
{
    public interface IOrdersService
    {
        Task SubmitShippingAsync(Guid id, DateTime shipmentDate);
        Task CompleteAsync(Guid id);
        Task DeleteAsync(Guid id);
    }
}
