using ECommerce.Core.Aggregates;
using ECommerce.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DAL.Models
{
    public class ItemEntity: IDTO<Item>
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public List<OrderItemEntity> OrderItems { get; set; } = new List<OrderItemEntity>();

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
