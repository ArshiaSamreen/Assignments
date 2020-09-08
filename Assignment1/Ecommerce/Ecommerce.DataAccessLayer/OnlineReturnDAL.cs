using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecommerce.Ecommerce.Contracts.DALContracts;
using Ecommerce.Ecommerce.Entities;
using Ecommerce.Ecommerce.Exceptions;
using Ecommerce.Ecommerce.Helpers;

namespace Ecommerce.Ecommerce.DataAccessLayer
{
    public class ReturnDAL : ReturnDALBase, IDisposable
    {
        public override bool AddOnlineReturnDAL(Return newOnlineReturn)
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

        public override List<Return> GetAllOnlineReturnsDAL()
        {
            return onlineReturnList;
        }

        public override Return GetOnlineReturnByOnlineReturnIDDAL(Guid searchOnlineReturnID)
        {
            Return matchingOnlineReturn = null;
            try
            {              
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

        public override List<Return> GetOnlineReturnsByPurposeDAL(PurposeOfReturn purpose)
        {
            List<Return> matchingOnlineReturns = new List<Return>();
            try
            {                
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

        public override List<Return> GetOnlineReturnByCustomerIDDAL(Guid customerID)
        {
            List<Return> matchingOnlineReturn = new List<Return>();
            try
            {               
                matchingOnlineReturn = onlineReturnList.FindAll(
                    (item) => { return item.CustomerID == customerID; }
                );
            }
            catch (Exception)
            {

                throw;
            }
            return matchingOnlineReturn;
        }

        public override bool UpdateOnlineReturnDAL(Return updateOnlineReturn)
        {
            bool onlineReturnUpdated = false;
            try
            {                
                Return matchingOnlineReturn = GetOnlineReturnByOnlineReturnIDDAL(updateOnlineReturn.OnlineReturnID);

                if (matchingOnlineReturn != null)
                {                    
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

        public override bool DeleteOnlineReturnDAL(Guid deleteOnlineReturnID)
        {
            bool onlineReturnDeleted = false;
            try
            {                
                Return matchingOnlineReturn = onlineReturnList.Find(
                    (item) => { return item.OnlineReturnID == deleteOnlineReturnID; }
                );

                if (matchingOnlineReturn != null)
                {                    
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

        public void Dispose()
        {
            //No unmanaged resources currently
        }
    }
}
