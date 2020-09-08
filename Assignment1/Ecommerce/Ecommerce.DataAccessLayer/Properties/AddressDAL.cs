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
    /// Contains data access layer methods for inserting, updating, deleting addresss from Addresss collection.
    /// </summary>
    public class AddressDAL : AddressDALBase, IDisposable
    {
        /// <summary>
        /// Adds new address to Addresss collection.
        /// </summary>
        /// <param name="newAddress">Contains the address details to be added.</param>
        /// <returns>Determinates whether the new address is added.</returns>
        public override bool AddAddressDAL(Address newAddress)
        {
            bool addressAdded = false;
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

        /// <summary>
        /// Gets all addresss from the collection.
        /// </summary>
        /// <returns>Returns list of all addresss.</returns>
        public override List<Address> GetAllAddresssDAL()
        {
            return addressList;
        }

        /// <summary>
        /// Gets address based on AddressID.
        /// </summary>
        /// <param name="searchAddressID">Represents AddressID to search.</param>
        /// <returns>Returns Address object.</returns>
        public override Address GetAddressByAddressIDDAL(Guid searchAddressID)
        {
            Address matchingAddress = null;
            try
            {
                //Find Address based on searchAddressID
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


        /// <summary>
        /// Gets address based on email.
        /// </summary>
        /// <param name="email">Represents Address's Email Address.</param>
        /// <returns>Returns Address object.</returns>
        public override List<Address> GetAddressByRetailerIDDAL(Guid RetailerID)
        {
            List<Address> matchingAddress = new List<Address>();
            try
            {
                //Find Address based on Email and Password
                matchingAddress = addressList.FindAll(
                    (item) => { return item.RetailerID == RetailerID; }
                );
            }
            catch (Exception)
            {
                throw;
            }
            return matchingAddress;
        }


        /// <summary>
        /// Updates address based on AddressID.
        /// </summary>
        /// <param name="updateAddress">Represents Address details including AddressID, AddressName etc.</param>
        /// <returns>Determinates whether the existing address is updated.</returns>
        public override bool UpdateAddressDAL(Address updateAddress)
        {
            bool addressUpdated = false;
            try
            {
                //Find Address based on AddressID
                Address matchingAddress = GetAddressByAddressIDDAL(updateAddress.AddressID);

                if (matchingAddress != null)
                {
                    //Update address details
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

        /// <summary>
        /// Deletes address based on AddressID.
        /// </summary>
        /// <param name="deleteAddressID">Represents AddressID to delete.</param>
        /// <returns>Determinates whether the existing address is updated.</returns>
        public override bool DeleteAddressDAL(Guid deleteAddressID)
        {
            bool addressDeleted = false;
            try
            {
                //Find Address based on searchAddressID
                Address matchingAddress = addressList.Find(
                    (item) => { return item.AddressID == deleteAddressID; }
                );

                if (matchingAddress != null)
                {
                    //Delete Address from the collection
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



        /// <summary>
        /// Clears unmanaged resources such as db connections or file streams.
        /// </summary>
        public void Dispose()
        {
            //No unmanaged resources currently
        }
    }
}



