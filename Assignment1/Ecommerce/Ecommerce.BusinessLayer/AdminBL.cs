using System;
using System.Threading.Tasks;
using Ecommerce.Ecommerce.Contracts.BLContracts;
using Ecommerce.Ecommerce.Contracts.DALContracts;
using Ecommerce.Ecommerce.DataAccessLayer;
using Ecommerce.Ecommerce.Entities;

namespace Ecommerce.Ecommerce.BusinessLayer
{
    public class AdminBL : BLBase<Admin>, IAdminBL, IDisposable
    {
        readonly AdminDALBase adminDAL;
        
        public async Task<Admin> GetAdminByAdminEmailBL(string email)
        {
            Admin matchingAdmin = null;
            try
            {
                await Task.Run(() =>
                {
                    matchingAdmin = adminDAL.GetAdminByAdminEmailDAL(email);
                });
            }
            catch (Exception)
            {
                throw;
            }
            return matchingAdmin;
        }
        public async Task<Admin> GetAdminByEmailAndPasswordBL(string email, string password)
        {
            Admin matchingAdmin = null;
            try
            {
                await Task.Run(() =>
                {
                    matchingAdmin = adminDAL.GetAdminByEmailAndPasswordDAL(email, password);
                });
            }
            catch (Exception)
            {
                throw;
            }
            return matchingAdmin;
        }
       
        public async Task<bool> UpdateAdminBL(Admin updateAdmin)
        {
            bool adminUpdated = false;
            try
            {
                if ((await Validate(updateAdmin)) && (await GetAdminByAdminEmailBL(updateAdmin.Email)) != null)
                {
                    this.adminDAL.UpdateAdminDAL(updateAdmin);
                    adminUpdated = true;
                    Serialize();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return adminUpdated;
        }

        public async Task<bool> UpdateAdminPasswordBL(Admin updateAdmin)
        {
            bool passwordUpdated = false;
            try
            {
                if ((await Validate(updateAdmin)) && (await GetAdminByAdminEmailBL(updateAdmin.Email)) != null)
                {
                    this.adminDAL.UpdateAdminPasswordDAL(updateAdmin);
                    passwordUpdated = true;
                    Serialize();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return passwordUpdated;
        }

        public AdminBL()
        {
            adminDAL = new AdminDAL();
        }

        public void Dispose()
        {
            ((AdminDAL)adminDAL).Dispose();
        }

        public void Serialize()
        {
            try
            {
                AdminDAL.Serialize();
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
                AdminDAL.Deserialize();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
