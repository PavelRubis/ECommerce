using ECommerce.Core.Aggregates;
using ECommerce.Core.Entities;
using ECommerce.Core.OtherInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DAL.Models
{
    public class OrderItemEntity
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public OrderEntity? Order { get; set; }
        public Guid ItemId { get; set; }
        public ItemEntity? Item { get; set; }
        public int ItemsCount { get; set; }
        public decimal ItemPrice { get; set; }
    }
}
