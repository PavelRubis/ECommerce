using Core.Aggregates;
using Core.Entities;
using Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DAL.Models
{
    public class OrderItemEntity : IDTO<OrderItem>
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public OrderEntity? Order { get; set; }
        public Guid ItemId { get; set; }
        public ItemEntity? Item { get; set; }
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
