using System;
using System.Threading.Tasks;
using Ecommerce.Ecommerce.Entities;

namespace Ecommerce.Ecommerce.Contracts.BLContracts
{
    public interface IAdminBL : IDisposable
    {
        Task<Admin> GetAdminByEmailAndPasswordBL(string email, string password);
        Task<bool> UpdateAdminBL(Admin updateAdmin);
        Task<bool> UpdateAdminPasswordBL(Admin updateAdmin);
        Task<Admin> GetAdminByAdminEmailBL(string Email);
    }
}