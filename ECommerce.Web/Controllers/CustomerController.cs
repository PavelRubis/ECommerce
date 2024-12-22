using AutoMapper;
using ECommerce.Core.Aggregates;
using ECommerce.Core.RepositoryInterfaces;
using ECommerce.Core.ServiceInterfaces;
using ECommerce.Core.OtherInterfaces;
using ECommerce.DAL.DTOs;
using ECommerce.DAL.Models;
using ECommerce.DAL.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Controllers
{

    [Route("api/customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var dto = await _unitOfWork.CustomersRepository.GetDtoByIdAsync(id);
            return Ok(dto);
        }

        [HttpGet("{page}/{pageSize}")]
        public async Task<IActionResult> GetByPage(int page, int pageSize)
        {
            var dtos = await _unitOfWork.CustomersRepository.GetDtosByPageAsync(page, pageSize);
            return Ok(dtos);
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAll()
        {
            var dtos = await _unitOfWork.CustomersRepository.GetAllDtosAsync();
            return Ok(dtos);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CustomerWebDTO customerDto)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                var customer = _mapper.Map<Customer>(customerDto);
                var customerId = await _unitOfWork.CustomersRepository.CreateAsync(customer);
                customerDto.Account.CustomerId = customerId;
                var accountId = await _unitOfWork.AccountsRepository.CreateAsync(customerDto.Account);
                _unitOfWork.CommitTransaction();
                return Ok(new { customerId , accountId });
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

        [HttpPut("Edit")]
        public async Task<IActionResult> Edit([FromBody] CustomerWebDTO customerDto)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                var customer = _mapper.Map<Customer>(customerDto);
                var customerId = await _unitOfWork.CustomersRepository.EditAsync(customer);
                customerDto.Account.CustomerId = customerId;
                var accountId = await _unitOfWork.AccountsRepository.EditAsync(customerDto.Account);
                _unitOfWork.CommitTransaction();
                return Ok(new { customerId, accountId });
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

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                await _unitOfWork.CustomersRepository.DeleteAsync(id);
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
