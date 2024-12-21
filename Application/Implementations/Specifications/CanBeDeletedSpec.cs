using ECommerce.Core.Aggregates;
using ECommerce.Core.Utils;
using ECommerce.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Implementations.Specifications
{
    public class CanBeDeletedSpec : IOrderSpecification
    {
        private readonly Guid _orderId;
        public CanBeDeletedSpec(Guid orderId)
        {
            _orderId = orderId;
        }

        public Expression<Func<IOrderSpecDTO, bool>> ToExpression()
        {
            return orderEntity => orderEntity.Id == _orderId && orderEntity.Status == "New";
        }
    }
}
