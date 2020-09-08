using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Ecommerce.Ecommerce.Entities;
using Ecommerce.Ecommerce.Contracts;
using Ecommerce.Ecommerce.Helpers;
using Ecommerce.Ecommerce.Contracts.DALContracts;

namespace Ecommerce.Ecommerce.DataAccessLayer
{    
    public class OrderDAL : OrderDALBase, IDisposable
    {

        public override (bool, Guid) AddOrderDAL(Order newOrder)
        {
            bool orderAdded;
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
            return (orderAdded, newOrder.OrderId);
        }

        public override Order GetOrderByOrderIDDAL(Guid searchOrderID)
        {
            Order matchingOrder = null;
            try
            {
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

        public override List<Order> GetOrdersByCustomerIDDAL(Guid searchCustomerID)
        {
            List<Order> matchingOrder = null;
            try
            {               
                matchingOrder = ordersList.FindAll(
                    (item) => { return item.CustomerID == searchCustomerID; }
                );
            }
            catch (Exception)
            {
                throw;
            }
            return matchingOrder;
        }
        
        public override bool UpdateOrderDAL(Order updateOrder)
        {
            bool orderUpdated = false;
            try
            {
                Order matchingOrder = GetOrderByOrderIDDAL(updateOrder.OrderId);
                if (matchingOrder != null)
                {
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

        public override bool DeleteOrderDAL(Guid deleteOrderID)
        {
            bool orderDeleted = false;
            try
            {
                Order matchingOrder = ordersList.Find(
                    (item) => { return item.OrderId == deleteOrderID; }
                );
                if (matchingOrder != null)
                {
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
                
        public void Dispose()
        {
        
        }
    }
}
