using Ecommerce.Ecommerce.BusinessLayer;
using Ecommerce.Ecommerce.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Ecommerce.Ecommerce.UnitTests
{
    [TestClass]
    public class AddOrderDetailsBLTest
    {
        /// <summary>
        /// Add Order Detail to the Collection if it is valid.
        /// </summary>
        [TestMethod]
        public async Task AddValidOrderDetail()
        {
            //Arrange
            OrderDetailBL orderDetailBL = new OrderDetailBL();
            OrderDetail orderDetail = new OrderDetail() {ProductID = Guid.NewGuid(), TotalAmount = 10, ProductQuantityOrdered = 10, ProductPrice=10 };
            bool isAdded = false;
            Guid newGuid;
            string errorMessage = null;

            //Act
            try
            {
                Customer customer = new Customer() { CustomerName = "Arshia", CustomerMobile = "9039607074", Password = "Arshia@123", Email = "arshia@gmail.com" };
                CustomerBL customerBL = new CustomerBL();
                bool isAdded2 = false;
                Guid newGuid2;
                (isAdded2, newGuid2) = await customerBL.AddCustomerBL(customer);

                Order order = new Order() { CustomerID = newGuid2, TotalQuantity =10, OrderAmount=10 };
                OrderBL orderBL = new OrderBL();
                bool isAdded1 = false;
                Guid newGuid1;
                (isAdded1, newGuid1) = await orderBL.AddOrderBL(order);
                orderDetail.OrderId = newGuid1;
                (isAdded, newGuid) = await orderDetailBL.AddOrderDetailsBL(orderDetail);
                
            }
            catch (Exception ex)
            {
                isAdded = false;
                errorMessage = ex.Message;
            }
            finally
            {
                //Assert
                Assert.IsTrue(isAdded, errorMessage);
            }
        }

        /// <summary>
        /// Product ID should be unique and present in database
        /// </summary>
        [TestMethod]
        public async Task ProductIDNotMatch()
        {
            //Arrange
            OrderDetailBL orderDetailBL = new OrderDetailBL();
            OrderDetail orderDetail = new OrderDetail() { OrderId = Guid.NewGuid(), ProductID = Guid.NewGuid(), TotalAmount = 10, ProductQuantityOrdered = 10 };
            bool isAdded = false;
            Guid newGuid;
            string errorMessage = null;

            //Act
            try
            {
                (isAdded, newGuid) = await orderDetailBL.AddOrderDetailsBL(orderDetail);
            }
            catch (Exception ex)
            {
                isAdded = false;
                errorMessage = ex.Message;
            }
            finally
            {
                //Assert
                Assert.IsFalse(isAdded, errorMessage);
            }
        }


        /// <summary>
        /// Total Amount of Product can't be negative
        /// </summary>
        [TestMethod]
        public async Task TotalAmountCannotBeNegative()
        {
            //Arrange
            OrderDetailBL orderDetailBL = new OrderDetailBL();
            OrderDetail orderDetail = new OrderDetail() { OrderId = Guid.NewGuid(), ProductID = Guid.NewGuid(), TotalAmount = 10, ProductQuantityOrdered = 10 };
            bool isAdded = false;
            Guid newGuid;
            string errorMessage = null;

            //Act
            try
            {
                (isAdded, newGuid) = await orderDetailBL.AddOrderDetailsBL(orderDetail);
            }
            catch (Exception ex)
            {
                isAdded = false;
                errorMessage = ex.Message;
            }
            finally
            {
                //Assert
                Assert.IsFalse(isAdded, errorMessage);
            }
        }

        /// <summary>
        /// Total Quantity of Product can't be negative
        /// </summary>
        [TestMethod]
        public async Task TotalQuantityCannotBeNegative()
        {
            //Arrange
            OrderDetailBL orderDetailBL = new OrderDetailBL();
            OrderDetail orderDetail = new OrderDetail() { OrderId = Guid.NewGuid(), ProductID = Guid.NewGuid(), TotalAmount = 10, ProductQuantityOrdered = 10 };
            bool isAdded = false;
            Guid newGuid;
            string errorMessage = null;

            //Act
            try
            {
                (isAdded, newGuid) = await orderDetailBL.AddOrderDetailsBL(orderDetail);
            }
            catch (Exception ex)
            {
                isAdded = false;
                errorMessage = ex.Message;
            }
            finally
            {
                //Assert
                Assert.IsFalse(isAdded, errorMessage);
            }
        }

    }
}

