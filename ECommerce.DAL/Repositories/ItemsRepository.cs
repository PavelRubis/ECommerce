using ECommerce.Core.Aggregates;
using ECommerce.DAL.Models;
using Microsoft.EntityFrameworkCore;
using ECommerce.Core.RepositoryInterfaces;
using ECommerce.Core.Utils;

namespace ECommerce.DAL.Repositories
{
    public class ItemsRepository : ICRUDRepository<Item>
    {
        private readonly ECommerceDbContext _dbContext;

        public ItemsRepository(ECommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IDTO<Item>> GetDtoByIdAsync(Guid id)
        {
            var entity = await _dbContext.Items.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
            if (entity == null)
            {
                throw new NullReferenceException("Item not found");
            }
            return entity;
        }

        public async Task<List<IDTO<Item>>> GetAllDtosAsync()
        {
            var items = await _dbContext.Items
                .AsNoTracking()
                .ToListAsync();
            return new List<IDTO<Item>>(items);
        }

        public async Task<List<IDTO<Item>>> GetDtosByPageAsync(int page, int pageSize)
        {
            var items = await _dbContext.Items
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return new List<IDTO<Item>>(items);
        }

        public async Task<Guid> CreateAsync(Item item)
        {
            var itemEntity = new ItemEntity();
            itemEntity.SetDataFromObject(item);
            var entry = await _dbContext.Items.AddAsync(itemEntity);
            return entry.Entity.Id;
        }

        public async Task<Guid> EditAsync(Item item)
        {
            var entity = await _dbContext.Items.FirstOrDefaultAsync(i => i.Id == item.Id);
            if (entity == null)
            {
                throw new NullReferenceException("Item not found");
            }
            entity.SetDataFromObject(item);
            _dbContext.Items.Update(entity);
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