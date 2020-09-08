using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Capgemini.Ecommerce.Entities;
using Capgemini.Ecommerce.Contracts;
using Capgemini.Ecommerce.Helpers;

namespace Capgemini.Ecommerce.DataAccessLayer
{
    /// <summary>
    /// Contains data access layer methods for inserting, updating order details from orderDetails collection.
    /// </summary>
    public class OrderDetailDAL : OrderDetailDALBase, IDisposable
    {
        /// <summary>
        /// Adds new orderdetails to orderdetails List collection.
        /// </summary>
        /// <param name="newOrder">Contains the order details to be added.</param>
        /// <returns>Determinates whether the new order is added.</returns>
        public override bool AddOrderDetailsDAL(OrderDetail newOrder)
        {
            bool orderAdded = false;
            try
            {
                newOrder.OrderId = Guid.NewGuid();
                newOrder.DateOfOrder = DateTime.Now;
                newOrder.LastModifiedDateTime = DateTime.Now;
                orderDetailsList.Add(newOrder);
                orderAdded = true;
            }
            catch (Exception)
            {
                throw;
            }
            return orderAdded;
        }

        /// <summary>
        /// Constructor for OrderDetailsDAL
        /// </summary>
        public OrderDetailDAL()
        {
            Serialize();
            Deserialize();
        }


        /// <summary>
        /// Gets OrderDetail based on OrderID.
        /// </summary>
        /// <param name="searchOrderID">Represents OrderID to search.</param>
        /// <returns>Returns OrderDetail object.</returns>
        public override List<OrderDetail> GetOrderDetailsByOrderIDDAL(Guid searchOrderID)
        {
            List<OrderDetail> matchingOrder = new List<OrderDetail>();
            try
            {
                //Find OrderDetail based on searchOrderID
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
                //Find OrderDetail based on searchOrderID
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

        /// <summary>
        /// Updates OrderDetail based on OrderID.
        /// </summary>
        /// <param name="updateOrder">Represents Order details like OrderID</param>
        /// <returns>Determinates whether the existing OrderDetail is updated.</returns>


        public override bool UpdateOrderDetailsDAL(OrderDetail updateOrder)
        {
            bool orderUpdated = false;
            try
            {
                //Find OrderDetail based on OrderID
                OrderDetail matchingOrder = GetOrderDetailByOrderIDDAL(updateOrder.OrderId);

                if (matchingOrder != null)
                {
                    //Update OrderDetail details
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

        /// <summary>
        /// gives wheather the order is dispatched or not
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns>determines wheather the order is dispatched</returns>
        public override bool UpdateOrderDispatchedStatusDAL(Guid orderId)
        {
            bool orderDispatched = false;
            try
            {
                //Find OrderDetail based on orderID
                OrderDetail matchingOrder = orderDetailsList.Find(
                    (item) => { return item.OrderId == orderId; }
                );
                if (matchingOrder != null)
                {
                    //order is dispatched
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


        /// <summary>
        /// gives wheather the order is shipped or not
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns>determines wheather the order is shipped</returns
        public override bool UpdateOrderShippedStatusDAL(Guid orderId)
        {
            bool orderShipped = false;
            try
            {
                //Find OrderDetail based on orderID
                OrderDetail matchingOrder = orderDetailsList.Find(
                    (item) => { return item.OrderId == orderId; }
                );
                if (matchingOrder != null)
                {
                    //order is shipped
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


        /// <summary>
        /// gives wheather the order is delivered or not
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns>determines wheather the order is delivered</returns
        public override bool UpdateOrderDeliveredStatusDAL(Guid orderId)
        {
            bool orderDelivered = false;
            try
            {
                //Find OrderDetail based on orderID
                OrderDetail matchingOrder = orderDetailsList.Find(
                    (item) => { return item.OrderId == orderId; }
                );
                if (matchingOrder != null)
                {
                    //order is delivered
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

        /// <summary>
        /// Deletes OrderDetail based on OrderID.
        /// </summary>
        /// <param name="deleteOrderID">Represents OrderID to delete.</param>
        /// <returns>Determinates whether the existing OrderDetail is updated.</returns>
        public override bool DeleteOrderDetailsDAL(Guid deleteOrderID)
        {
            bool orderDeleted = false;
            try
            {
                //Find OrderDetail based on orderID
                OrderDetail matchingOrder = orderDetailsList.Find(
                    (item) => { return item.OrderId == deleteOrderID; }
                );

                if (matchingOrder != null)
                {
                    //Delete OrderDetail from the collection
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

        /// <summary>
        /// Clears unmanaged resources such as db connections or file streams.
        /// </summary>
        public void Dispose()
        {
            //No unmanaged resources currently
        }
    }
}
