using Core.Aggregates;
using Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.OrderUtils
{
    internal interface IOrderState
    {
        void AddOrUpdateItem(OrderItem item);
        void RemoveItem(OrderItem item);
    }
}
