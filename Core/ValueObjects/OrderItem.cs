using Core.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ValueObjects
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ItemId { get; }
        public int ItemsCount { get; }
        public decimal ItemsPrice { get; }

        private OrderItem(Guid id, Guid orderId, Guid itemId, int itemsCount, decimal itemsPrice)
        {
            this.Id = id != default ? id : Guid.NewGuid();
            this.OrderId = orderId;
            this.ItemId = itemId;
            this.ItemsCount = itemsCount;
            this.ItemsPrice = itemsPrice;
        }

        public static OrderItem CreateOrFail(Guid orderId, Guid itemId, int itemsCount, decimal itemsPrice, Guid id = default)
        {
            if (itemsCount < 1)
            {
                throw new ArgumentException("Quantity can not be less than 1.");
            }
            if (itemsPrice < 0)
            {
                throw new ArgumentException("Price can not be less than 0.");
            }

            return new OrderItem(id, orderId, itemId, itemsCount, itemsPrice);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.ItemId, this.ItemsCount, this.ItemsPrice);
        }

        public override bool Equals(object obj)
        {
            if (obj is not OrderItem other)
                return false;

            return this.ItemId == other.ItemId &&
                   this.ItemsCount == other.ItemsCount &&
                   this.ItemsPrice == other.ItemsPrice;
        }

        public OrderItem Clone()
        {
            return new OrderItem(this.Id, this.OrderId, this.ItemId, this.ItemsCount, this.ItemsPrice);
        }
    }
}
