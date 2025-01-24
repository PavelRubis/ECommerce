using ECommerce.Core.Aggregates;
using ECommerce.DAL.Models;
using Microsoft.EntityFrameworkCore;
using ECommerce.Core.RepositoryInterfaces;
using ECommerce.Core.DTOsInterfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ECommerce.Application.DTOs;

namespace ECommerce.DAL.Repositories
{
    public class ItemsRepository : ICRUDRepository<Item>
    {
        private readonly ECommerceDbContext _dbContext;
        private readonly IMapper _mapper;

        public ItemsRepository(ECommerceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IDTO<Item>> GetDtoByIdAsync(Guid id)
        {
            var entity = await _dbContext.Items
                .AsNoTracking()
                .ProjectTo<ItemWebDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(i => i.Id == id);
            return entity;
        }

        public async Task<List<IDTO<Item>>> GetAllDtosAsync()
        {
            var items = await _dbContext.Items
                .AsNoTracking()
                .ProjectTo<ItemWebDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return new List<IDTO<Item>>(items);
        }

        public async Task<List<IDTO<Item>>> GetDtosByPageAsync(int page, int pageSize)
        {
            var items = await _dbContext.Items
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<ItemWebDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return new List<IDTO<Item>>(items);
        }

        public async Task<Guid> CreateAsync(Item item)
        {
            var entry = await _dbContext.Items.AddAsync(_mapper.Map<ItemEntity>(item));
            return entry.Entity.Id;
        }

        public async Task<Guid> EditAsync(Item item)
        {
            var entity = await _dbContext.Items.AsNoTracking().FirstOrDefaultAsync(i => i.Id == item.Id);
            if (entity == null)
            {
                throw new NullReferenceException("Item not found");
            }
            _dbContext.Items.Update(_mapper.Map<ItemEntity>(item));
            return entity.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _dbContext.Items.FirstOrDefaultAsync(i => i.Id == id);
            if (entity == null)
            {
                throw new NullReferenceException("Item not found");
            }
            _dbContext.Items.Remove(entity);
        }
    }
}