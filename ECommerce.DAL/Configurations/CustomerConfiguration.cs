﻿using ECommerce.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DAL.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<CustomerEntity>
    {
        public void Configure(EntityTypeBuilder<CustomerEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Code).IsRequired();
            builder.Property(x => x.Address).IsRequired();
            builder.Property(x => x.IsDeleted).IsRequired();
            builder
                .HasOne(c => c.Account)
                .WithOne(c => c.Customer)
                .HasForeignKey<CustomerEntity>(c => c.AccountId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
