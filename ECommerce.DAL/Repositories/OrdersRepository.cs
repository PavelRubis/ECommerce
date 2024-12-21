using ECommerce.Core.Aggregates;
using ECommerce.Core.Entities;
using ECommerce.Core.RepositoryInterfaces;
using ECommerce.Core.Utils;
using ECommerce.Core.ValueObjects;
using ECommerce.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DAL.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly ECommerceDbContext _dbContext;

        public OrdersRepository(ECommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<IDTO<Order>>> GetDtosAsync(int page, int pageSize, bool withItems = false)
        {
            var ordersQuery = _dbContext.Orders
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            if (withItems)
            {
                ordersQuery = ordersQuery.Include(o => o.OrderItems);
            }

            var orderEntities = await ordersQuery.ToListAsync();

            return new List<IDTO<Order>>(orderEntities);
        }

        public async Task<List<IDTO<Order>>> GetDtosBySpecificationAsync(IOrderSpecification spec, int page, int pageSize, bool withItems = false)
        {
            var ordersQuery = _dbContext.Orders.AsNoTracking();
            if (withItems)
            {
                ordersQuery = ordersQuery.Include(o => o.OrderItems);
            }
            ordersQuery.Where(spec.ToExpression()).Skip((page - 1) * pageSize).Take(pageSize);

            var orderEntities = await ordersQuery.ToListAsync();
            return new List<IDTO<Order>>(orderEntities);
        }

        public async Task<IDTO<Order>> GetDtoByIdAsync(Guid id, bool withItems = false)
        {
            var orderQuery = _dbContext.Orders.AsNoTracking();
            if (withItems)
            {
                orderQuery = orderQuery.Include(o => o.OrderItems);
            }

            var orderEntity = await orderQuery.FirstOrDefaultAsync(o => o.Id == id);
            if (orderEntity == null)
            {
                throw new NullReferenceException("Order not found");
            }
            return orderEntity;
        }

        public async Task<Guid> CreateAsync(Order order)
        {
            var orderEntity = new OrderEntity();
            orderEntity.SetDataFromObject(order);
            var orderEntry = await _dbContext.AddAsync(orderEntity);
            return orderEntry.Entity.Id;
        }

        public Guid Edit(Order order)
        {
            var orderEntity = new OrderEntity();
            orderEntity.SetDataFromObject(order);
            var orderEntry = _dbContext.Update(orderEntity);
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
