using ECommerce.Core.Entities;

namespace ECommerce.Core.Aggregates
{
    public class Account
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public AccountRole Role { get; set; }
        public Customer Customer { get; set; }

        private Account(Guid id, string username, string password, AccountRole Role, Customer customer)
        {
            Id = id != default ? id : Guid.NewGuid();
            Username = username;
            Password = password;
            this.Role = Role;
            Customer = customer;
        }

        public static Account CreateOrFail(string username, string password, AccountRole role, Customer customer, Guid id = default)
        {
            if (!IsInputValid(username, password, role, customer, out var errors))
            {
                throw new AggregateException(errors);
            }
            return new Account(id, username, password, role, customer);
        }

        private static bool IsInputValid(string username, string password, AccountRole role, Customer customer, out List<Exception> errors)
        {
            errors = new List<Exception>(4);
            if (string.IsNullOrWhiteSpace(username))
            {
                errors.Add(new ArgumentException("Account username can not be empty."));
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                errors.Add(new ArgumentException("Invalid account password."));
            }
            if (errors.Count > 0)
            {
                return false;
            }
            return true;
        }
    }

    public enum AccountRole
    {
        Undefined,
        Customer,
        Manager
    }
}
