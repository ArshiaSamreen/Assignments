using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Capgemini.Ecommerce.Entities;
using Capgemini.Ecommerce.Contracts;
using Capgemini.Ecommerce.Helpers;
using Capgemini.Ecommerce.Contracts.DALContracts;

namespace Capgemini.Ecommerce.DataAccessLayer
{
    /// <summary>
    /// Contains data access layer methods for inserting, updating orders from ordersList collection.
    /// </summary>
    public class OrderDAL : OrderDALBase, IDisposable
    {
        /// <summary>
        /// Adds new order to ordersList collection.
        /// </summary>
        /// <param name="newOrder">Contains the order details to be added.</param>
        /// <returns>Determinates whether the new order is added.</returns>
        public override bool AddOrderDAL(Order newOrder)
        {
            bool orderAdded = false;
            try
            {
                newOrder.OrderNumber = ordersList.Count + 1;
                newOrder.DateOfOrder = DateTime.Now;
                newOrder.LastModifiedDateTime = DateTime.Now;
                ordersList.Add(newOrder);
                orderAdded = true;
            }
            catch (Exception)
            {
                throw;
            }
            return orderAdded;
        }

        /// <summary>
        /// Constructor for OrderDAL
        /// </summary>
        public OrderDAL()
        {
            Serialize();
            Deserialize();
        }


        /// <summary>
        /// Gets Order based on OrderID.
        /// </summary>
        /// <param name="searchOrderID">Represents OrderID to search.</param>
        /// <returns>Returns Order object.</returns>
        public override Order GetOrderByOrderIDDAL(Guid searchOrderID)
        {
            Order matchingOrder = null;
            try
            {
                //Find Order based on searchOrderID
                matchingOrder = ordersList.Find(
                    (item) => { return item.OrderId == searchOrderID; }
                );
            }
            catch (Exception)
            {
                throw;
            }
            return matchingOrder;
        }

        public override Order GetOrderByOrderNumberDAL(double orderNumber)
        {
            Order matchingOrder = null;
            try
            {
                //Find Order based on searchOrderID
                matchingOrder = ordersList.Find(
                    (item) => { return item.OrderNumber == orderNumber; }
                );
            }
            catch (Exception)
            {
                throw;
            }
            return matchingOrder;
        }


        public override List<Order> GetOrdersByRetailerIDDAL(Guid searchRetailerID)
        {
            List<Order> matchingOrder = null;
            try
            {
                //Find Order based on searchOrderID
                matchingOrder = ordersList.FindAll(
                    (item) => { return item.RetailerID == searchRetailerID; }
                );
            }
            catch (Exception)
            {
                throw;
            }
            return matchingOrder;
        }

        /// <summary>
        /// Updates Order based on OrderID.
        /// </summary>
        /// <param name="updateOrder">Represents Order details like OrderID</param>
        /// <returns>Determinates whether the existing Order is updated.</returns>


        public override bool UpdateOrderDAL(Order updateOrder)
        {
            bool orderUpdated = false;
            try
            {
                //Find Order based on OrderID
                Order matchingOrder = GetOrderByOrderIDDAL(updateOrder.OrderId);

                if (matchingOrder != null)
                {
                    //Update order details
                    ReflectionHelpers.CopyProperties(updateOrder, matchingOrder, new List<string>() { "TotalQuantity", "OrderAmount" });
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
        /// Deletes order based on OrderID.
        /// </summary>
        /// <param name="deleteOrderID">Represents OrderID to delete.</param>
        /// <returns>Determinates whether the existing order is updated.</returns>
        public override bool DeleteOrderDAL(Guid deleteOrderID)
        {
            bool orderDeleted = false;
            try
            {
                //Find Order based on orderID
                Order matchingOrder = ordersList.Find(
                    (item) => { return item.OrderId == deleteOrderID; }
                );

                if (matchingOrder != null)
                {
                    //Delete order from the collection
                    ordersList.Remove(matchingOrder);
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
