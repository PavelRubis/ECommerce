using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.ValueObjects
{
    public abstract class OrderStatus
    {
        public OrderStatusEnum Value { get; }

        protected HashSet<OrderStatusEnum> _canBeChangedTo;

        protected OrderStatus(OrderStatusEnum value, HashSet<OrderStatusEnum> canBeChangedTo)
        {
            this.Value = value;
            _canBeChangedTo = canBeChangedTo;
        }

        public bool CanBeChangedTo(OrderStatusEnum newValue)
        {
            return _canBeChangedTo.Contains(newValue);
        }

        public abstract OrderStatus Clone();

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is not OrderStatus other)
                return false;

            return this.Value == other.Value;
        }
    }

    public enum OrderStatusEnum
    {
        New,
        Shipping,
        Shipped
    }

    public class NewOrderStatus : OrderStatus
    {
        public DateTime OrderDate { get; }
        public long OrderNumber { get; }

        public NewOrderStatus(DateTime orderDate, long orderNumber)
            : base(OrderStatusEnum.New, new HashSet<OrderStatusEnum>() { OrderStatusEnum.Shipping })
        {
            this.OrderDate = orderDate;
            this.OrderNumber = orderNumber;
        }

        public override OrderStatus Clone()
        {
            return new NewOrderStatus(this.OrderDate, this.OrderNumber);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.OrderDate, this.OrderNumber, this.Value);
        }

        public override bool Equals(object obj)
        {
            if (obj is not NewOrderStatus other)
                return false;

            return this.OrderDate == other.OrderDate &&
                   this.OrderNumber == other.OrderNumber &&
                   this.Value == other.Value;
        }
    }

    public class ShippingOrderStatus : OrderStatus
    {
        public DateTime ShipmentDate { get; }

        public ShippingOrderStatus(DateTime shipmentDate)
            : base(OrderStatusEnum.Shipping, new HashSet<OrderStatusEnum>() { OrderStatusEnum.Shipped })
        {
            this.ShipmentDate = shipmentDate;
        }

        public override OrderStatus Clone()
        {
            return new ShippingOrderStatus(this.ShipmentDate);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.ShipmentDate, this.Value);
        }

        public override bool Equals(object obj)
        {
            if (obj is not ShippingOrderStatus other)
                return false;

            return this.ShipmentDate == other.ShipmentDate &&
                   this.Value == other.Value;
        }
    }

    public class ShippedOrderStatus : OrderStatus
    {
        public ShippedOrderStatus()
            : base(OrderStatusEnum.Shipped, new HashSet<OrderStatusEnum>())
        {
        }

        public override OrderStatus Clone()
        {
            return new ShippedOrderStatus();
        }
    }
}
