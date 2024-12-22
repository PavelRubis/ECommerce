using ECommerce.Core.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.OtherInterfaces
{
    public interface IOrderDTO
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        Order GetOriginalObject();
        void SetDataFromObject(Order order);
    }
}
