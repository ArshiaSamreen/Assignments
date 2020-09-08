using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Ecommerce.Ecommerce.Contracts.BLContracts;
using Ecommerce.Ecommerce.Contracts.DALContracts;
using Ecommerce.Ecommerce.DataAccessLayer;
using Ecommerce.Ecommerce.Entities;
using Ecommerce.Ecommerce.Exceptions;
using Ecommerce.Ecommerce.Helpers.ValidationAttributes;
using Ecommerce.Ecommerce.Helpers;

namespace Ecommerce.Ecommerce.BusinessLayer
{    
    public class ReturnBL : BLBase<Return>, IReturnBL, IDisposable
    {
        readonly ReturnDALBase returnDAL;      

        protected async override Task<bool> Validate(Return entityObject)
        {
            StringBuilder sb = new StringBuilder();
            bool valid = await base.Validate(entityObject);
            OrderBL iorderBL = new OrderBL();
            var existingObject = await iorderBL.GetOrderByOrderIDBL(entityObject.OrderID);
            if (existingObject == null)
            {
                valid = false;
                sb.Append(Environment.NewLine + $"OrderID {entityObject.OrderID} does not exists");
            }

            ////productID is unique
            //ProductBL iproductBL = new ProductBL();

            //var existingObject2 = await iproductBL.GetProductByProductIDBL(entityObject.ProductID);
            //if (existingObject2 == null)
            //{
            //    valid = false;
            //    sb.Append(Environment.NewLine + $"ProductID {entityObject.ProductID} already exists");
            //}

            if (valid == false)
               throw new EcommerceException(sb.ToString());
            return valid;
        }

        public async Task<bool> AddReturnBL(Return newOnlineReturn)
        {
            bool onlineReturnAdded = false;
            try
            {
                if (await Validate(newOnlineReturn))
                {
                    await Task.Run(() =>
                    {
                        this.returnDAL.AddOnlineReturnDAL(newOnlineReturn);
                        onlineReturnAdded = true;
                        Serialize();
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
            return onlineReturnAdded;
        }
                
        public async Task<List<Return>> GetAllReturnsBL()
        {
            List<Return> onlineReturnsList = null;
            try
            {
                await Task.Run(() =>
                {
                    onlineReturnsList = returnDAL.GetAllOnlineReturnsDAL();
                });
            }
            catch (Exception)
            {
                throw;
            }
            return onlineReturnsList;
        }
                
        public async Task<Return> GetOnlineReturnByOnlineReturnIDBL(Guid searchOnlineReturnID)
        {
            Return matchingOnlineReturn = null;
            try
            {
                await Task.Run(() =>
                {
                    matchingOnlineReturn = returnDAL.GetOnlineReturnByOnlineReturnIDDAL(searchOnlineReturnID);
                });
            }
            catch (Exception)
            {
                throw;
            }
            return matchingOnlineReturn;
        }

        public async Task<List<Return>> GetOnlineReturnsByPurposeBL(PurposeOfReturn purpose)
        {
            List<Return> matchingOnlineReturns = new List<Return>();
            try
            {
                await Task.Run(() =>
                {
                    matchingOnlineReturns = returnDAL.GetOnlineReturnsByPurposeDAL(purpose);
                });
            }
            catch (Exception)
            {
                throw;
            }
            return matchingOnlineReturns;
        }

        public async Task<List<Return>> GetOnlineReturnByCustomerIDBL(Guid customerID)
        {
            List<Return> matchingOnlineReturn = null;
            try
            {
                await Task.Run(() =>
                {
                    matchingOnlineReturn = returnDAL.GetOnlineReturnByCustomerIDDAL(customerID);
                });
            }
            catch (Exception)
            {
                throw;
            }
            return matchingOnlineReturn;
        }

        public async Task<bool> UpdateReturnBL(Return updateOnlineReturn)
        {
            bool onlineReturnUpdated = false;
            try
            {
                if ((await Validate(updateOnlineReturn)) && (await GetOnlineReturnByOnlineReturnIDBL(updateOnlineReturn.OnlineReturnID)) != null)
                {
                    this.returnDAL.UpdateOnlineReturnDAL(updateOnlineReturn);
                    onlineReturnUpdated = true;
                    Serialize();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return onlineReturnUpdated;
        }

        public async Task<bool> DeleteOnlineReturnBL(Guid deleteOnlineReturnID)
        {
            bool onlineReturnDeleted = false;
            try
            {
                await Task.Run(() =>
                {
                    onlineReturnDeleted = returnDAL.DeleteOnlineReturnDAL(deleteOnlineReturnID);
                    Serialize();
                });
            }
            catch (Exception)
            {
                throw;
            }
            return onlineReturnDeleted;
        }

        public ReturnBL()
        {
            returnDAL = new ReturnDAL();
        }
        public void Dispose()
        {
            ((ReturnDAL)returnDAL).Dispose();
        }

        public static void Serialize()
        {
            try
            {
                ReturnDAL.Serialize();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void Deserialize()
        {
            try
            {
                ReturnDAL.Deserialize();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
