using System;
using System.Collections.Generic;
using Ecommerce.Ecommerce.Contracts.DALContracts;
using Ecommerce.Ecommerce.Entities;
using Ecommerce.Ecommerce.Helpers;

namespace Ecommerce.Ecommerce.DataAccessLayer
{
    public class AdminDAL : AdminDALBase, IDisposable
    {
        public AdminDAL()
        {
            Serialize();
            Deserialize();
        }

        public override Admin GetAdminByAdminEmailDAL(string email)
        {
            Admin matchingAdmin = null;   
            try
            {
                matchingAdmin = adminList.Find(
                    (item) => { return item.Email == email; }
                );
            }
            catch (Exception)
            {
                throw;
            }
            return matchingAdmin;
        }

        public override Admin GetAdminByEmailAndPasswordDAL(string email, string password)
        {
            Admin matchingAdmin = null;
            try
            {
                matchingAdmin = adminList.Find(
                    (item) => { return item.Email.Equals(email) && item.Password.Equals(password); }
                );
            }
            catch (Exception)
            {
                throw;
            }
            return matchingAdmin;
        }

        public override bool UpdateAdminDAL(Admin updateAdmin)
        {
            bool adminUpdated = false;
            try
            {
                Admin matchingAdmin = GetAdminByAdminEmailDAL(updateAdmin.Email);
                if (matchingAdmin != null)
                {                  
                    ReflectionHelpers.CopyProperties(updateAdmin, matchingAdmin, new List<string>() { "AdminName", "Email" });
                    matchingAdmin.LastModifiedDateTime = DateTime.Now;
                    adminUpdated = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return adminUpdated;
        }

        public override bool UpdateAdminPasswordDAL(Admin updateAdmin)
        {
            bool passwordUpdated = false;
            try
            {
                Admin matchingAdmin = GetAdminByAdminEmailDAL(updateAdmin.Email);
                if (matchingAdmin != null)
                {
                    ReflectionHelpers.CopyProperties(updateAdmin, matchingAdmin, new List<string>() { "Password" });
                    matchingAdmin.LastModifiedDateTime = DateTime.Now;
                    passwordUpdated = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return passwordUpdated;
        }

        public void Dispose()
        {
         
        }
    }
}
