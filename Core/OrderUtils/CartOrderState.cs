using Core.Aggregates;
using Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.OrderUtils
{
    internal class CartOrderState : IOrderState
    {
        private Order _order;

        public CartOrderState(Order order)
        {
            _order = order;
        }

        public void AddOrUpdateItem(OrderItem item)
        {
            _order.AddOrUpdateItemInternal(item);
        }

        public void RemoveItem(OrderItem item)
        {
            _order.RemoveItemInternal(item);
        }
    }
}
