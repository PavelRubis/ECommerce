using Core.Aggregates;
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
        ISpecification<IProjection<Order>> AreItemsModifiableSpec { get; }
        ISpecification<IProjection<Order>> CanBeDeletedSpec { get; }
        ISpecification<IProjection<Order>> HasSuchStatusSpec { get; }
        Order Get(Guid id);
        IEnumerable<Order> GetAll();
        Guid CreateCart(Order order);
        void SubmitNew(Guid id);
        void Delete(Guid id);
        void SubmitShipping(Guid id, DateTime shipmentDate);
        void Complete(Guid id);
    }
}
