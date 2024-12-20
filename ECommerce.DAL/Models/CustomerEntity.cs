using Core.Aggregates;
using Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DAL.Models
{
    public class CustomerEntity: IDTO<Customer>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Address { get; set; } = string.Empty;
        public decimal? Discount { get; set; } = decimal.Zero;
        public List<OrderEntity> Orders { get; set; } = new List<OrderEntity>();
        public AccountEntity? Account { get; set; }

        public Customer GetOriginalObject()
        {
            return Customer.CreateOrFail(this.Name, this.Code, this.Address, this.Discount ?? 0, this.Id);
        }

        public void SetDataFromObject(Customer obj)
        {
            Id = obj.Id;
            Name = obj.Name;
            Code = obj.Code;
            Address = obj.Address;
            Discount = obj.Discount;
        }
    }
}
