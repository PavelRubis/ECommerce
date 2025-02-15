﻿using ECommerce.Core.Aggregates;
using ECommerce.Core.Entities;

namespace ECommerce.DAL.Models
{
    public class AccountEntity
    {
        public Guid Id { get; set; }
        public string Role { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public CustomerEntity? Customer { get; set; }
        public bool IsDeleted { get; set; }
    }
}
