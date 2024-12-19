using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ValueObjects
{
    public abstract class OrderStatus
    {
        public OrderStatusEnum Value { get; }

        protected static HashSet<OrderStatusEnum> _canBeChangedTo;

        protected OrderStatus(OrderStatusEnum value)
        {
            this.Value = value;
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
        Cart,
        New,
        Shipping,
        Shipped
    }

    public class CartOrderStatus : OrderStatus
    {
        static CartOrderStatus()
        {
            _canBeChangedTo = new HashSet<OrderStatusEnum>()
            {
                OrderStatusEnum.New,
            };
        }

        public CartOrderStatus()
            : base(OrderStatusEnum.Cart)
        {
        }

        public override OrderStatus Clone()
        {
            return new CartOrderStatus();
        }
    }

    public class NewOrderStatus : OrderStatus
    {
        public DateTime OrderDate { get; }
        public long OrderNumber { get; }

        static NewOrderStatus()
        {
            _canBeChangedTo = new HashSet<OrderStatusEnum>()
            {
                OrderStatusEnum.Shipping,
            };
        }

        public NewOrderStatus(DateTime orderDate, long orderNumber)
            : base(OrderStatusEnum.New)
        {
            this.OrderDate = orderDate;
            this.OrderNumber = orderNumber;
        }

        public override OrderStatus Clone()
        {
            return new NewOrderStatus(this.OrderDate, this.OrderNumber);
        }
    }

    public class ShippingOrderStatus : OrderStatus
    {
        public DateTime ShipmentDate { get; }

        static ShippingOrderStatus()
        {
            _canBeChangedTo = new HashSet<OrderStatusEnum>()
            {
                OrderStatusEnum.Shipped,
            };
        }

        public ShippingOrderStatus(DateTime shipmentDate)
            : base(OrderStatusEnum.Shipping)
        {
            this.ShipmentDate = shipmentDate;
        }

        public override OrderStatus Clone()
        {
            return new ShippingOrderStatus(this.ShipmentDate);
        }
    }

    public class ShippedOrderStatus : OrderStatus
    {
        static ShippedOrderStatus()
        {
            _canBeChangedTo = new HashSet<OrderStatusEnum>();
        }

        public ShippedOrderStatus()
            : base(OrderStatusEnum.Shipped)
        {
        }

        public override OrderStatus Clone()
        {
            return new ShippedOrderStatus();
        }
    }
}
