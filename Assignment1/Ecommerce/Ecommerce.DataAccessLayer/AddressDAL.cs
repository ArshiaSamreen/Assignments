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
    public class AddressDAL : AddressDALBase, IDisposable
    {
        public override bool AddAddressDAL(Address newAddress)
        {
            bool addressAdded;
            try
            {
                newAddress.AddressID = Guid.NewGuid();
                newAddress.CreationDateTime = DateTime.Now;
                newAddress.LastModifiedDateTime = DateTime.Now;
                addressList.Add(newAddress);
                addressAdded = true;
            }
            catch (Exception)
            {
                throw;
            }
            return addressAdded;
        }

        public override List<Address> GetAllAddresssDAL()
        {
            return addressList;
        }

        public override Address GetAddressByAddressIDDAL(Guid searchAddressID)
        {
            Address matchingAddress = null;
            try
            {
                matchingAddress = addressList.Find(
                    (item) => { return item.AddressID == searchAddressID; }
                );
            }
            catch (Exception)
            {
                throw;
            }
            return matchingAddress;
        }

        public override List<Address> GetAddressByCustomerIDDAL(Guid CustomerID)
        {
            List<Address> matchingAddress = new List<Address>();
            try
            {
                matchingAddress = addressList.FindAll(
                    (item) => { return item.CustomerID == CustomerID; }
                );
            }
            catch (Exception)
            {
                throw;
            }
            return matchingAddress;
        }

        public override bool UpdateAddressDAL(Address updateAddress)
        {
            bool addressUpdated = false;
            try
            {              
                Address matchingAddress = GetAddressByAddressIDDAL(updateAddress.AddressID);

                if (matchingAddress != null)
                {
                    ReflectionHelpers.CopyProperties(updateAddress, matchingAddress, new List<string>() { "AddressLine1", "AddressLine2", "Landmark", "City", "State", "PinCode" });
                    matchingAddress.LastModifiedDateTime = DateTime.Now;
                    addressUpdated = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return addressUpdated;
        }

        public override bool DeleteAddressDAL(Guid deleteAddressID)
        {
            bool addressDeleted = false;
            try
            {
                Address matchingAddress = addressList.Find(
                    (item) => { return item.AddressID == deleteAddressID; }
                );

                if (matchingAddress != null)
                {
                    addressList.Remove(matchingAddress);
                    addressDeleted = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return addressDeleted;
        }

        public void Dispose()
        {
            
        }
    }
}
