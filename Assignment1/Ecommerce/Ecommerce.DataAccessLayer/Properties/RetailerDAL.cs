using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Capgemini.Ecommerce.Contracts.DALContracts;
using Capgemini.Ecommerce.Entities;
using Capgemini.Ecommerce.Exceptions;
using Capgemini.Ecommerce.Helpers;

namespace Capgemini.Ecommerce.DataAccessLayer
{
    /// <summary>
    /// Contains data access layer methods for inserting, updating, deleting retailers from Retailers collection.
    /// </summary>
    public class RetailerDAL : RetailerDALBase, IDisposable
    {
        /// <summary>
        /// Adds new customer to Retailers collection.
        /// </summary>
        /// <param name="newRetailer">Contains the customer details to be added.</param>
        /// <returns>Determinates whether the new customer is added.</returns>
        public override bool AddRetailerDAL(Customer newRetailer)
        {
            bool retailerAdded = false;
            try
            {
                newRetailer.RetailerID = Guid.NewGuid();
                newRetailer.CreationDateTime = DateTime.Now;
                newRetailer.LastModifiedDateTime = DateTime.Now;
                retailerList.Add(newRetailer);
                retailerAdded = true;
            }
            catch (Exception)
            {
                throw;
            }
            return retailerAdded;
        }

        /// <summary>
        /// Gets all retailers from the collection.
        /// </summary>
        /// <returns>Returns list of all retailers.</returns>
        public override List<Customer> GetAllRetailersDAL()
        {
            return retailerList;
        }

        /// <summary>
        /// Gets customer based on RetailerID.
        /// </summary>
        /// <param name="searchRetailerID">Represents RetailerID to search.</param>
        /// <returns>Returns Customer object.</returns>
        public override Customer GetRetailerByRetailerIDDAL(Guid searchRetailerID)
        {
            Customer matchingRetailer = null;
            try
            {
                //Find Customer based on searchRetailerID
                matchingRetailer = retailerList.Find(
                    (item) => { return item.RetailerID == searchRetailerID; }
                );
            }
            catch (Exception)
            {
                throw;
            }
            return matchingRetailer;
        }

        /// <summary>
        /// Gets customer based on RetailerName.
        /// </summary>
        /// <param name="retailerName">Represents RetailerName to search.</param>
        /// <returns>Returns Customer object.</returns>
        public override List<Customer> GetRetailersByNameDAL(string retailerName)
        {
            List<Customer> matchingRetailers = new List<Customer>();
            try
            {
                //Find All Retailers based on retailerName
                matchingRetailers = retailerList.FindAll(
                    (item) => { return item.RetailerName.Equals(retailerName, StringComparison.OrdinalIgnoreCase); }
                );
            }
            catch (Exception)
            {
                throw;
            }
            return matchingRetailers;
        }

        /// <summary>
        /// Gets customer based on email.
        /// </summary>
        /// <param name="email">Represents Customer's Email Address.</param>
        /// <returns>Returns Customer object.</returns>
        public override Customer GetRetailerByEmailDAL(string email)
        {
            Customer matchingRetailer = null;
            try
            {
                //Find Customer based on Email and Password
                matchingRetailer = retailerList.Find(
                    (item) => { return item.Email.Equals(email); }
                );
            }
            catch (Exception)
            {
                throw;
            }
            return matchingRetailer;
        }

        /// <summary>
        /// Gets customer based on Email and Password.
        /// </summary>
        /// <param name="email">Represents Customer's Email Address.</param>
        /// <param name="password">Represents Customer's Password.</param>
        /// <returns>Returns Customer object.</returns>
        public override Customer GetRetailerByEmailAndPasswordDAL(string email, string password)
        {
            Customer matchingRetailer = null;
            try
            {
                //Find Customer based on Email and Password
                matchingRetailer = retailerList.Find(
                    (item) => { return item.Email.Equals(email) && item.Password.Equals(password); }
                );
            }
            catch (Exception)
            {
                throw;
            }
            return matchingRetailer;
        }

        /// <summary>
        /// Updates customer based on RetailerID.
        /// </summary>
        /// <param name="updateRetailer">Represents Customer details including RetailerID, RetailerName etc.</param>
        /// <returns>Determinates whether the existing customer is updated.</returns>
        public override bool UpdateRetailerDAL(Customer updateRetailer)
        {
            bool retailerUpdated = false;
            try
            {
                //Find Customer based on RetailerID
                Customer matchingRetailer = GetRetailerByRetailerIDDAL(updateRetailer.RetailerID);

                if (matchingRetailer != null)
                {
                    //Update customer details
                    ReflectionHelpers.CopyProperties(updateRetailer, matchingRetailer, new List<string>() { "RetailerName", "Email" });
                    matchingRetailer.LastModifiedDateTime = DateTime.Now;

                    retailerUpdated = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return retailerUpdated;
        }

        /// <summary>
        /// Deletes customer based on RetailerID.
        /// </summary>
        /// <param name="deleteRetailerID">Represents RetailerID to delete.</param>
        /// <returns>Determinates whether the existing customer is updated.</returns>
        public override bool DeleteRetailerDAL(Guid deleteRetailerID)
        {
            bool retailerDeleted = false;
            try
            {
                //Find Customer based on searchRetailerID
                Customer matchingRetailer = retailerList.Find(
                    (item) => { return item.RetailerID == deleteRetailerID; }
                );

                if (matchingRetailer != null)
                {
                    //Delete Customer from the collection
                    retailerList.Remove(matchingRetailer);
                    retailerDeleted = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return retailerDeleted;
        }

        /// <summary>
        /// Updates customer's password based on RetailerID.
        /// </summary>
        /// <param name="updateRetailer">Represents Customer details including RetailerID, Password.</param>
        /// <returns>Determinates whether the existing customer's password is updated.</returns>
        public override bool UpdateRetailerPasswordDAL(Customer updateRetailer)
        {
            bool passwordUpdated = false;
            try
            {
                //Find Customer based on RetailerID
                Customer matchingRetailer = GetRetailerByRetailerIDDAL(updateRetailer.RetailerID);

                if (matchingRetailer != null)
                {
                    //Update customer details
                    ReflectionHelpers.CopyProperties(updateRetailer, matchingRetailer, new List<string>() { "Password" });
                    matchingRetailer.LastModifiedDateTime = DateTime.Now;

                    passwordUpdated = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return passwordUpdated;
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



