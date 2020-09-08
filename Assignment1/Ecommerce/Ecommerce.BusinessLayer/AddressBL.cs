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

namespace Ecommerce.Ecommerce.BusinessLayer
{
    public class AddressBL : BLBase<Address>, IAddressBL, IDisposable
    {
        readonly AddressDALBase addressDAL;
        
        protected async override Task<bool> Validate(Address entityObject)
        {           
            bool valid = await base.Validate(entityObject);
            return valid;
        }
                
        public async Task<bool> AddAddressBL(Address newAddress)
        {
            bool addressAdded = false;
            try
            {
                if (await Validate(newAddress))
                {
                    await Task.Run(() =>
                    {
                        this.addressDAL.AddAddressDAL(newAddress);
                        addressAdded = true;
                        Serialize();
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
            return addressAdded;
        }

        public async Task<List<Address>> GetAllAddresssBL()
        {
            List<Address> addresssList = null;
            try
            {
                await Task.Run(() =>
                {
                    addresssList = addressDAL.GetAllAddresssDAL();
                });
            }
            catch (Exception)
            {
                throw;
            }
            return addresssList;
        }

        public async Task<Address> GetAddressByAddressIDBL(Guid searchAddressID)
        {
            Address matchingAddress = null;
            try
            {
                await Task.Run(() =>
                {
                    matchingAddress = addressDAL.GetAddressByAddressIDDAL(searchAddressID);
                });
            }
            catch (Exception)
            {
                throw;
            }
            return matchingAddress;
        }

        public async Task<List<Address>> GetAddressByCustomerIDBL(Guid CustomerID)
        {
           List<Address> matchingAddress = null;
            try
            {
                await Task.Run(() =>
                {
                    matchingAddress = addressDAL.GetAddressByCustomerIDDAL(CustomerID);
                });
            }
            catch (Exception)
            {
                throw;
            }
            return matchingAddress;
        }

        public async Task<bool> UpdateAddressBL(Address updateAddress)
        {
            bool addressUpdated = false;
            try
            {
                if ((await Validate(updateAddress)) && (await GetAddressByAddressIDBL(updateAddress.AddressID)) != null)
                {
                    addressUpdated = addressDAL.UpdateAddressDAL(updateAddress);
                    Serialize();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return addressUpdated;
        }

        public async Task<bool> DeleteAddressBL(Guid deleteAddressID)
        {
            bool addressDeleted = false;
            try
            {
                await Task.Run(() =>
                {
                    addressDeleted = addressDAL.DeleteAddressDAL(deleteAddressID);
                    Serialize();
                });
            }
            catch (Exception)
            {
                throw;
            }
            return addressDeleted;
        }

        public AddressBL()
        {
            addressDAL = new AddressDAL();
        }

        public void Dispose()
        {
            ((AddressDAL)addressDAL).Dispose();
        }

        public void Serialize()
        {
            try
            {
                AddressDAL.Serialize();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Deserialize()
        {
            try
            {
                AddressDAL.Deserialize();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
