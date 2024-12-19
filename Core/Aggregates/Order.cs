using Core.Entities;
using Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aggregates
{
    public class Order
    {
        public Guid Id { get; }
        public Guid CustomerId { get; }
        public OrderStatus Status { get; private set; }

        public Order(Guid customerId, Guid id = default, OrderStatus status = default)
        {
            this.Id = id != default ? id : Guid.NewGuid();
            this.CustomerId = customerId;
            this.Status = status != default ? status.Clone() : new CartOrderStatus();
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
}
