using ECommerce.Core.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.DTOsInterfaces
{
    public interface IDTO<T>
    {
    }

    public interface IOrderDTO : IDTO<Order>
    {
        public Order GetOriginalObject();
    }
}
