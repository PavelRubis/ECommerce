using Core.Aggregates;
using Core.Entities;
using Core.RepositoryInterfaces;
using Core.Utils;
using Core.ValueObjects;
using ECommerce.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DAL.Repositories
{
    public class OrdersRepository: IOrdersRepository
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

        public async Task<List<IDTO<Order>>> GetDtosBySpecificationAsync(ISpecification<IDTO<Order>> spec, int page, int pageSize, bool withItems = false)
        {
            var ordersQuery = _dbContext.Orders
                .AsNoTracking()
                .Where(spec.ToExpression())
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            if (withItems)
            {
                ordersQuery = ordersQuery.Include(o => (o as OrderEntity).OrderItems);
            }

            var orderEntities = await ordersQuery.ToListAsync();
            return orderEntities;
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
                throw new NullReferenceException("order");
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

        public async Task ChangeStatusAsync(Guid id, OrderStatus newStatus)
        {
            var orderEntity = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (orderEntity == null)
            {
                throw new NullReferenceException("order");
            }
            var order = orderEntity.GetOriginalObject();
            order.ChangeStatusOrFail(newStatus);
            orderEntity.SetDataFromObject(order);
        }

        public async Task DeleteAsync(Guid id)
        {
            var orderEntity = await _dbContext.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (orderEntity != null)
            {
                _dbContext.Orders.Remove(orderEntity);
            }
        }
    }
}
