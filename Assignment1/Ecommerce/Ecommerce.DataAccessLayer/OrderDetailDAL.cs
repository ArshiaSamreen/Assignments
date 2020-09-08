using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Ecommerce.Ecommerce.Entities;
using Ecommerce.Ecommerce.Contracts;
using Ecommerce.Ecommerce.Helpers;

namespace Ecommerce.Ecommerce.DataAccessLayer
{    
    public class OrderDetailDAL : OrderDetailDALBase, IDisposable
    {        
        public override (bool, Guid) AddOrderDetailsDAL(OrderDetail newOrder)
        {
            bool orderAdded = false;
            try
            {
                newOrder.OrderDetailId = Guid.NewGuid();
                newOrder.DateOfOrder = DateTime.Now;
                newOrder.LastModifiedDateTime = DateTime.Now;
                orderDetailsList.Add(newOrder);
                orderAdded = true;
            }
            catch (Exception)
            {
                throw;
            }
            return (orderAdded, newOrder.OrderDetailId);
        }

        public override List<OrderDetail> GetOrderDetailsByOrderIDDAL(Guid searchOrderID)
        {
            List<OrderDetail> matchingOrder = new List<OrderDetail>();
            try
            {                
                matchingOrder = orderDetailsList.FindAll(
                    (item) => { return item.OrderId == searchOrderID; }
                );
            }
            catch (Exception)
            {
                throw;
            }
            return matchingOrder;
        }

        public override OrderDetail GetOrderDetailByOrderIDDAL(Guid searchOrderID)
        {
            OrderDetail matchingOrder = null;
            try
            {                
                matchingOrder = orderDetailsList.Find(
                    (item) => { return item.OrderId == searchOrderID; }
                );
            }
            catch (Exception)
            {
                throw;
            }
            return matchingOrder;
        }

        public override bool UpdateOrderDetailsDAL(OrderDetail updateOrder)
        {
            bool orderUpdated = false;
            try
            {                
                OrderDetail matchingOrder = GetOrderDetailByOrderIDDAL(updateOrder.OrderId);

                if (matchingOrder != null)
                {                    
                    ReflectionHelpers.CopyProperties(updateOrder, matchingOrder, new List<string>() { "ProductQuantityOrdered", "TotalAmount" });
                    matchingOrder.LastModifiedDateTime = DateTime.Now;
                    orderUpdated = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return orderUpdated;
        }

        public override bool UpdateOrderDispatchedStatusDAL(Guid orderId)
        {
            bool orderDispatched = false;
            try
            {                
                OrderDetail matchingOrder = orderDetailsList.Find(
                    (item) => { return item.OrderId == orderId; }
                );
                if (matchingOrder != null)
                {                    
                    if (matchingOrder.DateOfOrder.AddDays(2) >= DateTime.Now)
                        orderDispatched = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return orderDispatched;
        }

        public override bool UpdateOrderShippedStatusDAL(Guid orderId)
        {
            bool orderShipped = false;
            try
            {                
                OrderDetail matchingOrder = orderDetailsList.Find(
                    (item) => { return item.OrderId == orderId; }
                );
                if (matchingOrder != null)
                {                    
                    if (matchingOrder.DateOfOrder.AddDays(5) >= DateTime.Now && matchingOrder.DateOfOrder.AddDays(2) < DateTime.Now)
                        orderShipped = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return orderShipped;
        }

        public override bool UpdateOrderDeliveredStatusDAL(Guid orderId)
        {
            bool orderDelivered = false;
            try
            {                
                OrderDetail matchingOrder = orderDetailsList.Find(
                    (item) => { return item.OrderId == orderId; }
                );
                if (matchingOrder != null)
                {                    
                    if (matchingOrder.DateOfOrder.AddDays(6) >= DateTime.Now)
                        orderDelivered = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return orderDelivered;
        }

        public override bool DeleteOrderDetailsDAL(Guid deleteOrderID)
        {
            bool orderDeleted = false;
            try
            {                
                OrderDetail matchingOrder = orderDetailsList.Find(
                    (item) => { return item.OrderId == deleteOrderID; }
                );

                if (matchingOrder != null)
                {                    
                    orderDetailsList.Remove(matchingOrder);
                    orderDeleted = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return orderDeleted;
        }

        public void Dispose()
        {
            //No unmanaged resources currently
        }
    }
}
