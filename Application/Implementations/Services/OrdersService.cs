using Application.Implementations.Specifications;
using ECommerce.Core.Aggregates;
using ECommerce.Core.RepositoryInterfaces;
using ECommerce.Core.ServiceInterfaces;
using ECommerce.Core.Utils;
using ECommerce.Core.ValueObjects;
using ECommerce.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Implementations.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IOrdersRepository _repo;

        public OrdersService(IOrdersRepository repository)
        {
            _repo = repository;
        }

        public IOrderSpecification ByStatusSpec(string statusStr)
        {
            return new HasSuchStatusSpec(statusStr);
        }

        public async Task<IEnumerable<IDTO<Order>>> GetbyStatusAsync(string statusStr, int page, int pageSize, bool withItems = false)
        {
            var spec = this.ByStatusSpec(statusStr);
            var dtos = await _repo.GetDtosBySpecificationAsync(spec, page, pageSize, withItems);
            return dtos;
        }

        public async Task SubmitShippingAsync(Guid id, DateTime shipmentDate)
        {
            var orderDTO = await _repo.GetDtoByIdAsync(id);
            var order = orderDTO.GetOriginalObject();
            order.ChangeStatusOrFail(new ShippingOrderStatus(shipmentDate));
            _repo.EditAsync(order);
        }

        public async Task CompleteAsync(Guid id)
        {
            var orderDTO = await _repo.GetDtoByIdAsync(id);
            var order = orderDTO.GetOriginalObject();
            order.ChangeStatusOrFail(new ShippedOrderStatus());
            _repo.EditAsync(order);
        }

        public async Task DeleteAsync(Guid id)
        {
            var orderDTO = await _repo.GetDtoByIdAsync(id);
            if (orderDTO.GetOriginalObject().Status.Value == OrderStatusEnum.New)
            {
                await _repo.DeleteAsync(id);
            }
        }
    }
}
