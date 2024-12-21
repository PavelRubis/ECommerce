using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Utils
{
    public interface IDTO<T>
    {
        T GetOriginalObject();
        void SetDataFromObject(T obj);
    }
}
