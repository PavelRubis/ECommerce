using ECommerce.Core.Aggregates;
using ECommerce.Core.Entities;
using ECommerce.Core.Utils;
using ECommerce.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DAL.Models
{
    public class OrderEntity : IDTO<Order>, IOrderSpecDTO
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public CustomerEntity? Customer { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ShipmentDate { get; set; }
        public long OrderNumber { get; set; }
        public string Status { get; set; }
        public List<OrderItemEntity> OrderItems { get; set; } = new List<OrderItemEntity>();

        public Order GetOriginalObject()
        {
            var status = default(OrderStatus);
            switch (Enum.Parse<OrderStatusEnum>(this.Status))
            {
                case OrderStatusEnum.New:
                    status = new NewOrderStatus(this.OrderDate, this.OrderNumber);
                    break;
                case OrderStatusEnum.Shipping:
                    status = new ShippingOrderStatus(this.ShipmentDate);
                    break;
                case OrderStatusEnum.Shipped:
                    status = new ShippedOrderStatus();
                    break;
            }
            var items = this.OrderItems.Select(item => item.GetOriginalObject()).ToList();
            return new Order(this.CustomerId, status, items, this.Id);
        }

        public void SetDataFromObject(Order order)
        {
            this.Id = order.Id;
            this.CustomerId = order.CustomerId;
            switch (order.Status.Value)
            {
                case OrderStatusEnum.New:
                    {
                        var typedStatus = order.Status as NewOrderStatus;
                        this.OrderDate = typedStatus.OrderDate;
                        this.OrderNumber = typedStatus.OrderNumber;
                    }
                    break;
                case OrderStatusEnum.Shipping:
                    {
                        var typedStatus = order.Status as ShippingOrderStatus;
                        this.ShipmentDate = typedStatus.ShipmentDate;
                    }
                    break;
                case OrderStatusEnum.Shipped:
                    break;
            }
            this.Status = order.Status.Value.ToString();
            this.OrderItems = new List<OrderItemEntity>(order.Items.Select(item =>
            {
                var itemEntity = new OrderItemEntity();
                itemEntity.SetDataFromObject(item);
                return itemEntity;
            }));
        }
    }
}
