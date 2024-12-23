using ECommerce.Core.Aggregates;
using ECommerce.Core.OtherInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DAL.Models
{
    public class CustomerEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Address { get; set; } = string.Empty;
        public decimal? Discount { get; set; } = decimal.Zero;
        public List<OrderEntity> Orders { get; set; } = new List<OrderEntity>();
        public Guid AccountId { get; set; }
        public AccountEntity? Account { get; set; }
    }
}
