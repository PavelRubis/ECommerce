using ECommerce.Core.Aggregates;
using ECommerce.Core.RepositoryInterfaces;
using ECommerce.Core.ServiceInterfaces;
using ECommerce.Core.Utils;
using ECommerce.DAL.Models;
using ECommerce.DAL.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ECommerce.Web.Controllers
{
    [Route("api/Orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _ordersService;
        private readonly IUnitOfWork _unitOfWork;
        public OrdersController(IOrdersService ordersService, IUnitOfWork unitOfWork)
        {
            _ordersService = ordersService;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{id}")]
        public async Task<IDTO<Order>> Get(Guid id)
        {
            var orderDTO = await _unitOfWork.OrdersRepository.GetDtoByIdAsync(id);
            return orderDTO;
        }

        [HttpGet("{status}/{page}/{pageSize}/{withItems}")]
        public async Task<List<IDTO<Order>>> GetbyStatusAsync(string statusStr, int page, int pageSize, bool withItems = false)
        {
            var dtos = await _ordersService.GetbyStatusAsync(statusStr, page, pageSize, withItems);
            return dtos;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] OrderEntity orderEntity)
        {
            try
            {
                var id = await _unitOfWork.OrdersRepository.CreateAsync(orderEntity.GetOriginalObject());
                _unitOfWork.CommitTransaction();
                return Ok(id);
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackTransaction();
                return BadRequest(ex);
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }

        [HttpPut("SubmitShipping/{id}")]
        public async Task<IActionResult> SubmitShipping(Guid id)
        {
            try
            {
                await _ordersService.SubmitShippingAsync(id, DateTime.UtcNow);
                _unitOfWork.CommitTransaction();
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackTransaction();
                return BadRequest(ex);
            }
            finally
            {
                _unitOfWork.Dispose();
            }
            return Ok();
        }

        [HttpPut("Complete/{id}")]
        public async Task<IActionResult> Complete(Guid id)
        {
            try
            {
                await _ordersService.CompleteAsync(id);
                _unitOfWork.CommitTransaction();
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackTransaction();
                return BadRequest(ex);
            }
            finally
            {
                _unitOfWork.Dispose();
            }
            return Ok();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _ordersService.DeleteAsync(id);
                _unitOfWork.CommitTransaction();
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackTransaction();
                return BadRequest(ex);
            }
            finally
            {
                _unitOfWork.Dispose();
            }
            return Ok();
        }
    }
}
