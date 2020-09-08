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
    /// Contains data access layer methods for inserting, updating, deleting salesPersons from SalesPersons collection.
    /// </summary>
    public class SalesPersonDAL : SalesPersonDALBase, IDisposable
    {
        /// <summary>
        /// Adds new salesPerson to SalesPersons collection.
        /// </summary>
        /// <param name="newSalesPerson">Contains the salesPerson details to be added.</param>
        /// <returns>Determinates whether the new salesPerson is added.</returns>
        public override bool AddSalesPersonDAL(SalesPerson newSalesPerson)
        {
            bool salesPersonAdded = false;
            try
            {
                newSalesPerson.SalesPersonID = Guid.NewGuid();
                newSalesPerson.CreationDateTime = DateTime.Now;
                newSalesPerson.LastModifiedDateTime = DateTime.Now;
                salesPersonList.Add(newSalesPerson);
                salesPersonAdded = true;
            }
            catch (Exception)
            {
                throw;
            }
            return salesPersonAdded;
        }

        /// <summary>
        /// Gets all salesPersons from the collection.
        /// </summary>
        /// <returns>Returns list of all salesPersons.</returns>
        public override List<SalesPerson> GetAllSalesPersonsDAL()
        {
            return salesPersonList;
        }

        /// <summary>
        /// Gets salesPerson based on SalesPersonID.
        /// </summary>
        /// <param name="searchSalesPersonID">Represents SalesPersonID to search.</param>
        /// <returns>Returns SalesPerson object.</returns>
        public override SalesPerson GetSalesPersonBySalesPersonIDDAL(Guid searchSalesPersonID)
        {
            SalesPerson matchingSalesPerson = null;
            try
            {
                //Find SalesPerson based on searchSalesPersonID
                matchingSalesPerson = salesPersonList.Find(
                    (item) => { return item.SalesPersonID == searchSalesPersonID; }
                );
            }
            catch (Exception)
            {
                throw;
            }
            return matchingSalesPerson;
        }

        /// <summary>
        /// Gets salesPerson based on SalesPersonName.
        /// </summary>
        /// <param name="salesPersonName">Represents SalesPersonName to search.</param>
        /// <returns>Returns SalesPerson object.</returns>
        public override List<SalesPerson> GetSalesPersonsByNameDAL(string salesPersonName)
        {
            List<SalesPerson> matchingSalesPersons = new List<SalesPerson>();
            try
            {
                //Find All SalesPersons based on salesPersonName
                matchingSalesPersons = salesPersonList.FindAll(
                    (item) => { return item.SalesPersonName.Equals(salesPersonName, StringComparison.OrdinalIgnoreCase); }
                );
            }
            catch (Exception)
            {
                throw;
            }
            return matchingSalesPersons;
        }

        /// <summary>
        /// Gets salesPerson based on email.
        /// </summary>
        /// <param name="email">Represents SalesPerson's Email Address.</param>
        /// <returns>Returns SalesPerson object.</returns>
        public override SalesPerson GetSalesPersonByEmailDAL(string email)
        {
            SalesPerson matchingSalesPerson = null;
            try
            {
                //Find SalesPerson based on Email and Password
                matchingSalesPerson = salesPersonList.Find(
                    (item) => { return item.Email.Equals(email); }
                );
            }
            catch (Exception)
            {
                throw;
            }
            return matchingSalesPerson;
        }

        /// <summary>
        /// Gets salesPerson based on Email and Password.
        /// </summary>
        /// <param name="email">Represents SalesPerson's Email Address.</param>
        /// <param name="password">Represents SalesPerson's Password.</param>
        /// <returns>Returns SalesPerson object.</returns>
        public override SalesPerson GetSalesPersonByEmailAndPasswordDAL(string email, string password)
        {
            SalesPerson matchingSalesPerson = null;
            try
            {
                //Find SalesPerson based on Email and Password
                matchingSalesPerson = salesPersonList.Find(
                    (item) => { return item.Email.Equals(email) && item.Password.Equals(password); }
                );
            }
            catch (Exception)
            {
                throw;
            }
            return matchingSalesPerson;
        }

        /// <summary>
        /// Updates salesPerson based on SalesPersonID.
        /// </summary>
        /// <param name="updateSalesPerson">Represents SalesPerson details including SalesPersonID, SalesPersonName etc.</param>
        /// <returns>Determinates whether the existing salesPerson is updated.</returns>
        public override bool UpdateSalesPersonDAL(SalesPerson updateSalesPerson)
        {
            bool salesPersonUpdated = false;
            try
            {
                //Find SalesPerson based on SalesPersonID
                SalesPerson matchingSalesPerson = GetSalesPersonBySalesPersonIDDAL(updateSalesPerson.SalesPersonID);

                if (matchingSalesPerson != null)
                {
                    //Update salesPerson details
                    ReflectionHelpers.CopyProperties(updateSalesPerson, matchingSalesPerson, new List<string>() { "SalesPersonName", "Email" });
                    matchingSalesPerson.LastModifiedDateTime = DateTime.Now;

                    salesPersonUpdated = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return salesPersonUpdated;
        }

        /// <summary>
        /// Deletes salesPerson based on SalesPersonID.
        /// </summary>
        /// <param name="deleteSalesPersonID">Represents SalesPersonID to delete.</param>
        /// <returns>Determinates whether the existing salesPerson is updated.</returns>
        public override bool DeleteSalesPersonDAL(Guid deleteSalesPersonID)
        {
            bool salesPersonDeleted = false;
            try
            {
                //Find SalesPerson based on searchSalesPersonID
                SalesPerson matchingSalesPerson = salesPersonList.Find(
                    (item) => { return item.SalesPersonID == deleteSalesPersonID; }
                );

                if (matchingSalesPerson != null)
                {
                    //Delete SalesPerson from the collection
                    salesPersonList.Remove(matchingSalesPerson);
                    salesPersonDeleted = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return salesPersonDeleted;
        }

        /// <summary>
        /// Updates salesPerson's password based on SalesPersonID.
        /// </summary>
        /// <param name="updateSalesPerson">Represents SalesPerson details including SalesPersonID, Password.</param>
        /// <returns>Determinates whether the existing salesPerson's password is updated.</returns>
        public override bool UpdateSalesPersonPasswordDAL(SalesPerson updateSalesPerson)
        {
            bool passwordUpdated = false;
            try
            {
                //Find SalesPerson based on SalesPersonID
                SalesPerson matchingSalesPerson = GetSalesPersonBySalesPersonIDDAL(updateSalesPerson.SalesPersonID);

                if (matchingSalesPerson != null)
                {
                    //Update salesPerson details
                    ReflectionHelpers.CopyProperties(updateSalesPerson, matchingSalesPerson, new List<string>() { "Password" });
                    matchingSalesPerson.LastModifiedDateTime = DateTime.Now;

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




