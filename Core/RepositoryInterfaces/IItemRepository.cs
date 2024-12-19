using Core.Aggregates;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.RepositoryInterfaces
{
    public interface IItemRepository
    {
        Item GetById(Guid id);
        IEnumerable<Item> GetAll();
        Guid Create(Item item);
        Guid Edit(Item item);
        void Delete(Guid id);
    }
}
