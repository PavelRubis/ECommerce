using AutoMapper;
using ECommerce.Application.DTOs;
using ECommerce.Core.ServiceInterfaces;
using ECommerce.DAL.UnitOfWork;
using ECommerce.Web.Infrastructure.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    [Authorize(AuthConstants.ManagerRolePolicy)]
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
            var dto = await _unitOfWork.AccountsRepository.GetByIdAsProjectionAsync(id);
            return Ok(dto);
        }

        [HttpGet("{page}/{pageSize}")]
        public async Task<IActionResult> GetByPage(int page, int pageSize)
        {
            var dtos = await _unitOfWork.AccountsRepository.GetByPageAsProjectionAsync(page, pageSize);
            return Ok(dtos);
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAll()
        {
            var dtos = await _unitOfWork.AccountsRepository.GetAllAsProjectionsAsync();
            return Ok(dtos);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] AccountWebDTO accDto)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                var newAcc = accDto.GetOriginalObject(true);
                var accountId = await _accountsService.CreateAsync(newAcc);
                var customerId = default(Guid);
                if (accDto.Customer != null)
                {
                    customerId = newAcc.Customer.Id;
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
        public async Task<IActionResult> Edit([FromBody] AccountWebDTO accDto)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                var acc = accDto.GetOriginalObject();
                var accountId = await _accountsService.EditAsync(acc);
                var customerId = default(Guid);
                if (accDto.Customer != null)
                {
                    customerId = acc.Customer.Id;
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
