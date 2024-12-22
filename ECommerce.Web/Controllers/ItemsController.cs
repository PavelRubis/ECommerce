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

    [Route("api/items")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public ItemsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{id}")]
        public async Task<IDTO<Item>> Get(Guid id)
        {
            var orderDTO = await _unitOfWork.ItemsRepository.GetDtoByIdAsync(id);
            return orderDTO;
        }

        [HttpGet("{page}/{pageSize}")]
        public async Task<List<IDTO<Item>>> GetByPage(int page, int pageSize)
        {
            var dtos = await _unitOfWork.ItemsRepository.GetDtosByPageAsync(page, pageSize);
            return dtos;
        }

        [HttpGet("All")]
        public async Task<List<IDTO<Item>>> GetAll()
        {
            var dtos = await _unitOfWork.ItemsRepository.GetAllDtosAsync();
            return dtos;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] ItemEntity itemEntity)
        {
            try
            {
                var id = await _unitOfWork.ItemsRepository.CreateAsync(itemEntity.GetOriginalObject());
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

        [HttpPut("Edit")]
        public async Task<IActionResult> Edit([FromBody] ItemEntity itemEntity)
        {
            try
            {
                var id = await _unitOfWork.ItemsRepository.CreateAsync(itemEntity.GetOriginalObject());
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

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _unitOfWork.ItemsRepository.DeleteAsync(id);
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
