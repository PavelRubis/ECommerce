using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ECommerce.Core.Aggregates
{
    public class Customer
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Code { get; }
        public string Address { get; }
        public decimal Discount { get; }

        private Customer(Guid id, string name, string code, string address, decimal discount)
        {
            this.Id = id != default ? id : Guid.NewGuid();
            this.Name = name;
            this.Code = code;
            this.Address = address;
            this.Discount = discount;
        }

        public static Customer CreateOrFail(string name, string code, string address, decimal discount, Guid accountId, Guid id = default)
        {
            if (!Customer.IsInputValid(name, code, discount, out var errors))
            {
                throw new AggregateException(errors);
            }
            return new Customer(id, name, code, address, discount);
        }

        private static Regex CodeRegex = new Regex(@"^\d{4}-[12][09]\d{2}$", RegexOptions.Compiled);
        private static bool IsInputValid(string name, string code, decimal discount, out List<Exception> errors)
        {
            errors = new List<Exception>(4);
            if (string.IsNullOrWhiteSpace(name))
            {
                errors.Add(new ArgumentException("Customer name can not be empty."));
            }
            if (!CodeRegex.IsMatch(code))
            {
                errors.Add(new ArgumentException("Invalid code format."));
            }
            if (discount < 0 || discount > 100)
            {
                errors.Add(new ArgumentException("Discount can not be less than 0 and more than 100."));
            }
            if (errors.Count > 0)
            {
                return false;
            }
            return true;
        }
    }
}
