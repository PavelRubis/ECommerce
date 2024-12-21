using ECommerce.Core.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Utils
{
    public interface IOrderSpecDTO
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
    }
}
