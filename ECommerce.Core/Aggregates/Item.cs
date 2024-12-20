using Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.Aggregates
{
    public class Item
    {
        public Guid Id { get; }
        public string Code { get; }
        public string Name { get; }
        public decimal Price { get; }
        public ItemCategory Category { get; }

        private Item(Guid id, string code, string name, decimal price, ItemCategory category)
        {
            this.Id = id != default ? id : Guid.NewGuid();
            this.Code = code;
            this.Name = name;
            this.Price = price;
            this.Category = category;
        }

        public static Item CreateOrFail(string code, string name, decimal price, ItemCategory category, Guid id = default)
        {
            if (!Item.IsInputValid(code, name, price, out var errors))
            {
                throw new AggregateException(errors);
            }
            return new Item(id, code, name, price, category);
        }

        private static Regex CodeRegex = new Regex(@"^\d{2}-\d{4}-YY\d{2}$", RegexOptions.Compiled);
        private static bool IsInputValid(string code, string name, decimal price, out List<Exception> errors)
        {
            errors = new List<Exception>(3);
            if (!CodeRegex.IsMatch(code))
            {
                errors.Add(new ArgumentException("Invalid code format."));
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                errors.Add(new ArgumentException("Item name can not be empty."));
            }
            if (price < 0)
            {
                errors.Add(new ArgumentException("Price can not be less than 0."));
            }
            if (errors.Count > 0)
            {
                return false;
            }
            return true;
        }
    }

    public enum ItemCategory
    {
        Undefined,
        Shirt,
        TShirt,
        Sweater,
        Jacket,
        Jeans,
        Shorts,
        Dress,
        Scarf,
        Hat,
        Shoes
    }
}
