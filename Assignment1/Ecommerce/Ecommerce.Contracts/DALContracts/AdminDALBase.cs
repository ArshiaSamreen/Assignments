using System;
using System.Collections.Generic;
using System.IO;
using Ecommerce.Ecommerce.Entities;
using Newtonsoft.Json;

namespace Ecommerce.Ecommerce.Contracts.DALContracts
{
    public abstract class AdminDALBase
    {
        public static List<Admin> adminList = new List<Admin>()
        {
            new Admin() 
            { 
                AdminID = Guid.NewGuid(), Email = "admin@ecommerce.com", 
                AdminName = "Ecommerce", Password = "ecommerce", 
                CreationDateTime = DateTime.Now, LastModifiedDateTime = DateTime.Now 
            },
        };
        private static readonly string fileName = "Admin.json";
        
        public abstract Admin GetAdminByAdminEmailDAL(string Email);
        public abstract Admin GetAdminByEmailAndPasswordDAL(string email, string password);
        public abstract bool UpdateAdminDAL(Admin updateAdmin);
        public abstract bool UpdateAdminPasswordDAL(Admin updateAdmin);
        
        public static void Serialize()
        {
            string serializedJson = JsonConvert.SerializeObject(adminList);
            using (StreamWriter streamWriter = new StreamWriter(fileName))
            {
                streamWriter.Write(serializedJson);
                streamWriter.Close();
            }
        }

        public static void Deserialize()
        {
            if (!File.Exists(fileName))
                File.Create(fileName).Close();

            using (StreamReader streamReader = new StreamReader(fileName))
            {
                string fileContent = streamReader.ReadToEnd();
                streamReader.Close();
                var adminListFromFile = JsonConvert.DeserializeObject<List<Admin>>(fileContent);
                if (adminListFromFile != null)
                {
                    adminList = adminListFromFile;
                }
            }
        }      
    }
}