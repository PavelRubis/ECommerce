﻿using ECommerce.Core.RepositoryInterfaces;
using ECommerce.Core.ServiceInterfaces;
using ECommerce.Core.ValueObjects;

namespace ECommerce.Application.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IOrdersRepository _repo;

        public OrdersService(IOrdersRepository repository)
        {
            _repo = repository;
        }

        public async Task SubmitShippingAsync(Guid id, DateTime shipmentDate)
        {
            // TODO: optimize
            var orderDto = await _repo.GetDtoByIdAsync(id);
            var order = orderDto.GetOriginalObject();
            var newStatus = new ShippingOrderStatus(shipmentDate);
            order.ChangeStatusOrFail(newStatus);
            await _repo.ChangeStatusAsync(order.Id, newStatus);
        }

        public async Task CompleteAsync(Guid id)
        {
            // TODO: optimize
            var orderDto = await _repo.GetDtoByIdAsync(id);
            var order = orderDto.GetOriginalObject();
            var newStatus = new ShippedOrderStatus();
            order.ChangeStatusOrFail(newStatus);
            await _repo.ChangeStatusAsync(order.Id, newStatus);
        }

        public async Task DeleteOwnAsync(Guid id)
        {
            var orderDto = await _repo.GetDtoByIdAsync(id);
            var order = orderDto.GetOriginalObject();
            if (order.CanBeDeleted)
            {
                await _repo.DeleteAsync(id);
                return;
            }
            throw new InvalidOperationException("Order with such status can not be deleted.");
        }
    }
}
