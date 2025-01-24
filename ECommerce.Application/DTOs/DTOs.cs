using ECommerce.Core.Aggregates;
using ECommerce.Core.Entities;
using ECommerce.Core.DTOsInterfaces;
using ECommerce.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Application.Enums;

namespace ECommerce.Application.DTOs
{
    public class CustomerOutWebDTO: IDTO<Customer>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Address { get; set; } = string.Empty;
        public decimal Discount { get; set; } = decimal.Zero;
    }

    public class AccountOutWebDTO
    {
        public Guid Id { get; set; }
        public AppRole Role { get; set; }
        public string Username { get; set; }
        public CustomerInWebDTO Customer { get; set; }
    }

    public class AccountInWebDTO
    {
        public Guid Id { get; set; }
        public AppRole Role { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public CustomerInWebDTO Customer { get; set; }
    }

    public class CustomerInWebDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Address { get; set; } = string.Empty;
        public decimal Discount { get; set; } = decimal.Zero;
        public Guid AccountId { get; set; }
    }

    public class ItemWebDTO : IDTO<Item>
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
    }

    public class OrderWebDTO : IOrderDTO
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ShipmentDate { get; set; }
        public long OrderNumber { get; set; }
        public string Status { get; set; }
        public List<OrderItemWebDTO> OrderItems { get; set; } = new List<OrderItemWebDTO>();

        public OrderWebDTO() { }

        public OrderWebDTO(Order order)
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
            this.OrderItems = new List<OrderItemWebDTO>(order.Items.Select(item =>
            {
                var itemEntity = new OrderItemWebDTO();
                itemEntity.SetDataFromObject(item);
                return itemEntity;
            }));
        }

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
            var result = new Order(this.CustomerId, status, items, this.Id);
            result.Items.ForEach(item => item.OrderId = result.Id);
            return result;
        }
    }

    public class OrderItemWebDTO : IDTO<OrderItem>
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ItemId { get; set; }
        public int ItemsCount { get; set; }
        public decimal ItemPrice { get; set; }

        public OrderItem GetOriginalObject()
        {
            return OrderItem.CreateOrFail(this.OrderId, this.ItemId, this.ItemsCount, this.ItemPrice);
        }

        public void SetDataFromObject(OrderItem obj)
        {
            this.Id = obj.Id;
            this.OrderId = obj.OrderId;
            this.ItemId = obj.ItemId;
            this.ItemsCount = obj.ItemsCount;
            this.ItemPrice = obj.ItemPrice;
        }
    }
}
