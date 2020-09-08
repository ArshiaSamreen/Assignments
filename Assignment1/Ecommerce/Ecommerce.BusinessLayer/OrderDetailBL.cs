using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Ecommerce.Entities;
using Ecommerce.Ecommerce.DataAccessLayer;
using Ecommerce.Ecommerce.Contracts;
using Ecommerce.Ecommerce.Helpers;
using Ecommerce.Ecommerce.Exceptions;

namespace Ecommerce.Ecommerce.BusinessLayer
{
    public class OrderDetailBL : BLBase<OrderDetail>, IOrderDetailBL, IDisposable
    {
        readonly OrderDetailDALBase orderDetailDAL;

        protected override async Task<bool> Validate(OrderDetail entityObject)
        {            
            StringBuilder sb = new StringBuilder();
            bool valid = await base.Validate(entityObject);
            if (entityObject.ProductPrice <= 0)
            {
                valid = false;
                sb.Append(Environment.NewLine + "Total Price cannot be negative");
            }
            if (entityObject.ProductQuantityOrdered <= 0)
            {
                valid = false;
                sb.Append(Environment.NewLine + "Total Quantity cannot be negative");
            }
            
            ProductBL iProductBL = new ProductBL();
            var existingObject = await iProductBL.GetProductByProductIDBL(entityObject.ProductID);
            if (existingObject == null)
            {
                valid = false;
                sb.Append(Environment.NewLine + $"CustomerID {entityObject.ProductID} does not exists");
            }
            if (valid == false)
            { throw new Exception(sb.ToString()); }
            return valid;
        }
                        
        public async Task<(bool, Guid)> AddOrderDetailsBL(OrderDetail newOrder)
        {
            bool orderAdded = false;
            Guid OrderGuid=Guid.NewGuid();
            try
            {
                if (await Validate(newOrder))
                {
                    await Task.Run(() =>
                    {
                        (orderAdded, OrderGuid) = orderDetailDAL.AddOrderDetailsDAL(newOrder);
                        Serialize();
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
            return (orderAdded, OrderGuid);
        }

        public async Task<List<OrderDetail>> GetOrderDetailsByOrderIDBL(Guid searchOrderID)
        {
            List<OrderDetail> matchingOrder = null;
            try
            {
                await Task.Run(() =>
                {
                    matchingOrder = orderDetailDAL.GetOrderDetailsByOrderIDDAL(searchOrderID);
                });
            }
            catch (Exception)
            {
                throw;
            }
            return matchingOrder;
        }


        public async Task<bool> UpdateOrderDetailsBL(OrderDetail updateOrder)
        {
            bool orderUpdated = false;
            try
            {
                if ((await Validate(updateOrder)) && (await GetOrderDetailsByOrderIDBL(updateOrder.OrderId)) != null)
                {
                    this.orderDetailDAL.UpdateOrderDetailsDAL(updateOrder);
                    orderUpdated = true;
                    Serialize();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return orderUpdated;
        }

        public async Task<bool> DeleteOrderDetailsBL(Guid deleteOrderID)
        {
            bool orderDeleted = false;
            try
            {
                await Task.Run(() =>
                {
                    orderDeleted = orderDetailDAL.DeleteOrderDetailsDAL(deleteOrderID);
                    Serialize();
                });
            }
            catch (Exception)
            {
                throw;
            }
            return orderDeleted;
        }

        public async Task<bool> UpdateOrderDispatchedStatusBL(Guid orderID)
        {
            bool orderDispatched = false;
            try
            {
                await Task.Run(() =>
                {
                    orderDispatched = orderDetailDAL.UpdateOrderDispatchedStatusDAL(orderID);
                    Serialize();
                });
            }
            catch (Exception)
            {
                throw;
            }
            return orderDispatched;
        }

        public async Task<bool> UpdateOrderShippedStatusBL(Guid orderID)
        {
            bool orderShipped = false;
            try
            {
                await Task.Run(() =>
                {
                    orderShipped = orderDetailDAL.UpdateOrderShippedStatusDAL(orderID);
                    Serialize();
                });
            }
            catch (Exception)
            {
                throw;
            }
            return orderShipped;
        }

        public async Task<bool> UpdateOrderDeliveredStatusBL(Guid orderID)
        {
            bool orderDelivered = false;
            try
            {
                await Task.Run(() =>
                {
                    orderDelivered = orderDetailDAL.UpdateOrderDeliveredStatusDAL(orderID);
                    Serialize();
                });
            }
            catch (Exception)
            {
                throw;
            }
            return orderDelivered;
        }

        public OrderDetailBL()
        {
            orderDetailDAL = new OrderDetailDAL();
        }

        public void Dispose()
        {
            ((OrderDetailDAL)orderDetailDAL).Dispose();
        }

        public static void Serialize()
        {
            try
            {
                OrderDetailDAL.Serialize();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void Deserialize()
        {
            try
            {
                OrderDetailDAL.Deserialize();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
