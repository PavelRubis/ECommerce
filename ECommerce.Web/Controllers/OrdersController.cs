using ECommerce.Core.Aggregates;
using ECommerce.Core.RepositoryInterfaces;
using ECommerce.Core.ServiceInterfaces;
using ECommerce.Core.OtherInterfaces;
using ECommerce.DAL.DTOs;
using ECommerce.DAL.Models;
using ECommerce.DAL.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Web;

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

        [Authorize(Program.CUSTOMER_ROLE)]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var orderDTO = await _unitOfWork.OrdersRepository.GetDtoByIdAsync(id);
            return Ok(orderDTO);
        }

        [Authorize(Program.CUSTOMER_ROLE)]
        [HttpGet("{statusStr}/{page}/{pageSize}/{withItems}")]
        public async Task<IActionResult> GetbyStatusAsync(string statusStr, int page, int pageSize, bool withItems = false)
        {
            var dtos = await _unitOfWork.OrdersRepository.GetDtosByStatusAsync(statusStr, page, pageSize, withItems);
            return Ok(dtos);
        }

        [HttpPost("Create")]
        [Authorize(Program.CUSTOMER_ROLE)]
        public async Task<IActionResult> Create([FromBody] OrderWebDTO orderDto)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                var id = await _unitOfWork.OrdersRepository.CreateAsync(orderDto.GetOriginalObject());
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
        [Authorize(Program.MANAGER_ROLE)]
        public async Task<IActionResult> SubmitShipping(Guid id)
        {
            try
            {
                _unitOfWork.BeginTransaction();
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
        [Authorize(Program.MANAGER_ROLE)]
        public async Task<IActionResult> Complete(Guid id)
        {
            try
            {
                _unitOfWork.BeginTransaction();
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
        [Authorize(Program.CUSTOMER_ROLE)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                _unitOfWork.BeginTransaction();
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
