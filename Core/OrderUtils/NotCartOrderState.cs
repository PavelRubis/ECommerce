using Core.Aggregates;
using Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.OrderUtils
{
    internal class NotCartOrderState : IOrderState
    {
        private Order _order;

        public NotCartOrderState(Order order)
        {
            _order = order;
        }

        public void AddOrUpdateItem(OrderItem item)
        {
            throw new InvalidOperationException("New items can not be added to order due to order curent state.");
        }

        public void RemoveItem(OrderItem item)
        {
            throw new InvalidOperationException("New items can not be removed frm order due to order curent state.");
        }
    }
}
