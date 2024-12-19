using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utils
{
    public interface IProjection<T>
    {
        IProjection<T> From(T obj);
        T To(IProjection<T> projection);
    }
}
