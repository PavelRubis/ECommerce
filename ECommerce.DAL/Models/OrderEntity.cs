using ECommerce.Core.Aggregates;
using ECommerce.Core.Entities;
using ECommerce.Core.Interfaces;
using ECommerce.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DAL.Models
{
    public class OrderEntity
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public CustomerEntity? Customer { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ShipmentDate { get; set; }
        public long OrderNumber { get; set; }
        public string Status { get; set; }
        public List<OrderItemEntity> OrderItems { get; set; } = new List<OrderItemEntity>();
        public bool IsDeleted { get; set; }

        public void SetStatusProps(OrderStatus orderStatus)
        {
            switch (orderStatus.Value)
            {
                case OrderStatusEnum.New:
                    {
                        var typedStatus = orderStatus as NewOrderStatus;
                        this.OrderDate = typedStatus.OrderDate;
                        this.OrderNumber = typedStatus.OrderNumber;
                    }
                    break;
                case OrderStatusEnum.Shipping:
                    {
                        var typedStatus = orderStatus as ShippingOrderStatus;
                        this.ShipmentDate = typedStatus.ShipmentDate;
                    }
                    break;
                case OrderStatusEnum.Shipped:
                    break;
            }
            this.Status = orderStatus.Value.ToString();
        }
    }
}
