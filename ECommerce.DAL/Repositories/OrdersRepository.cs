using AutoMapper;
using AutoMapper.QueryableExtensions;
using ECommerce.Core.Aggregates;
using ECommerce.Core.Entities;
using ECommerce.Core.RepositoryInterfaces;
using ECommerce.Core.ValueObjects;
using ECommerce.Application.DTOs;
using ECommerce.DAL.Models;
using ECommerce.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Core.Interfaces;
using System.Linq.Expressions;

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

        public async Task<IDTO<Order>> GetDtoByIdAsync(Guid id, bool withItems = false)
        {
            var orderQuery = _dbContext.Orders.AsNoTracking();
            if (withItems)
            {
                orderQuery = orderQuery.Include(o => o.OrderItems);
            }

            var orderDto = await orderQuery
                .Where(o => o.Id == id && !o.IsDeleted)
                .ProjectTo<OrderWebDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            return orderDto;
        }

        public async Task<List<IDTO<Order>>> GetDtosAsync(bool withItems = false)
        {
            var ordersQuery = _dbContext.Orders.AsNoTracking();
            if (withItems)
            {
                ordersQuery = ordersQuery.Include(o => o.OrderItems);
            }

            var orderDtos = await ordersQuery
                .Where(orderEntity => !orderEntity.IsDeleted)
                .ProjectTo<OrderWebDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new List<IDTO<Order>>(orderDtos);
        }

        public async Task<List<IDTO<Order>>> GetDtosByStatusAsync(string starusStr, int page, int pageSize, bool withItems = false)
        {
            var ordersQuery = _dbContext.Orders.AsNoTracking();
            if (withItems)
            {
                ordersQuery = ordersQuery.Include(o => o.OrderItems);
            }

            var orderDtos = await ordersQuery
                .Where(orderEntity => orderEntity.Status == starusStr && !orderEntity.IsDeleted)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<OrderWebDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new List<IDTO<Order>>(orderDtos);
        }

        public async Task<IDTO<Order>> GetOwnDtoByIdAsync(Customer customer, Guid id, bool withItems = false)
        {
            var orderQuery = _dbContext.Orders.AsNoTracking();
            if (withItems)
            {
                orderQuery = orderQuery.Include(o => o.OrderItems);
            }

            var orderDto = await orderQuery
                .Where(o => o.Id == id && !o.IsDeleted && o.CustomerId == customer.Id)
                .ProjectTo<OrderWebDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            return orderDto;
        }

        public async Task<List<IDTO<Order>>> GetOwnDtosAsync(Customer customer, bool withItems = false)
        {
            var ordersQuery = _dbContext.Orders.AsNoTracking();
            if (withItems)
            {
                ordersQuery = ordersQuery.Include(o => o.OrderItems);
            }

            var orderDtos = await ordersQuery
                .Where(orderEntity => !orderEntity.IsDeleted && orderEntity.CustomerId == customer.Id)
                .ProjectTo<OrderWebDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new List<IDTO<Order>>(orderDtos);
        }

        public async Task<List<IDTO<Order>>> GetOwnDtosByStatusAsync(Customer customer, string starusStr, int page, int pageSize, bool withItems = false)
        {
            var ordersQuery = _dbContext.Orders.AsNoTracking();
            if (withItems)
            {
                ordersQuery = ordersQuery.Include(o => o.OrderItems);
            }

            var orderDtos = await ordersQuery
                .Where(orderEntity => orderEntity.Status == starusStr && !orderEntity.IsDeleted && orderEntity.CustomerId == customer.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<OrderWebDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new List<IDTO<Order>>(orderDtos);
        }

        public async Task<Guid> CreateAsync(Order order)
        {
            var orderNumber = await this.GetNewOrderNumberAsync();

            var orderDto = new OrderWebDTO(order);
            orderDto.OrderDate = DateTime.UtcNow;
            orderDto.OrderNumber = orderNumber;
            orderDto.Status = OrderStatusEnum.New.ToString();

            var orderEntry = await _dbContext.Orders.AddAsync(_mapper.Map<OrderEntity>(orderDto));
            return orderEntry.Entity.Id;
        }

        public async Task<Guid> ChangeStatusAsync(Guid id, OrderStatus orderStatus)
        {
            var orderEntity = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (orderEntity == null)
            {
                throw new NullReferenceException("Order not found");
            }
            orderEntity.SetStatusProps(orderStatus);
            return orderEntity.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            var orderEntity = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (orderEntity == null)
            {
                throw new NullReferenceException("Order not found");
            }
            orderEntity.IsDeleted = true;
        }

        private async Task<long> GetNewOrderNumberAsync()
        {
            var ordersCount = await _dbContext.Orders.AsNoTracking().CountAsync();
            return ordersCount + 1;
        }
    }
}
