using ECommerce.Core.Aggregates;
using ECommerce.Core.Entities;
using ECommerce.Core.OtherInterfaces;
using ECommerce.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.RepositoryInterfaces
{
    public interface IOrdersRepository
    {
        Task<IOrderDTO> GetDtoByIdAsync(Guid id, bool withItems = false);
        Task<List<IOrderDTO>> GetDtosAsync(int page, int pageSize, bool withItems = false);
        Task<List<IOrderDTO>> GetDtosBySpecificationAsync(IOrderSpecification spec, int page, int pageSize, bool withItems = false);
        Task<Guid> CreateAsync(Order order);
        Guid Edit(Order order);
        Task DeleteAsync(Guid id);
    }
}
