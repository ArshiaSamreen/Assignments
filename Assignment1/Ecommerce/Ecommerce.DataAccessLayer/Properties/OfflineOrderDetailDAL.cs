using System;
using System.Collections.Generic;
using System.Text;
using Capgemini.Ecommerce.Contracts.DALContracts;
using Capgemini.Ecommerce.Entities;
using Capgemini.Ecommerce.Exceptions;


namespace Capgemini.Ecommerce.DataAccessLayer
{   /// <summary>
    /// Contains data access layer methods for inserting, updating, deleting systemUsers from OfflineOrderDetail collection.
    /// </summary>
    public class OfflineOrderDetailDAL : OfflineOrderDetailDALBase, IDisposable

    {
        public static List<OfflineOrderDetail> OfflineOrderDetailList1 = new List<OfflineOrderDetail>();

        /// <summary>
        /// Adds new Offline Order to OfflineOrderDetail collection.
        /// </summary>
        /// <param name="newOfflineOrderDetail">Contains the Offline Orders Detail to be added.</param>
        /// <returns>Determinates whether the new Offline Order is added.</returns>
        public bool AddOfflineOrderDetailDAL(OfflineOrderDetail newOfflineOrderDetail)
        {
            bool OfflineOrderDetailAdded = false;
            try
            {
                newOfflineOrderDetail.OfflineOrderDetailID = Guid.NewGuid();
                OfflineOrderDetailList1.Add(newOfflineOrderDetail);
                OfflineOrderDetailAdded = true;
            }
            catch (SystemException ex)
            {
                throw new OfflineOrderException(ex.Message);
            }
            return OfflineOrderDetailAdded;

        }
        // <summary>
        /// Gets all offline orders from the collection.
        /// </summary>
        /// <returns>Returns list of all offline order.</returns>
        public List<OfflineOrderDetail> GetAllOfflineOrderDetailDAL()
        {
            return OfflineOrderDetailList1;
        }
        /// <summary>
        /// Gets Offline Order based on OfflineOrderDetailID.
        /// </summary>
        /// <param name="searchOfflineOrderDetailID">Represents OfflineOrderDetailID to search.</param>
        /// <returns>Returns SystemUser object.</returns>
        public OfflineOrderDetail GetOfflineOrderDetailByOfflineOrderDetailIDDAL(Guid searchOfflineOrderDetailID)
        {
            OfflineOrderDetail matchingOfflineOrderDetail = null;
            try
            {
                //Find SystemUser based on searchSystemUserID
                matchingOfflineOrderDetail = OfflineOrderDetailList1.Find(
                    (item) => { return item.OfflineOrderDetailID == searchOfflineOrderDetailID; }
                );
            }
            catch (Exception)
            {
                throw;
            }
            return matchingOfflineOrderDetail;
        }

        public List<OfflineOrderDetail> GetOfflineOrderDetailByOfflineOrderIDDAL(Guid offlineOrderID)
        {
            List<OfflineOrderDetail> matchingOfflineOrderDetails = new List<OfflineOrderDetail>();
            try
            {
                //Find SystemUser based on searchSystemUserID
                matchingOfflineOrderDetails = OfflineOrderDetailList.FindAll(
                    (item) => { return item.OfflineOrderID == offlineOrderID; }
                );
            }
            catch (SystemException ex)
            {
                throw new OfflineOrderException(ex.Message);
            }
            return matchingOfflineOrderDetails;
        }


        public bool UpdateOfflineOrderDetailDAL(OfflineOrderDetail updateOfflineOrderDetail)
        {
            bool OfflineOrderDetailUpdated = false;
            try
            {
                for (int i = 0; i < OfflineOrderDetailList1.Count; i++)
                {
                    if (OfflineOrderDetailList1[i].OfflineOrderDetailID == updateOfflineOrderDetail.OfflineOrderDetailID)
                    {
                        OfflineOrderDetailList1[i] = updateOfflineOrderDetail;

                        OfflineOrderDetailUpdated = true;
                    }
                }
            }
            catch (SystemException ex)
            {
                throw new OfflineOrderException(ex.Message);
            }
            return OfflineOrderDetailUpdated;

        }
        /// <summary>
        /// Deletes OfflineOrderDetail based on OfflineOrderDetailID.
        /// </summary>
        /// <param name="deleteOfflineOrderDetailID">Represents OfflineOrderDetailID to delete.</param>
        /// <returns>Determinates whether the existing OfflineOrderDetail is deleted.</returns>
        public bool DeleteOfflineOrderDetailDAL(Guid deleteOfflineOrderDetailID)
        {
            bool OfflineOrderDetailDeleted = false;
            try
            {
                //Find SystemUser based on searchSystemUserID
                OfflineOrderDetail matchingOfflineOrderDetail = OfflineOrderDetailList1.Find(
                    (item) => { return item.OfflineOrderDetailID == deleteOfflineOrderDetailID; }
                );

                if (matchingOfflineOrderDetail != null)
                {
                    //Delete SystemUser from the collection
                    OfflineOrderDetailList1.Remove(matchingOfflineOrderDetail);
                    OfflineOrderDetailDeleted = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return OfflineOrderDetailDeleted;
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