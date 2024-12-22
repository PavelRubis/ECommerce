using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.OtherInterfaces
{
    public interface IOrderSpecification
    {
        Expression<Func<IOrderDTO, bool>> ToExpression();
    }
}
