using ECommerce.Core.Aggregates;
using ECommerce.Core.Utils;
using ECommerce.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DAL.DTOs
{
    public class CustomerWebDTO: IDTO<Customer>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Address { get; set; } = string.Empty;
        public decimal Discount { get; set; } = decimal.Zero;
        public AccountWebDTO Account { get; set; }

        public Customer GetOriginalObject()
        {
            return Customer.CreateOrFail(this.Name, this.Code, this.Address, this.Discount, this.Id);
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

    public class AccountWebDTO
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Guid CustomerId { get; set; }
    }


    public class ItemWebDTO : IDTO<Item>
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }

        public Item GetOriginalObject()
        {
            return Item.CreateOrFail(this.Code, this.Name, this.Price, Enum.Parse<ItemCategory>(Category), this.Id);
        }

        public void SetDataFromObject(Item obj)
        {
            this.Id = obj.Id;
            this.Code = obj.Code;
            this.Name = obj.Name;
            this.Price = obj.Price;
            this.Category = obj.Category.ToString();
        }
    }
}
