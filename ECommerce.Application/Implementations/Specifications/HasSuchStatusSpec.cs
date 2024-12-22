using ECommerce.Core.Aggregates;
using ECommerce.Core.OtherInterfaces;
using ECommerce.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Implementations.Specifications
{
    public class HasSuchStatusSpec : IOrderSpecification
    {
        private readonly string _statusStr;
        public HasSuchStatusSpec(string statusStr)
        {
            _statusStr = statusStr;
        }

        public Expression<Func<IOrderDTO, bool>> ToExpression()
        {
            return orderEntity => orderEntity.Status == _statusStr;
        }
    }
}
