using AutoMapper;
using ECommerce.Core.Aggregates;
using ECommerce.Core.RepositoryInterfaces;
using ECommerce.Core.ServiceInterfaces;
using ECommerce.Core.Interfaces;
using ECommerce.Application.DTOs;
using ECommerce.DAL.Models;
using ECommerce.DAL.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Web;
using ECommerce.Web.Infrastructure.Auth;

namespace ECommerce.Web.Controllers
{

    [Route("api/items")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ItemsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [Authorize(AuthConstants.AnyRolePolicy)]
        public async Task<IActionResult> Get(Guid id)
        {
            var orderDTO = await _unitOfWork.ItemsRepository.GetDtoByIdAsync(id);
            return Ok(orderDTO);
        }

        [HttpGet("{page}/{pageSize}")]
        [Authorize(AuthConstants.AnyRolePolicy)]
        public async Task<IActionResult> GetByPage(int page, int pageSize)
        {
            var dtos = await _unitOfWork.ItemsRepository.GetDtosByPageAsync(page, pageSize);
            return Ok(dtos);
        }

        [HttpGet("All")]
        [Authorize(AuthConstants.AnyRolePolicy)]
        public async Task<IActionResult> GetAll()
        {
            var dtos = await _unitOfWork.ItemsRepository.GetAllDtosAsync();
            return Ok(dtos);
        }

        [HttpPost("Create")]
        [Authorize(AuthConstants.ManagerRolePolicy)]
        public async Task<IActionResult> Create([FromBody] ItemWebDTO itemDto)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                var id = await _unitOfWork.ItemsRepository.CreateAsync(itemDto.GetOriginalObject(true));
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
        [Authorize(AuthConstants.ManagerRolePolicy)]
        public async Task<IActionResult> Edit([FromBody] ItemWebDTO itemDto)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                var id = await _unitOfWork.ItemsRepository.EditAsync(itemDto.GetOriginalObject());
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
        [Authorize(AuthConstants.ManagerRolePolicy)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                _unitOfWork.BeginTransaction();
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
