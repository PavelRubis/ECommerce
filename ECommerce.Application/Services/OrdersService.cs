using ECommerce.Application.InfrastructureInterfaces;
using ECommerce.Core.Aggregates;
using ECommerce.Core.Interfaces;
using ECommerce.Core.RepositoryInterfaces;
using ECommerce.Core.ServiceInterfaces;
using ECommerce.Core.ValueObjects;
using System.Security.Principal;

namespace ECommerce.Application.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IOrdersRepository _repo;
        private readonly Account _currentAccount;

        public OrdersService(IOrdersRepository repository, ICurrentAccountContext context)
        {
            _repo = repository;
            _currentAccount = context?.CurrentAccount;
        }

        public async Task<IDTO<Order>> Get(Guid id)
        {
            this.RequireCurrentAccountExistence();
            if (_currentAccount.Role == AccountRole.Manager)
            {
                var orderDTO = await _repo.GetDtoByIdAsync(id);
                return orderDTO;
            }
            var ownOrderDTO = await _repo.GetOwnDtoByIdAsync(_currentAccount.Customer, id);
            return ownOrderDTO;
        }

        public async Task<List<IDTO<Order>>> GetDtosAsync(bool withItems = false)
        {
            this.RequireCurrentAccountExistence();
            if (_currentAccount.Role == AccountRole.Manager)
            {
                var dtos = await _repo.GetDtosAsync(withItems);
                return dtos;
            }
            var ownOrdersDTO = await _repo.GetOwnDtosAsync(_currentAccount.Customer, withItems);
            return ownOrdersDTO;
        }

        public async Task<List<IDTO<Order>>> GetDtosByStatusAsync(string statusStr, int page, int pageSize, bool withItems = false)
        {
            this.RequireCurrentAccountExistence();
            if (_currentAccount.Role == AccountRole.Manager)
            {
                var dtos = await _repo.GetDtosByStatusAsync(statusStr, page, pageSize, withItems);
                return dtos;
            }
            var ownOrdersDTO = await _repo.GetOwnDtosByStatusAsync(_currentAccount.Customer, statusStr, page, pageSize, withItems);
            return ownOrdersDTO;
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

        public async Task DeleteOwnAsync(Guid id)
        {
            var orderDto = await _repo.GetDtoByIdAsync(id);
            var order = orderDto.GetOriginalObject();
            if (order.CustomerId != _currentAccount?.Customer?.Id)
            {
                throw new ApplicationException("Invalid operation.");
            }
            if (order.CanBeDeleted)
            {
                await _repo.DeleteAsync(id);
                return;
            }
            throw new InvalidOperationException("Order with such status can not be deleted.");
        }

        private void RequireCurrentAccountExistence()
        {
            if (_currentAccount == null)
            {
                throw new ApplicationException("Invalid operation.");
            }
        }
    }
}
