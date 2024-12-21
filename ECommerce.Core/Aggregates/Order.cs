using ECommerce.Core.Entities;
using ECommerce.Core.Utils;
using ECommerce.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Aggregates
{
    public class Order
    {
        public Guid Id { get; }
        public Guid CustomerId { get; }
        public OrderStatus Status { get; private set; }
        public List<OrderItem> Items { get; private set; }

        public Order(Guid customerId, OrderStatus status, List<OrderItem> items, Guid id = default)
        {
            this.Id = id != default ? id : Guid.NewGuid();
            this.CustomerId = customerId;
            this.Status = status.Clone();
            this.Items = new List<OrderItem>(items);
        }

        public void ChangeStatusOrFail(OrderStatus newStatus)
        {
            if (!this.Status.CanBeChangedTo(newStatus.Value))
            {
                throw new ArgumentException("Invalid order status change.");
            }
            this.Status = newStatus.Clone();
        }
    }
    public interface IOrderSpecification
    {
        Expression<Func<IOrderSpecDTO, bool>> ToExpression();
    }
}
