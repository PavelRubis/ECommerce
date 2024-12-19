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
        Order GetById(Guid id);
        IEnumerable<Order> GetAll();
        IEnumerable<Order> GetBySpecification(ISpecification<IProjection<Order>> spec);
        Guid Create(Order order);
        void ChangeStatus(Guid id, OrderStatus newStatus);
        void SetOrUpdateItem(OrderItem item);
        void RemoveItem(OrderItem item);
        void Delete(Guid id);
    }
}
