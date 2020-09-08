using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Ecommerce.Entities;

namespace Ecommerce.Ecommerce.Contracts
{
    public interface IOrderDetailBL : IDisposable
    {
        Task<List<OrderDetail>> GetOrderDetailsByOrderIDBL(Guid searchOrderID);
        Task<(bool,Guid)> AddOrderDetailsBL(OrderDetail newOrder);
        Task<bool> UpdateOrderDetailsBL(OrderDetail updateOrder);
        Task<bool> DeleteOrderDetailsBL(Guid deleteOrderID);
    }
}
