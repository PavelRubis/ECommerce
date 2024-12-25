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
using ECommerce.Application.Services;
using ECommerce.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Web;

namespace ECommerce.Web.Controllers
{

    [Route("api/accounts")]
    [ApiController]
    //[Authorize(Program.MANAGER_ROLE)]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountsService _accountsService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AccountsController(IAccountsService accountsService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _accountsService = accountsService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var dto = await _unitOfWork.AccountsRepository.GetByIdAsync(id);
            return Ok(dto);
        }

        [HttpGet("{page}/{pageSize}")]
        public async Task<IActionResult> GetByPage(int page, int pageSize)
        {
            var dtos = await _unitOfWork.AccountsRepository.GetByPageAsync(page, pageSize);
            return Ok(dtos);
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAll()
        {
            var dtos = await _unitOfWork.AccountsRepository.GetAllAsync();
            return Ok(dtos);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] AccountInWebDTO accDto)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                var accountId = await _accountsService.CreateAsync(accDto);
                var customerId = default(Guid);
                if (accDto.Customer != null)
                {
                    accDto.Customer.AccountId = accountId;
                    var customer = _mapper.Map<Customer>(accDto.Customer);
                    customerId = await _unitOfWork.CustomersRepository.CreateAsync(customer);
                }
                _unitOfWork.CommitTransaction();
                return Ok(new { accountId, customerId });
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
        public async Task<IActionResult> Edit([FromBody] AccountInWebDTO accDto)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                var accountId = await _accountsService.EditAsync(accDto);
                var customerId = default(Guid);
                if (accDto.Customer != null)
                {
                    accDto.Customer.AccountId = accountId;
                    var customer = _mapper.Map<Customer>(accDto.Customer);
                    customerId = await _unitOfWork.CustomersRepository.EditAsync(customer);
                }
                _unitOfWork.CommitTransaction();
                return Ok(new { accountId, customerId });
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
                await _unitOfWork.AccountsRepository.DeleteAsync(id);
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
