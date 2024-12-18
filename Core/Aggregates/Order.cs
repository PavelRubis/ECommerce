using Core.ValueObjects;
using Core.OrderUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aggregates
{
    public class Order: IOrderState
    {
        public Guid Id { get; }
        public Guid CustomerId { get; }
        public int OrderNumber { get; }
        public OrderStatus Status { get; private set; }

        private Dictionary<Guid, OrderItem> _itemsByProductIds = new Dictionary<Guid, OrderItem>();

        public Order(OrderItem item, Guid customerId, Guid id = default)
        {
            this.Id = id != default ? id : Guid.NewGuid();
            this.CustomerId = customerId;
            this.Status = new CartOrderStatus();
            _itemsByProductIds.Add(item.ItemId, item);
        }

        public void ChangeStatusOrFail(OrderStatus newStatus)
        {
            if (!this.Status.CanBeChangedTo(newStatus.Value))
            {
                throw new ArgumentException("Invalid order status change.");
            }
            this.Status = newStatus.Clone();
        }

        public void AddOrUpdateItem(OrderItem item)
        {
            OrderStateFactory.GetStateForOrder(this).AddOrUpdateItem(item);
        }

        public void RemoveItem(OrderItem item)
        {
            OrderStateFactory.GetStateForOrder(this).RemoveItem(item);
        }

        internal void AddOrUpdateItemInternal(OrderItem item)
        {
            if (!_itemsByProductIds.TryAdd(item.ItemId, item.Clone()))
            {
                _itemsByProductIds[item.ItemId] = item.Clone();
            }
        }

        internal void RemoveItemInternal(OrderItem item)
        {
            _itemsByProductIds.Remove(item.ItemId);
        }
    }
}
