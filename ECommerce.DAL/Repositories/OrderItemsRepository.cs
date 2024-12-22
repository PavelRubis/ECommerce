using AutoMapper;
using ECommerce.Core.Entities;
using ECommerce.DAL.DTOs;
using ECommerce.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DAL.Repositories
{
    public class OrderItemsRepository
    {
        private readonly ECommerceDbContext _dbContext;
        private readonly IMapper _mapper;
        public OrderItemsRepository(ECommerceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task CreateAsyncFromOrder(OrderWebDTO order)
        {
            var items = order.OrderItems;
            await _dbContext.OrderItems.AddRangeAsync(items.Select(item => _mapper.Map<OrderItemEntity>(item)));
            return;
        }
    }
}
