using AutoMapper;
using ECommerce.Application.DTOs;
using ECommerce.Core.Aggregates;
using ECommerce.Core.Entities;
using ECommerce.DAL.Models;

namespace ECommerce.DAL
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerEntity>();
            CreateMap<Customer, CustomerWebDTO>();
            CreateMap<CustomerWebDTO, Customer>();

            CreateMap<CustomerEntity, CustomerWebDTO>();
            CreateMap<CustomerWebDTO, CustomerEntity>();

            CreateMap<AccountEntity, AccountSafeProjection>().ForMember(m => m.Role, opt => opt.MapFrom(entity => Enum.Parse<AccountRole>(entity.Role)));
            CreateMap<AccountSafeProjection, AccountEntity>();

            CreateMap<AccountEntity, AccountWebDTO>().ForMember(m => m.Role, opt => opt.MapFrom(entity => Enum.Parse<AccountRole>(entity.Role)));
            CreateMap<AccountWebDTO, AccountEntity>();

            CreateMap<Account, AccountEntity>();


            CreateMap<Item, ItemEntity>();
            CreateMap<ItemEntity, ItemWebDTO>().ForMember(m => m.Category, opt => opt.MapFrom(entity => Enum.Parse<ItemCategory>(entity.Category))); ;
            CreateMap<Item, ItemWebDTO>();
            CreateMap<ItemWebDTO, Item>();

            CreateMap<OrderWebDTO, OrderEntity>();
            CreateMap<OrderEntity, OrderWebDTO>();

            CreateMap<OrderItemWebDTO, OrderItemEntity>();
            CreateMap<OrderItemEntity, OrderItemWebDTO>();
        }
    }

}
