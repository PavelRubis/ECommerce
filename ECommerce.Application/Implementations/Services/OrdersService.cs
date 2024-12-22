using ECommerce.Core.Aggregates;
using ECommerce.Core.RepositoryInterfaces;
using ECommerce.Core.ServiceInterfaces;
using ECommerce.Core.OtherInterfaces;
using ECommerce.Core.ValueObjects;
using ECommerce.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.Implementations.Services
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
            var orderDto = await _repo.GetDtoByIdAsync(id);
            var order = orderDto.GetOriginalObject();
            var newStatus = new ShippingOrderStatus(shipmentDate);
            order.ChangeStatusOrFail(newStatus);
            await _repo.ChangeStatusAsync(order.Id, newStatus);
        }

        public async Task CompleteAsync(Guid id)
        {
            var orderDto = await _repo.GetDtoByIdAsync(id);
            var order = orderDto.GetOriginalObject();
            var newStatus = new ShippedOrderStatus();
            order.ChangeStatusOrFail(newStatus);
            await _repo.ChangeStatusAsync(order.Id, newStatus);
        }

        public async Task DeleteAsync(Guid id)
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
