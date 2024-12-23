using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DAL.Models
{
    public class AccountEntity
    {
        public Guid Id { get; set; }
        public string Role { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public CustomerEntity? Customer { get; set; }
    }
}
