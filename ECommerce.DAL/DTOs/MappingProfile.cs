using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ECommerce.Core.Aggregates;
using ECommerce.Core.Utils;
using ECommerce.DAL.Models;

namespace ECommerce.DAL.DTOs
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerEntity>();
            CreateMap<CustomerEntity, CustomerWebDTO>();
            CreateMap<Customer, CustomerWebDTO>();
            CreateMap<CustomerWebDTO, Customer>();

            CreateMap<AccountEntity, AccountWebDTO>();
            CreateMap<AccountWebDTO, AccountEntity>();

            CreateMap<Item, ItemEntity>();
            CreateMap<ItemEntity, ItemWebDTO>();
            CreateMap<Item, ItemWebDTO>();
            CreateMap<ItemWebDTO, Item>();
        }
    }

}
