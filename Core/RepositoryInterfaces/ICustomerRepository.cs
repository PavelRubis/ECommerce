using Core.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.RepositoryInterfaces
{
    public interface ICustomerRepository
    {
        Customer GetById(Guid id);
        IEnumerable<Customer> GetAll();
        Guid Create(Customer customer);
        Guid Edit(Customer customer);
        void Delete(Guid id);
    }
}
