using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ECommerce.Core.Aggregates;
using ECommerce.Application.DTOs;
using ECommerce.DAL.Models;
using ECommerce.Application.Enums;

namespace ECommerce.DAL
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerEntity>();
            CreateMap<CustomerEntity, CustomerOutWebDTO>();
            CreateMap<Customer, CustomerOutWebDTO>();
            CreateMap<CustomerOutWebDTO, Customer>();
            CreateMap<AccountOutWebDTO, AccountEntity>();
            CreateMap<AccountEntity, AccountOutWebDTO>().ForMember(m => m.Role, opt => opt.MapFrom(entity => Enum.Parse<AppRole>(entity.Role)));

            CreateMap<Customer, CustomerEntity>();
            CreateMap<CustomerInWebDTO, CustomerEntity>();
            CreateMap<CustomerEntity, CustomerInWebDTO>();
            CreateMap<Customer, CustomerInWebDTO>();
            CreateMap<CustomerInWebDTO, Customer>();
            CreateMap<AccountInWebDTO, AccountEntity>();
            CreateMap<AccountEntity, AccountInWebDTO>().ForMember(m => m.Role, opt => opt.MapFrom(entity => Enum.Parse<AppRole>(entity.Role)));


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
