using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Interfaces
{
    public interface IAccountContext
    {
        public Guid AccountId { get; set; }
        public AppRole Role { get; set; }
    }

    public enum AppRole
    {
        Customer,
        Manager
    }
}
