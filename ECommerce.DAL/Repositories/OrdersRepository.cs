using AutoMapper;
using AutoMapper.QueryableExtensions;
using ECommerce.Core.Aggregates;
using ECommerce.Core.Entities;
using ECommerce.Core.RepositoryInterfaces;
using ECommerce.Core.ValueObjects;
using ECommerce.DAL.DTOs;
using ECommerce.DAL.Models;
using ECommerce.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Core.OtherInterfaces;

namespace ECommerce.DAL.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly ECommerceDbContext _dbContext;
        private readonly IMapper _mapper;

        public OrdersRepository(ECommerceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IOrderDTO> GetDtoByIdAsync(Guid id, bool withItems = false)
        {
            var orderQuery = _dbContext.Orders.AsNoTracking();
            if (withItems)
            {
                orderQuery = orderQuery.Include(o => o.OrderItems);
            }

            var orderDto = await orderQuery
                .ProjectTo<OrderWebDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(o => o.Id == id);
            if (orderDto == null)
            {
                throw new NullReferenceException("Order not found");
            }
            return orderDto;
        }

        public async Task<List<IOrderDTO>> GetDtosAsync(int page, int pageSize, bool withItems = false)
        {
            var ordersQuery = _dbContext.Orders
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            if (withItems)
            {
                ordersQuery = ordersQuery.Include(o => o.OrderItems);
            }

            var orderDtos = await ordersQuery
                .ProjectTo<OrderWebDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new List<IOrderDTO>(orderDtos);
        }

        public async Task<List<IOrderDTO>> GetDtosBySpecificationAsync(IOrderSpecification spec, int page, int pageSize, bool withItems = false)
        {
            var ordersQuery = _dbContext.Orders
                .AsNoTracking()
                .ProjectTo<OrderWebDTO>(_mapper.ConfigurationProvider)
                .Where(spec.ToExpression())
                .ProjectTo<OrderWebDTO>(_mapper.ConfigurationProvider);
            
            if (withItems)
            {
                ordersQuery = ordersQuery.Include(o => o.OrderItems);
            }

            var orderDtos = await ordersQuery
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new List<IOrderDTO>(orderDtos);
        }

        public async Task<Guid> CreateAsync(Order order)
        {
            var orderDto = new OrderWebDTO();
            orderDto.SetDataFromObject(order);
            var orderEntry = await _dbContext.AddAsync(_mapper.Map<OrderEntity>(orderDto));
            return orderEntry.Entity.Id;
        }

        public Guid Edit(Order order)
        {
            var orderDto = new OrderWebDTO();
            orderDto.SetDataFromObject(order);
            var orderEntry = _dbContext.Update(_mapper.Map<OrderEntity>(orderDto));
            return orderEntry.Entity.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            var orderEntity = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (orderEntity == null)
            {
                throw new NullReferenceException("Order not found");
            }
            _dbContext.Orders.Remove(orderEntity);
        }
    }
}
