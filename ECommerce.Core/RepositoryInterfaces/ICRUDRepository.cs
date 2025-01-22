using ECommerce.Core.Aggregates;
using ECommerce.Core.DTOsInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.RepositoryInterfaces
{
    public interface ICRUDRepository<T> where T : class
    {
        Task<IDTO<T>> GetDtoByIdAsync(Guid id);
        Task<List<IDTO<T>>> GetDtosByPageAsync(int page, int pageSize);
        Task<List<IDTO<T>>> GetAllDtosAsync();
        Task<Guid> CreateAsync(T item);
        Task<Guid> EditAsync(T item);
        Task DeleteAsync(Guid id);
    }
}
