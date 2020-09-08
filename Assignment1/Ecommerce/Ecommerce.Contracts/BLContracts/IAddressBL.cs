using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ecommerce.Ecommerce.Entities;

namespace Ecommerce.Ecommerce.Contracts.BLContracts
{
    public interface IAddressBL : IDisposable
    {
        Task<bool> AddAddressBL(Address newAddress);
        Task<List<Address>> GetAllAddresssBL();
        Task<Address> GetAddressByAddressIDBL(Guid searchAddressID);
        Task<List<Address>> GetAddressByCustomerIDBL( Guid CustomerID);
        Task<bool> UpdateAddressBL(Address updateAddress);
        Task<bool> DeleteAddressBL(Guid deleteAddressID);
    }
}


