using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ECommerce.Core.Aggregates;
using ECommerce.Core.OtherInterfaces;
using ECommerce.DAL.Models;

namespace ECommerce.DAL.DTOs
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerEntity>();
            CreateMap<CustomerEntity, CustomerOutWebDTO>();
            CreateMap<Customer, CustomerOutWebDTO>();
            CreateMap<CustomerOutWebDTO, Customer>();
            CreateMap<AccountEntity, AccountOutWebDTO>();
            CreateMap<AccountOutWebDTO, AccountEntity>();

            CreateMap<Customer, CustomerEntity>();
            CreateMap<CustomerInWebDTO, CustomerEntity>();
            CreateMap<CustomerEntity, CustomerInWebDTO>();
            CreateMap<Customer, CustomerInWebDTO>();
            CreateMap<CustomerInWebDTO, Customer>();
            CreateMap<AccountEntity, AccountInWebDTO>();
            CreateMap<AccountInWebDTO, AccountEntity>();


            CreateMap<Item, ItemEntity>();
            CreateMap<ItemEntity, ItemWebDTO>();
            CreateMap<Item, ItemWebDTO>();
            CreateMap<ItemWebDTO, Item>();

            CreateMap<OrderWebDTO, OrderEntity>();
            CreateMap<OrderEntity, OrderWebDTO>();

            CreateMap<OrderItemWebDTO, OrderItemEntity>();
            CreateMap<OrderItemEntity, OrderItemWebDTO>();
        }
    }

}
