using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ecommerce.Ecommerce.Entities;
using Ecommerce.Ecommerce.DataAccessLayer;
using Ecommerce.Ecommerce.Contracts.DALContracts;
using Ecommerce.Ecommerce.Contracts;
using System.Text;

namespace Ecommerce.Ecommerce.BusinessLayer
{
    public class OrderBL : BLBase<Order>, IOrderBL, IDisposable
    {
        readonly OrderDALBase orderDAL;       
        
        protected override async Task<bool> Validate(Order entityObject)
        {
            StringBuilder sb = new StringBuilder();
            bool valid = await base.Validate(entityObject);
            if (entityObject.OrderAmount <= 0)
            {
                valid = false;
                sb.Append(Environment.NewLine + "Total Amount cannot be negative");
            }
            if (entityObject.TotalQuantity <= 0)
            {
                valid = false;
                sb.Append(Environment.NewLine + "Total Quantity cannot be negative");
            }
            CustomerBL iCustomerBL = new CustomerBL();
            var existingObject = await iCustomerBL.GetCustomerByCustomerIDBL(entityObject.CustomerID);
            if (existingObject == null)
            {
                valid = false;
                sb.Append(Environment.NewLine + $"CustomerID {entityObject.CustomerID} does not exists");
            }
            if (valid == false)
            { throw new Exception(sb.ToString()); }
            return valid;
        }
            
        public async Task<(bool, Guid)> AddOrderBL(Order newOrder)
        {
            bool orderAdded = false;
            try
            {
                if (await Validate(newOrder))
                {
                    await Task.Run(() =>
                    {
                        (orderAdded, newOrder.OrderId) = orderDAL.AddOrderDAL(newOrder);
                        Serialize();
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
            return (orderAdded, newOrder.OrderId);
        }

        public async Task<Order> GetOrderByOrderIDBL(Guid searchOrderID)
        {
            Order matchingOrder = null;
            try
            {
                await Task.Run(() =>
                {
                    matchingOrder = orderDAL.GetOrderByOrderIDDAL(searchOrderID);
                });
            }
            catch (Exception)
            {
                throw;
            }
            return matchingOrder;
        }

        public async Task<Order> GetOrderByOrderNumberBL(double orderNumber)
        {
            Order matchingOrder = null;
            try
            {
                await Task.Run(() =>
                {
                    matchingOrder = orderDAL.GetOrderByOrderNumberDAL(orderNumber);
                });
            }
            catch (Exception)
            {
                throw;
            }
            return matchingOrder;
        }

        public async Task<List<Order>> GetOrdersByCustomerIDBL(Guid searchCustomerID)
        {
            List<Order> matchingOrder = null;
            try
            {
                await Task.Run(() =>
                {
                    matchingOrder = orderDAL.GetOrdersByCustomerIDDAL(searchCustomerID);
                });
            }
            catch (Exception)
            {
                throw;
            }
            return matchingOrder;
        }

        public async Task<bool> UpdateOrderBL(Order updateOrder)
        {
            bool orderUpdated = false;
            try
            {
                if ((await Validate(updateOrder)) && (await GetOrderByOrderIDBL(updateOrder.OrderId)) != null)
                {
                    this.orderDAL.UpdateOrderDAL(updateOrder);
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

        public async Task<bool> DeleteOrderBL(Guid deleteOrderID)
        {
            bool orderDeleted = false;
            try
            {
                await Task.Run(() =>
                {
                    orderDeleted = orderDAL.DeleteOrderDAL(deleteOrderID);
                    Serialize();
                });
            }
            catch (Exception)
            {
                throw;
            }
            return orderDeleted;
        }

        public OrderBL()
        {
            orderDAL = new OrderDAL();
        }
        public void Dispose()
        {
            ((OrderDAL)orderDAL).Dispose();
        }

        public void Serialize()
        {
            try
            {
                OrderDAL.Serialize();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Deserialize()
        {
            try
            {
                OrderDAL.Deserialize();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
