using System;
using System.Threading.Tasks;
using Ecommerce.Ecommerce.BusinessLayer;
using Ecommerce.Ecommerce.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ecommerce.Ecommerce.UnitTests
{
    //[TestClass]
    //public class AdminBLTest
    //{
    //    [TestMethod]
    //    public async Task GetAdminByEmailAndPasswordBLTest()
    //    {
    //        //Arrange
    //        AdminBL adminBL = new AdminBL();

    //        //Act
    //        Admin admin = await adminBL.GetAdminByEmailAndPasswordBL("admin@Ecommerce.com", "manager");

    //        //Assert
    //        Assert.AreEqual(admin.AdminName, "Admin");
    //    }
    //}
    [TestClass]
    public class UpdateAdminBLTest
    {
        /// <summary>
        /// Add Admin to the Collection if it is valid.
        /// </summary>
        [TestMethod]
        public async Task UpdateValidAdmin()
        {
            //Arrange
            AdminBL adminBL = new AdminBL();
            Admin admin = new Admin() { AdminName = "Ecommerce", Password = "ecommerce", Email = "admin@Ecommerce.com" };
            bool isAdded = false;
            string errorMessage = null;

            //Act
            try
            {
                isAdded = await adminBL.UpdateAdminBL(admin);
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
        /// Admin Name can't be null
        /// </summary>
        [TestMethod]
        public async Task AdminNameCanNotBeNull()
        {
            //Arrange
            AdminBL AdminBL = new AdminBL();
            Admin Admin = new Admin() { AdminName = null, Password = "Smith123#", Email = "smith@gmail.com" };
            bool isAdded = false;
            string errorMessage = null;

            //Act
            try
            {
                isAdded = await AdminBL.UpdateAdminBL(Admin);
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
        /// Admin Email can't be null
        /// </summary>
        [TestMethod]
        public async Task AdminEmailCanNotBeNull()
        {
            //Arrange
            AdminBL adminBL = new AdminBL();
            Admin admin = new Admin() { AdminName = "John", Password = "John123#", Email = null };
            bool isAdded = false;
            string errorMessage = null;

            //Act
            try
            {
                isAdded = await adminBL.UpdateAdminBL(admin);
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
        /// AdminName should contain at least two characters
        /// </summary>
        [TestMethod]
        public async Task AdminNameShouldContainAtLeastTwoCharacters()
        {
            //Arrange
            AdminBL adminBL = new AdminBL();
            Admin admin = new Admin() { AdminName = "J", Password = "John123#", Email = "john@gmail.com" };
            bool isAdded = false;
            string errorMessage = null;

            //Act
            try
            {
                isAdded = await adminBL.UpdateAdminBL(admin);
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
        /// Email should be a valid email as per regular expression
        /// </summary>
        [TestMethod]
        public async Task AdminEmailRegExp()
        {
            //Arrange
            AdminBL adminBL = new AdminBL();
            Admin admin = new Admin() { AdminName = "John", Password = "John123#", Email = "john" };
            bool isAdded = false;
            string errorMessage = null;

            //Act
            try
            {
                isAdded = await adminBL.UpdateAdminBL(admin);
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