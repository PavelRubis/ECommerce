using ECommerce.Core.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.ServiceInterfaces
{
    public interface IProjection<T>
    {
        void SetDataFromOriginalObject(T obj);
    }
}
