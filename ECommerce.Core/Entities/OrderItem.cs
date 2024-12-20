using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ItemId { get; }
        public int ItemsCount { get; }
        public decimal ItemPrice { get; }

        private OrderItem(Guid id, Guid orderId, Guid itemId, int itemsCount, decimal itemPrice)
        {
            Id = id != default ? id : Guid.NewGuid();
            OrderId = orderId;
            ItemId = itemId;
            ItemsCount = itemsCount;
            ItemPrice = itemPrice;
        }

        public static OrderItem CreateOrFail(Guid orderId, Guid itemId, int itemsCount, decimal itemsPrice, Guid id = default)
        {
            var errors = new List<Exception>(3);
            if (itemsCount < 1)
            {
                errors.Add(new ArgumentException("Quantity can not be less than 1."));
            }
            if (itemsPrice < 0)
            {
                errors.Add(new ArgumentException("Price can not be less than 0."));
            }
            if (errors.Count > 0)
            {
                throw new AggregateException(errors);
            }

            return new OrderItem(id, orderId, itemId, itemsCount, itemsPrice);
        }
    }
}
