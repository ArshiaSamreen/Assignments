using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ecommerce.Ecommerce.BusinessLayer;
using Ecommerce.Ecommerce.Entities;
using Ecommerce.Ecommerce.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ecommerce.Ecommerce.UnitTests

{
    //Ayush Agrawal
    // Creation Date: 01-10-2019
    //Last Modified Date: 
    //Module Name: AddOnlineReturnUnitTest
    //Project: Ecommerce


    [TestClass]
    public class AddOnlineReturnBLTest
    {
        /// <summary>
        /// Add OnlineReturn to the Collection if it is valid.
        /// </summary>
        [TestMethod]
        public async Task AddValidOnlineReturn()
        {
            //Arrange
            ReturnBL onlineReturnBL = new ReturnBL();
            Return onlineReturn = new Return() { OrderNumber = 20, ProductNumber = 4, QuantityOfReturn = 5, Purpose = 0 };
            bool isAdded = false;
            string errorMessage = null;

            //Act
            try
            {
                isAdded = await onlineReturnBL.AddReturnBL(onlineReturn);
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
        /// OnlineReturn OrderNumber can't be Zero
        /// </summary>
        [TestMethod]
        public async Task OnlineReturnOrderNumberCanNotBeZero()
        {
            //Arrange
            ReturnBL onlineReturnBL = new ReturnBL();
            Return onlineReturn = new Return() { OrderNumber = 0, ProductNumber = 4, QuantityOfReturn = 5, Purpose = 0 };
            bool isAdded = false;
            string errorMessage = null;

            //Act
            try
            {
                isAdded = await onlineReturnBL.AddReturnBL(onlineReturn);
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
        /// OnlineReturn Product Number can't be Zero
        /// </summary>
        [TestMethod]
        public async Task OnlineReturnProductNumberCanNotBeZero()
        {
            //Arrange
            ReturnBL onlineReturnBL = new ReturnBL();
            Return onlineReturn = new Return() { OrderNumber = 20, ProductNumber = 0, QuantityOfReturn = 5, Purpose = 0 };
            bool isAdded = false;
            string errorMessage = null;

            //Act
            try
            {
                isAdded = await onlineReturnBL.AddReturnBL(onlineReturn);
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
        /// OnlineReturn QuantityOfReturn can't be Zero
        /// </summary>
        [TestMethod]
        public async Task OnlineReturnQuantityOfReturnCanNotBeZero()
        {
            //Arrange
            ReturnBL onlineReturnBL = new ReturnBL();
            Return onlineReturn = new Return() { OrderNumber = 20, ProductNumber = 4, QuantityOfReturn = 0, Purpose = 0 };
            bool isAdded = false;
            string errorMessage = null;

            //Act
            try
            {
                isAdded = await onlineReturnBL.AddReturnBL(onlineReturn);
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
        /// OnlineReturn Purpose can't be null
        /// </summary>
        [TestMethod]
        public async Task OnlineReturnPurposeCanNotBeNull()
        {
            //Arrange
            ReturnBL onlineReturnBL = new ReturnBL();
            Return onlineReturn = new Return() { OrderNumber = 20, ProductNumber = 4, QuantityOfReturn = 5, Purpose = PurposeOfReturn.DefectiveProduct };
            bool isAdded = false;
            string errorMessage = null;

            //Act
            try
            {
                isAdded = await onlineReturnBL.AddReturnBL(onlineReturn);
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
        /// OnlineReturn Purpose can't be null
        /// </summary>
        [TestMethod]
        public async Task OnlineReturnPurposeCanNotBeNull1()
        {
            //Arrange
            ReturnBL onlineReturnBL = new ReturnBL();
            Return onlineReturn = new Return() { OrderNumber = 20, ProductNumber = 4, QuantityOfReturn = 5, Purpose = PurposeOfReturn.UnsatiSfactoryProduct };
            bool isAdded = false;
            string errorMessage = null;

            //Act
            try
            {
                isAdded = await onlineReturnBL.AddReturnBL(onlineReturn);
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
        /// OnlineReturn Purpose can't be null
        /// </summary>
        [TestMethod]
        public async Task OnlineReturnPurposeCanNotBeNull2()
        {
            //Arrange
            ReturnBL onlineReturnBL = new ReturnBL();
            Return onlineReturn = new Return() { OrderNumber = 20, ProductNumber = 4, QuantityOfReturn = 5, Purpose = PurposeOfReturn.WrongProductOrdered };
            bool isAdded = false;
            string errorMessage = null;

            //Act
            try
            {
                isAdded = await onlineReturnBL.AddReturnBL(onlineReturn);
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
        /// OnlineReturn Purpose can't be null
        /// </summary>
        [TestMethod]
        public async Task OnlineReturnPurposeCanNotBeNull3()
        {
            //Arrange
            ReturnBL onlineReturnBL = new ReturnBL();
            Return onlineReturn = new Return() { OrderNumber = 20, ProductNumber = 4, QuantityOfReturn = 5, Purpose = PurposeOfReturn.WrongProductShipped };
            bool isAdded = false;
            string errorMessage = null;

            //Act
            try
            {
                isAdded = await onlineReturnBL.AddReturnBL(onlineReturn);
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



    }




    [TestClass]
    public class UpdateOnlineReturnBLTest
    {
        /// <summary>
        /// Update OnlineReturn to the Collection if it is valid.
        /// </summary>
        [TestMethod]
        public async Task UpdateValidOnlineReturn()
        {
            //Arrange
            ReturnBL onlineReturnBL = new ReturnBL();
            Return onlineReturn1 = new Return() { OrderNumber = 20, ProductNumber = 4, QuantityOfReturn = 5, Purpose = PurposeOfReturn.DefectiveProduct };
            await onlineReturnBL.AddReturnBL(onlineReturn1);
            Return onlineReturn2 = new Return() { OnlineReturnID = onlineReturn1.OnlineReturnID, OrderNumber = 20, ProductNumber = 4, QuantityOfReturn = 15, Purpose = PurposeOfReturn.DefectiveProduct };

            bool isUpdated = false;
            string errorMessage = null;

            //Act
            try
            {
                isUpdated = await onlineReturnBL.UpdateReturnBL(onlineReturn2);
            }
            catch (Exception ex)
            {
                isUpdated = false;
                errorMessage = ex.Message;
            }
            finally
            {
                //Assert
                Assert.IsTrue(isUpdated, errorMessage);
            }
        }

        /// <summary>
        /// OnlineReturn OrderNumber can't be Zero
        /// </summary>
        [TestMethod]
        public async Task OnlineReturnOrderNumberCanNotBeZero()
        {
            //Arrange
            ReturnBL onlineReturnBL = new ReturnBL();
            Return onlineReturn1 = new Return() { OrderNumber = 20, ProductNumber = 4, QuantityOfReturn = 5, Purpose = 0 };
            await onlineReturnBL.AddReturnBL(onlineReturn1);
            Return onlineReturn2 = new Return() { OnlineReturnID = onlineReturn1.OnlineReturnID, OrderNumber = 0, ProductNumber = 4, QuantityOfReturn = 5, Purpose = 0 };
            bool isUpdated = false;
            string errorMessage = null;

            //Act
            try
            {
                isUpdated = await onlineReturnBL.UpdateReturnBL(onlineReturn2);
            }
            catch (Exception ex)
            {
                isUpdated = false;
                errorMessage = ex.Message;
            }
            finally
            {
                //Assert
                Assert.IsFalse(isUpdated, errorMessage);
            }
        }

        /// <summary>
        /// OnlineReturn ProductNumber can't be Zero
        /// </summary>
        [TestMethod]
        public async Task OnlineReturnProductNumberCanNotBeZero()
        {
            //Arrange
            ReturnBL onlineReturnBL = new ReturnBL();
            Return onlineReturn1 = new Return() { OrderNumber = 20, ProductNumber = 4, QuantityOfReturn = 5, Purpose = 0 };
            await onlineReturnBL.AddReturnBL(onlineReturn1);
            Return onlineReturn2 = new Return() { OnlineReturnID = onlineReturn1.OnlineReturnID, OrderNumber = 20, ProductNumber = 0, QuantityOfReturn = 5, Purpose = 0 };
            bool isUpdated = false;
            string errorMessage = null;

            //Act
            try
            {
                isUpdated = await onlineReturnBL.UpdateReturnBL(onlineReturn2);
            }
            catch (Exception ex)
            {
                isUpdated = false;
                errorMessage = ex.Message;
            }
            finally
            {
                //Assert
                Assert.IsFalse(isUpdated, errorMessage);
            }
        }





        /// <summary>
        /// OnlineReturn QuantityOfReturn can't be Zero
        /// </summary>
        [TestMethod]
        public async Task OnlineReturnQuantityOfReturnCanNotBeZero()
        {
            //Arrange
            ReturnBL onlineReturnBL = new ReturnBL();
            Return onlineReturn1 = new Return() { OrderNumber = 20, ProductNumber = 4, QuantityOfReturn = 5, Purpose = 0 };
            await onlineReturnBL.AddReturnBL(onlineReturn1);
            Return onlineReturn2 = new Return() { OnlineReturnID = onlineReturn1.OnlineReturnID, OrderNumber = 20, ProductNumber = 4, QuantityOfReturn = 0, Purpose = 0 };
            bool isUpdated = false;
            string errorMessage = null;

            //Act
            try
            {
                isUpdated = await onlineReturnBL.UpdateReturnBL(onlineReturn2);
            }
            catch (Exception ex)
            {
                isUpdated = false;
                errorMessage = ex.Message;
            }
            finally
            {
                //Assert
                Assert.IsFalse(isUpdated, errorMessage);
            }
        }

        /// <summary>
        /// OnlineReturn Purpose can't be null
        /// </summary>
        [TestMethod]
        public async Task OnlineReturnPurposeCanNotBeNull()
        {
            //Arrange
            ReturnBL onlineReturnBL = new ReturnBL();
            Return onlineReturn1 = new Return() { OrderNumber = 20, ProductNumber = 4, QuantityOfReturn = 5, Purpose = 0 };
            await onlineReturnBL.AddReturnBL(onlineReturn1);
            Return onlineReturn2 = new Return() { OnlineReturnID = onlineReturn1.OnlineReturnID, OrderNumber = 20, ProductNumber = 4, QuantityOfReturn = 5, Purpose = PurposeOfReturn.UnsatiSfactoryProduct };
            bool isUpdated = false;
            string errorMessage = null;

            //Act
            try
            {
                isUpdated = await onlineReturnBL.UpdateReturnBL(onlineReturn2);
            }
            catch (Exception ex)
            {
                isUpdated = false;
                errorMessage = ex.Message;
            }
            finally
            {
                //Assert
                Assert.IsFalse(isUpdated, errorMessage);
            }
        }

        /// <summary>
        /// OnlineReturn Purpose can't be null
        /// </summary>
        [TestMethod]
        public async Task OnlineReturnPurposeCanNotBeNull1()
        {
            //Arrange
            ReturnBL onlineReturnBL = new ReturnBL();
            Return onlineReturn1 = new Return() { OrderNumber = 20, ProductNumber = 4, QuantityOfReturn = 5, Purpose = PurposeOfReturn.DefectiveProduct };
            await onlineReturnBL.AddReturnBL(onlineReturn1);
            Return onlineReturn2 = new Return() { OnlineReturnID = onlineReturn1.OnlineReturnID, OrderNumber = 20, ProductNumber = 4, QuantityOfReturn = 5, Purpose = PurposeOfReturn.UnsatiSfactoryProduct }; bool isUpdated = false;
            string errorMessage = null;

            //Act
            try
            {
                isUpdated = await onlineReturnBL.UpdateReturnBL(onlineReturn2);
            }
            catch (Exception ex)
            {
                isUpdated = false;
                errorMessage = ex.Message;
            }
            finally
            {
                //Assert
                Assert.IsFalse(isUpdated, errorMessage);
            }
        }



        /// <summary>
        /// OnlineReturn Purpose can't be null
        /// </summary>
        [TestMethod]
        public async Task OnlineReturnPurposeCanNotBeNull2()
        {
            //Arrange
            ReturnBL onlineReturnBL = new ReturnBL();
            Return onlineReturn1 = new Return() { OrderNumber = 20, ProductNumber = 4, QuantityOfReturn = 5, Purpose = PurposeOfReturn.DefectiveProduct };
            await onlineReturnBL.AddReturnBL(onlineReturn1);
            Return onlineReturn2 = new Return() { OnlineReturnID = onlineReturn1.OnlineReturnID, OrderNumber = 20, ProductNumber = 4, QuantityOfReturn = 5, Purpose = PurposeOfReturn.WrongProductOrdered };
            bool isUpdated = false;
            string errorMessage = null;

            //Act
            try
            {
                isUpdated = await onlineReturnBL.UpdateReturnBL(onlineReturn2);
            }
            catch (Exception ex)
            {
                isUpdated = false;
                errorMessage = ex.Message;
            }
            finally
            {
                //Assert
                Assert.IsFalse(isUpdated, errorMessage);
            }
        }

        /// <summary>
        /// OnlineReturn Purpose can't be null
        /// </summary>
        [TestMethod]
        public async Task OnlineReturnPurposeCanNotBeNull3()
        {
            //Arrange
            ReturnBL onlineReturnBL = new ReturnBL();
            Return onlineReturn1 = new Return() { OrderNumber = 20, ProductNumber = 4, QuantityOfReturn = 5, Purpose = PurposeOfReturn.DefectiveProduct };
            await onlineReturnBL.AddReturnBL(onlineReturn1);
            Return onlineReturn2 = new Return() { OnlineReturnID = onlineReturn1.OnlineReturnID, OrderNumber = 20, ProductNumber = 4, QuantityOfReturn = 5, Purpose = PurposeOfReturn.WrongProductShipped };
            bool isUpdated = false;
            string errorMessage = null;

            //Act
            try
            {
                isUpdated = await onlineReturnBL.UpdateReturnBL(onlineReturn2);
            }
            catch (Exception ex)
            {
                isUpdated = false;
                errorMessage = ex.Message;
            }
            finally
            {
                //Assert
                Assert.IsFalse(isUpdated, errorMessage);
            }
        }


        /// <summary>
        /// OnlineReturnID should Exist
        /// </summary>
        [TestMethod]
        public async Task OnlineReturnIDDoesNotExist()
        {
            //Arrange
            ReturnBL onlineReturnBL = new ReturnBL();
            Return onlineReturn1 = new Return() { OrderNumber = 20, ProductNumber = 4, QuantityOfReturn = 5, Purpose = 0 };
            await onlineReturnBL.AddReturnBL(onlineReturn1);
            Return onlineReturn2 = new Return() { OnlineReturnID = default(Guid), OrderNumber = 20, ProductNumber = 4, QuantityOfReturn = 5, Purpose = 0 };
            bool isUpdated = false;
            string errorMessage = null;

            //Act
            try
            {
                isUpdated = await onlineReturnBL.UpdateReturnBL(onlineReturn2);
            }
            catch (Exception ex)
            {
                isUpdated = false;
                errorMessage = ex.Message;
            }
            finally
            {
                //Assert
                Assert.IsFalse(isUpdated, errorMessage);
            }
        }


    }

    [TestClass]
    public class DeleteOnlineReturnBLTest
    {

        /// <summary>
        /// delete OnlineReturn to the Collection if it is valid.
        /// </summary>
        [TestMethod]
        public async Task DeleteValidOnlineReturnBL()
        {
            //Arrange
            ReturnBL onlineReturnBL = new ReturnBL();
            Return onlineReturn = new Return() { OrderNumber = 20, ProductNumber = 4, QuantityOfReturn = 5, Purpose = 0 };
            await onlineReturnBL.AddReturnBL(onlineReturn);
            bool isDeleted = false;
            string errorMessage = null;

            //Act
            try
            {
                isDeleted = await onlineReturnBL.DeleteOnlineReturnBL(onlineReturn.OnlineReturnID);
            }
            catch (Exception ex)
            {
                isDeleted = false;
                errorMessage = ex.Message;
            }
            finally
            {
                //Assert
                Assert.IsTrue(isDeleted, errorMessage);
            }
        }

        /// <summary>
        /// OnlineReturnID Should Exist
        /// </summary>
        [TestMethod]
        public async Task DeleteinValidOnlineReturnBL()
        {
            //Arrange
            ReturnBL onlineReturnBL = new ReturnBL();
            Return onlineReturn = new Return() { OrderNumber = 20, ProductNumber = 4, QuantityOfReturn = 5, Purpose = 0 };
            await onlineReturnBL.AddReturnBL(onlineReturn);
            bool isDeleted = false;
            string errorMessage = null;

            //Act
            try
            {
                isDeleted = await onlineReturnBL.DeleteOnlineReturnBL(default(Guid));
            }
            catch (Exception ex)
            {
                isDeleted = false;
                errorMessage = ex.Message;
            }
            finally
            {
                //Assert
                Assert.IsFalse(isDeleted, errorMessage);
            }
        }
    }

    [TestClass]
    public class GetOnlineReturnBLTest
    {
        /// <summary>
        /// Get All OnlineReturn in the Collection if it is valid.
        /// </summary>

        [TestMethod]
        public async Task GetAllValidOnlineReturns()
        {
            //Arrange
            ReturnBL onlineReturnBL = new ReturnBL();
            Return onlineReturn = new Return() { OrderNumber = 20, ProductNumber = 4, QuantityOfReturn = 5, Purpose = PurposeOfReturn.DefectiveProduct };
            await onlineReturnBL.AddReturnBL(onlineReturn);
            bool isDisplayed = false;
            string errorMessage = null;

            //Act
            try
            {
                List<Return> onlineReturnList = await onlineReturnBL.GetAllReturnsBL();
                if (onlineReturnList.Count > 0)
                {
                    isDisplayed = true;
                }
            }
            catch (Exception ex)
            {
                isDisplayed = false;
                errorMessage = ex.Message;
            }
            finally
            {
                //Assert
                Assert.IsTrue(isDisplayed, errorMessage);
            }
        }

        /// <summary>
        /// OnlineReturnList Can Not Ne Empty.
        /// </summary>
        /// 
        [TestMethod]
        public async Task OnlineReturnListCanNotBeEmpty()
        {
            //Arrange

            bool isDisplayed = false;
            string errorMessage = null;

            //Act
            try
            {
                ReturnBL onlineReturnBL = new ReturnBL();
                List<Return> onlineReturnList = await onlineReturnBL.GetAllReturnsBL();
                if (onlineReturnList.Count < 1)
                {
                    isDisplayed = true;
                }
            }
            catch (Exception ex)
            {
                isDisplayed = false;
                errorMessage = ex.Message;
            }
            finally
            {
                //Assert
                Assert.IsFalse(isDisplayed, errorMessage);
            }
        }





        /// <summary>
        /// Get All OnlineReturn in the Collection if it is valid.
        /// </summary>

        [TestMethod]
        public async Task GetOnlineReturnByOnlineReturnIDBL()
        {
            //Arrange
            ReturnBL onlineReturnBL = new ReturnBL();
            Return onlineReturn = new Return() { OrderNumber = 20, ProductNumber = 4, QuantityOfReturn = 5, Purpose = 0 };
            await onlineReturnBL.AddReturnBL(onlineReturn);
            bool isDisplayed = false;
            string errorMessage = null;

            //Act
            try
            {
                Return onlineReturn1 = await onlineReturnBL.GetOnlineReturnByOnlineReturnIDBL(onlineReturn.OnlineReturnID);
                if (onlineReturn.OnlineReturnID == onlineReturn1.OnlineReturnID)

                {
                    isDisplayed = true;
                }
            }
            catch (Exception ex)
            {
                isDisplayed = false;
                errorMessage = ex.Message;
            }
            finally
            {
                //Assert
                Assert.IsTrue(isDisplayed, errorMessage);
            }
        }

        /// <summary>
        /// OnlineReturnID should Exist
        /// </summary>
        [TestMethod]
        public async Task OnlineReturnIDDoesNotExist()
        {
            //Arrange
            ReturnBL onlineReturnBL = new ReturnBL();
            Return onlineReturn = new Return() { OrderNumber = 20, ProductNumber = 4, QuantityOfReturn = 5, Purpose = 0 };
            await onlineReturnBL.AddReturnBL(onlineReturn);
            bool isDisplayed = false;
            string errorMessage = null;

            //Act
            try
            {
                Return onlineReturn1 = await onlineReturnBL.GetOnlineReturnByOnlineReturnIDBL(default(Guid));
            }
            catch (Exception ex)
            {
                isDisplayed = false;
                errorMessage = ex.Message;
            }
            finally
            {
                //Assert
                Assert.IsFalse(isDisplayed, errorMessage);
            }
        }



        /// <summary>
        /// GetOnlineReturnByCustomerID to the Collection if it is isvalid.
        /// </summary>
        [TestMethod]
        public async Task GetOnlineReturnByCustomerIDBL()
        {
            //Arrang
            ReturnBL onlineReturnBL = new ReturnBL();
            Return onlineReturn = new Return() { OrderNumber = 20, ProductNumber = 4, QuantityOfReturn = 5, Purpose = 0 };
            await onlineReturnBL.AddReturnBL(onlineReturn);
            bool isDisplayed = false;
            string errorMessage = null;

            //Act
            try
            {
               List<Return> onlineReturn1 = await onlineReturnBL.GetOnlineReturnByCustomerIDBL(onlineReturn.CustomerID);
                if (onlineReturn1.Count>0 )

                {
                    isDisplayed = true;
                }
            }
            catch (Exception ex)
            {
                isDisplayed = false;
                errorMessage = ex.Message;
            }
            finally
            {
                //Assert
                Assert.IsTrue(isDisplayed, errorMessage);
            }
        }

        /// <summary>
        /// CustomerID should Exist
        /// </summary>
        [TestMethod]
        public async Task CustomerIDDoesNotExist()
        {
            //Arrange
            ReturnBL onlineReturnBL = new ReturnBL();
            Return onlineReturn = new Return() { OrderNumber = 20, ProductNumber = 4, QuantityOfReturn = 5, Purpose = 0 };
            await onlineReturnBL.AddReturnBL(onlineReturn);
            bool isDisplayed = false;
            string errorMessage = null;

            //Act
            try
            {
                List<Return> onlineReturnList1 = await onlineReturnBL.GetOnlineReturnByCustomerIDBL(default(Guid));
                if (onlineReturnList1.Count < 1)
                {
                    isDisplayed = true;
                }
            }
            catch (Exception ex)
            {
                isDisplayed = false;
                errorMessage = ex.Message;
            }
            finally
            {
                //Assert
                Assert.IsTrue(isDisplayed, errorMessage);
            }
        }



    //    /// <summary>
    //    /// Get OnlineReturnbyPurposeOfReturn in the Collection if it is valid.
    //    /// </summary>

    //    [TestMethod]
    //    public async Task GetOnlineReturnByPurposeBL()
    //    {
    //        //Arrange
    //        OnlineReturnBL onlineReturnBL = new OnlineReturnBL();
    //        OnlineReturn onlineReturn = new OnlineReturn() { OrderNumber = 20, ProductNumber = 4, QuantityOfReturn = 5, Purpose = PurposeOfReturn.WrongProductShipped };
    //        await onlineReturnBL.AddOnlineReturnBL(onlineReturn);
    //        bool isDisplayed = false;
    //        string errorMessage = null;

    //        //Act
    //        try
    //        {
    //            OnlineReturn onlineReturn1 = await onlineReturnBL.GetOnlineReturnByPurposeBL(onlineReturn.Purpose);
    //            if (onlineReturn.Purpose == onlineReturn1.Purpose)

    //            {
    //                isDisplayed = true;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            isDisplayed = false;
    //            errorMessage = ex.Message;
    //        }
    //        finally
    //        {
    //            //Assert
    //            Assert.IsTrue(isDisplayed, errorMessage);
    //        }
    //    }

    }

}
