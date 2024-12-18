using Core.Aggregates;
using Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.OrderUtils
{
    internal static class OrderStateFactory
    {
        public static IOrderState GetStateForOrder(Order order)
        {
            switch (order.Status.Value)
            {
                case OrderStatusEnum.Cart:
                    return new CartOrderState(order);
                default:
                    return new NotCartOrderState(order);
            }
        }
    }
}
