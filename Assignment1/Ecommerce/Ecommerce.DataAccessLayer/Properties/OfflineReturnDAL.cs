using System;
using System.Collections.Generic;
using System.Text;
using Capgemini.Ecommerce.Entities;
using Capgemini.Ecommerce.Exceptions;
using Capgemini.Ecommerce.Contracts.DALContracts;
namespace Capgemini.Ecommerce.DataAccessLayer
{/// <summary>
 /// Contains data access layer methods for inserting, updating, deleting systemUsers from OfflineReturn collection.
 /// </summary>
    public class OfflineReturnDAL : OfflineReturnDALBase, IDisposable

    {
        public static List<OfflineReturn> OfflineReturnList1 = new List<OfflineReturn>();

        /// <summary>
        /// Adds new Offline return to OfflineReturn collection.
        /// </summary>
        /// <param name="newOfflineReturn">Contains the Offline returns details to be added.</param>
        /// <returns>Determinates whether the new Offline return is added.</returns>
        public bool AddOfflineReturnDAL(OfflineReturn newOfflineReturn)
        {
            bool OfflineReturnAdded = false;
            try
            {
                newOfflineReturn.OfflineReturnID = Guid.NewGuid();
                newOfflineReturn.DateOfOfflineReturn = DateTime.Now;

                OfflineReturnList1.Add(newOfflineReturn);
                OfflineReturnAdded = true;
            }
            catch (SystemException ex)
            {
                throw new OfflineReturnException(ex.Message);
            }
            return OfflineReturnAdded;

        }
        // <summary>
        /// Gets all offline returns from the collection.
        /// </summary>
        /// <returns>Returns list of all offline return.</returns>
        public List<OfflineReturn> GetAllOfflineReturnsDAL()
        {
            return OfflineReturnList1;
        }
        /// <summary>
        /// Gets Offline return based on OfflineReturnID.
        /// </summary>
        /// <param name="searchOfflineReturnID">Represents OfflineReturnID to search.</param>
        /// <returns>Returns SystemUser object.</returns>
        public OfflineReturn GetOfflineReturnByOfflineReturnIDDAL(Guid searchOfflineReturnID)
        {
            OfflineReturn matchingOfflineReturn = null;
            try
            {
                //Find SystemUser based on searchSystemUserID
                matchingOfflineReturn = OfflineReturnList1.Find(
                    (item) => { return item.OfflineReturnID == searchOfflineReturnID; }
                );
            }
            catch (Exception)
            {
                throw;
            }
            return matchingOfflineReturn;
        }



        public bool UpdateOfflineReturnDAL(OfflineReturn updateOfflineReturn)
        {
            bool OfflineReturnUpdated = false;
            try
            {
                for (int i = 0; i < OfflineReturnList1.Count; i++)
                {
                    if (OfflineReturnList1[i].OfflineReturnID == updateOfflineReturn.OfflineReturnID)
                    {
                        OfflineReturnList1[i] = updateOfflineReturn;

                        OfflineReturnUpdated = true;
                    }
                }
            }
            catch (SystemException ex)
            {
                throw new OfflineReturnException(ex.Message);
            }
            return OfflineReturnUpdated;

        }
        /// <summary>
        /// Deletes OfflineReturn based on OfflineReturnID.
        /// </summary>
        /// <param name="deleteOfflineReturnID">Represents OfflineReturnID to delete.</param>
        /// <returns>Determinates whether the existing OfflineReturn is deleted.</returns>
        public bool DeleteOfflineReturnDAL(Guid deleteOfflineReturnID)
        {
            bool OfflineReturnDeleted = false;
            try
            {
                //Find SystemUser based on searchSystemUserID
                OfflineReturn matchingOfflineReturn = OfflineReturnList1.Find(
                    (item) => { return item.OfflineReturnID == deleteOfflineReturnID; }
                );

                if (matchingOfflineReturn != null)
                {
                    //Delete SystemUser from the collection
                    OfflineReturnList1.Remove(matchingOfflineReturn);
                    OfflineReturnDeleted = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return OfflineReturnDeleted;
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
