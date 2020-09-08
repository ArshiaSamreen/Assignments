using System;
using System.Collections.Generic;
using System.Text;
using Capgemini.Ecommerce.Contracts.DALContracts;
using Capgemini.Ecommerce.Entities;
using Capgemini.Ecommerce.Exceptions;

namespace Capgemini.Ecommerce.DataAccessLayer
{
    public class OfflineReturnDetailDAL : OfflineReturnDetailDALBase, IDisposable

    {
        public static List<OfflineReturnDetail> OfflineReturnDetailList1 = new List<OfflineReturnDetail>();

        /// <summary>
        /// Adds new Offline Order to OfflineReturnDetail collection.
        /// </summary>
        /// <param name="newOfflineReturnDetail">Contains the Offline Orders Detail to be added.</param>
        /// <returns>Determinates whether the new Offline Order is added.</returns>
        public bool AddOfflineReturnDetailDAL(OfflineReturnDetail newOfflineReturnDetail)
        {
            bool OfflineReturnDetailAdded = false;
            try
            {
                newOfflineReturnDetail.OfflineReturnDetailID = Guid.NewGuid();
                OfflineReturnDetailList1.Add(newOfflineReturnDetail);
                OfflineReturnDetailAdded = true;
            }
            catch (SystemException ex)
            {
                throw new OfflineOrderException(ex.Message);
            }
            return OfflineReturnDetailAdded;

        }
        // <summary>
        /// Gets all offline orders from the collection.
        /// </summary>
        /// <returns>Returns list of all offline order.</returns>
        public List<OfflineReturnDetail> GetAllOfflineReturnDetailsDAL()
        {
            return OfflineReturnDetailList1;
        }
        /// <summary>
        /// Gets Offline Order based on OfflineReturnDetailID.
        /// </summary>
        /// <param name="searchOfflineReturnDetailID">Represents OfflineReturnDetailID to search.</param>
        /// <returns>Returns SystemUser object.</returns>
        public OfflineReturnDetail GetOfflineReturnDetailByOfflineReturnDetailIDDAL(Guid searchOfflineReturnDetailID)
        {
            OfflineReturnDetail matchingOfflineReturnDetail = null;
            try
            {
                //Find SystemUser based on searchSystemUserID
                matchingOfflineReturnDetail = OfflineReturnDetailList1.Find(
                    (item) => { return item.OfflineReturnDetailID == searchOfflineReturnDetailID; }
                );
            }
            catch (Exception)
            {
                throw;
            }
            return matchingOfflineReturnDetail;
        }


        public bool UpdateOfflineReturnDetailDAL(OfflineReturnDetail updateOfflineReturnDetail)
        {
            bool OfflineReturnDetailUpdated = false;
            try
            {
                for (int i = 0; i < OfflineReturnDetailList1.Count; i++)
                {
                    if (OfflineReturnDetailList1[i].OfflineReturnDetailID == updateOfflineReturnDetail.OfflineReturnDetailID)
                    {
                        OfflineReturnDetailList1[i] = updateOfflineReturnDetail;

                        OfflineReturnDetailUpdated = true;
                    }
                }
            }
            catch (SystemException ex)
            {
                throw new OfflineReturnException(ex.Message);
            }
            return OfflineReturnDetailUpdated;

        }
        /// <summary>
        /// Deletes OfflineReturnDetail based on OfflineReturnDetailID.
        /// </summary>
        /// <param name="deleteOfflineReturnDetailID">Represents OfflineReturnDetailID to delete.</param>
        /// <returns>Determinates whether the existing OfflineReturnDetail is deleted.</returns>
        public bool DeleteOfflineReturnDetailDAL(Guid deleteOfflineReturnDetailID)
        {
            bool OfflineReturnDetailDeleted = false;
            try
            {
                //Find SystemUser based on searchSystemUserID
                OfflineReturnDetail matchingOfflineReturnDetail = OfflineReturnDetailList1.Find(
                    (item) => { return item.OfflineReturnDetailID == deleteOfflineReturnDetailID; }
                );

                if (matchingOfflineReturnDetail != null)
                {
                    //Delete SystemUser from the collection
                    OfflineReturnDetailList1.Remove(matchingOfflineReturnDetail);
                    OfflineReturnDetailDeleted = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return OfflineReturnDetailDeleted;
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
