using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ecommerce.Ecommerce.Entities;
using Ecommerce.Ecommerce.Helpers;

namespace Ecommerce.Ecommerce.Contracts.BLContracts
{
    public interface IReturnBL : IDisposable
    {
        Task<bool> AddReturnBL(Return newOnlineReturn);
        Task<List<Return>> GetAllReturnsBL();
        Task<Return> GetOnlineReturnByOnlineReturnIDBL(Guid searchOnlineReturnID);
        Task<List<Return>> GetOnlineReturnsByPurposeBL(PurposeOfReturn purpose);
        Task<bool> UpdateReturnBL(Return updateOnlineReturn);
        Task<bool> DeleteOnlineReturnBL(Guid deleteOnlinereturnID);
    }
}

