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
    /// Contains data access layer methods for inserting, updating, deleting onlineReturn from OnlineReturn collection.
    /// </summary>
    public class OnlineReturnDAL : OnlineReturnDALBase, IDisposable
    {
        /// <summary>
        /// Adds new onlineReturn to OnlineReturn collection.
        /// </summary>
        /// <param name="newOnlineReturn">Contains the onlineReturn details to be added.</param>
        /// <returns>Determinates whether the new OnlineReturn is added.</returns>
        public override bool AddOnlineReturnDAL(OnlineReturn newOnlineReturn)
        {
            bool onlineReturnAdded = false;
            try
            {
                newOnlineReturn.OnlineReturnID = Guid.NewGuid();
                newOnlineReturn.CreationDateTime = DateTime.Now;
                newOnlineReturn.LastModifiedDateTime = DateTime.Now;
                onlineReturnList.Add(newOnlineReturn);
                onlineReturnAdded = true;
            }
            catch (Exception)
            {
                throw;
            }
            return onlineReturnAdded;
        }

        /// <summary>
        /// Gets all onlineReturn from the collection.
        /// </summary>
        /// <returns>Returns list of all onlineReturn.</returns>
        public override List<OnlineReturn> GetAllOnlineReturnsDAL()
        {
            return onlineReturnList;
        }

        /// <summary>
        /// Gets onlineReturn based on OnlineReturnID.
        /// </summary>
        /// <param name="searchOnlineReturnID">Represents OnlineReturnID to search.</param>
        /// <returns>Returns onlineReturn object.</returns>
        public override OnlineReturn GetOnlineReturnByOnlineReturnIDDAL(Guid searchOnlineReturnID)
        {
            OnlineReturn matchingOnlineReturn = null;
            try
            {
                //Find OnlineReturn based on searchOnlineReturnID
                matchingOnlineReturn = onlineReturnList.Find
                (
                    (item) => { return item.OnlineReturnID == searchOnlineReturnID; }
                );
            }
            catch (Exception)
            {
                throw;
            }
            return matchingOnlineReturn;
        }

        /// <summary>
        /// Gets onlineReturn based on Purpose.
        /// </summary>
        /// <param name="Purpose">Represents Purpose to search.</param>
        /// <returns>Returns OnlineReturn object.</returns>
        public override List<OnlineReturn> GetOnlineReturnsByPurposeDAL(PurposeOfReturn purpose)
        {
            List<OnlineReturn> matchingOnlineReturns = new List<OnlineReturn>();
            try
            {
                //Find All OnlineReturns based on purpose
                matchingOnlineReturns = onlineReturnList.FindAll(
                    (item) => { return item.Purpose == purpose; }
                );
            }
            catch (Exception)
            {
                throw;
            }
            return matchingOnlineReturns;
        }

        /// <summary>
        /// Updates Order based on OrderID.
        /// </summary>
        /// <param name="OnlineReturn">Represents Order details like OrderID</param>
        /// <returns>Determinates whether the existing Order is updated.</returns>

        public override List<OnlineReturn> GetOnlineReturnByRetailerIDDAL(Guid retailerID)
        {
            List<OnlineReturn> matchingOnlineReturn = new List<OnlineReturn>();
            try
            {
                //Find all order Based on Customer Name
                matchingOnlineReturn = onlineReturnList.FindAll(
                    (item) => { return item.RetailerID == retailerID; }
                );
            }
            catch (Exception)
            {

                throw;
            }
            return matchingOnlineReturn;
        }



        /// <summary>
        /// Updates onllineReturn based on OnlineReturnID.
        /// </summary>
        /// <param name="updateOnlineReturn">Represents OnlineReturn details including OnlineReturnID, PurposeOfReturn etc.</param>
        /// <returns>Determinates whether the existing onlineReturn is updated.</returns>
        public override bool UpdateOnlineReturnDAL(OnlineReturn updateOnlineReturn)
        {
            bool onlineReturnUpdated = false;
            try
            {
                //Find OnlineReturn based on OnlineReturnID
                OnlineReturn matchingOnlineReturn = GetOnlineReturnByOnlineReturnIDDAL(updateOnlineReturn.OnlineReturnID);

                if (matchingOnlineReturn != null)
                {
                    //Update onlineReturn details
                    ReflectionHelpers.CopyProperties(updateOnlineReturn, matchingOnlineReturn, new List<string>() { "PurposeOfReturn", "OrderID", "ProductID", "NoOfReturn", "Email" });
                    matchingOnlineReturn.LastModifiedDateTime = DateTime.Now;

                    onlineReturnUpdated = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return onlineReturnUpdated;
        }

        /// <summary>
        /// Deletes onlineReturn based on OnlineReturnID.
        /// </summary>
        /// <param name="deleteOnlineReturnID">Represents OnlineReturnID to delete.</param>
        /// <returns>Determinates whether the existing onlineReturn is updated.</returns>
        public override bool DeleteOnlineReturnDAL(Guid deleteOnlineReturnID)
        {
            bool onlineReturnDeleted = false;
            try
            {
                //Find OnlineReturn based on searchOnlineReturnID
                OnlineReturn matchingOnlineReturn = onlineReturnList.Find(
                    (item) => { return item.OnlineReturnID == deleteOnlineReturnID; }
                );

                if (matchingOnlineReturn != null)
                {
                    //Delete OnlineReturn from the collection
                    onlineReturnList.Remove(matchingOnlineReturn);
                    onlineReturnDeleted = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return onlineReturnDeleted;
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
