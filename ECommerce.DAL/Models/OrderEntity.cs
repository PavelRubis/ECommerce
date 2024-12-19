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
    }
}
