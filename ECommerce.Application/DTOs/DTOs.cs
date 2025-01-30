using ECommerce.Core.Aggregates;
using ECommerce.Core.Interfaces;
using ECommerce.Core.Entities;
using ECommerce.Core.ValueObjects;

namespace ECommerce.Application.DTOs
{
    public class AccountWebDTO : IDTO<Account>
    {
        public Guid Id { get; set; }
        public AccountRole Role { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public CustomerWebDTO Customer { get; set; }

        public Account GetOriginalObject(bool asNew = false)
        {
            var accId = asNew ? Guid.NewGuid() : this.Id;
            var customer = default(Customer);
            if (this.Customer != default)
            {
                this.Customer.AccountId = accId;
                customer = this.Customer.GetOriginalObject();
            }
            return Account.CreateOrFail(this.Username, this.Password, this.Role, customer, accId);
        }
    }

    public class CustomerWebDTO : IDTO<Customer>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Address { get; set; } = string.Empty;
        public decimal Discount { get; set; } = decimal.Zero;
        public Guid AccountId { get; set; }

        public Customer GetOriginalObject(bool asNew = false)
        {
            return Customer.CreateOrFail(this.Name, this.Code, this.Address, this.Discount, this.AccountId, asNew ? Guid.NewGuid() : this.Id);
        }
    }

    public class AccountSafeProjection : IAccountProjection
    {
        public Guid Id { get; set; }
        public AccountRole Role { get; set; }
        public string Username { get; set; }
        public CustomerWebDTO Customer { get; set; }

        public void SetDataFromOriginalObject(Account account)
        {
            this.Id = account.Id;
            this.Role = account.Role;
            this.Username = account.Username;
        }
    }

    public class ItemWebDTO : IDTO<Item>
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public ItemCategory Category { get; set; }

        public Item GetOriginalObject(bool asNew = false)
        {
            return Item.CreateOrFail(this.Code, this.Name, this.Price, this.Category, asNew ? Guid.NewGuid() : this.Id);
        }
    }

    public class OrderWebDTO : IDTO<Order>
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

        public Order GetOriginalObject(bool asNew = false)
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
            var orderId = asNew ? Guid.NewGuid() : this.Id;
            var items = this.OrderItems.Select(itemDto =>
            {
                itemDto.OrderId = orderId;
                return itemDto.GetOriginalObject();
            }).ToList();
            var result = new Order(this.CustomerId, status, items, orderId);
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

        public OrderItem GetOriginalObject(bool asNew = false)
        {
            return OrderItem.CreateOrFail(this.OrderId, this.ItemId, this.ItemsCount, this.ItemPrice, asNew ? Guid.NewGuid() : this.Id);
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
