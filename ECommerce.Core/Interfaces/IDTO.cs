using ECommerce.Core.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Interfaces
{
    public interface IDTO<T>
    {
        public T GetOriginalObject(bool asNew = false);
    }
}
   