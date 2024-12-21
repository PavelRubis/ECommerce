using ECommerce.Core.Aggregates;
using ECommerce.Core.RepositoryInterfaces;
using ECommerce.Core.ServiceInterfaces;
using ECommerce.Core.Utils;
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
        public CustomerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{id}")]
        public async Task<IDTO<Customer>> Get(Guid id)
        {
            var dto = await _unitOfWork.CustomersRepository.GetDtoByIdAsync(id);
            return dto;
        }

        [HttpGet("{page}/{pageSize}")]
        public async Task<List<IDTO<Customer>>> GetByPage(int page, int pageSize)
        {
            var dtos = await _unitOfWork.CustomersRepository.GetDtosByPageAsync(page, pageSize);
            return dtos;
        }

        [HttpGet("All")]
        public async Task<List<IDTO<Customer>>> GetAll()
        {
            var dtos = await _unitOfWork.CustomersRepository.GetAllDtosAsync();
            return dtos;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CustomerEntity customerEntity)
        {
            try
            {
                var id = await _unitOfWork.CustomersRepository.CreateAsync(customerEntity.GetOriginalObject());
                customerEntity.Account.CustomerId = id;
                await _unitOfWork.AccountsRepository.CreateAsync(customerEntity.Account);
                _unitOfWork.Commit();
                return Ok(id);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return BadRequest(ex);
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }

        [HttpPut("Edit")]
        public async Task<IActionResult> Edit([FromBody] CustomerEntity customerEntity)
        {
            try
            {
                var id = await _unitOfWork.CustomersRepository.EditAsync(customerEntity.GetOriginalObject());
                await _unitOfWork.AccountsRepository.EditAsync(customerEntity.Account);
                _unitOfWork.Commit();
                return Ok(id);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
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
                await _unitOfWork.CustomersRepository.DeleteAsync(id);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
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
