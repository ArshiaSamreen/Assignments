using System;
using System.Collections.Generic;
using System.IO;
using Ecommerce.Ecommerce.Entities;
using Newtonsoft.Json;
using Ecommerce.Ecommerce.Helpers;

namespace Ecommerce.Ecommerce.Contracts.DALContracts
{
    public abstract class ReturnDALBase
    {       
        protected static List<Return> onlineReturnList = new List<Return>();
        private static readonly string fileName = "OnlineReturns.json";
                
        public abstract bool AddOnlineReturnDAL(Return newOnlineReturn);
        public abstract List<Return> GetAllOnlineReturnsDAL();
        public abstract Return GetOnlineReturnByOnlineReturnIDDAL(Guid searchOnlineReturnID);
        public abstract List<Return> GetOnlineReturnByCustomerIDDAL(Guid customerID);
        public abstract List<Return> GetOnlineReturnsByPurposeDAL(PurposeOfReturn purpose);
        public abstract bool UpdateOnlineReturnDAL(Return updateOnlineReturn);
        public abstract bool DeleteOnlineReturnDAL(Guid deleteOnlineReturnID);

        public static void Serialize()
        {
            string serializedJson = JsonConvert.SerializeObject(onlineReturnList);
            using (StreamWriter streamWriter = new StreamWriter(fileName))
            {
                streamWriter.Write(serializedJson);
                streamWriter.Close();
            }
        }

        public static void Deserialize()
        {
            string fileContent = string.Empty;
            if (!File.Exists(fileName))
                File.Create(fileName).Close();

            using (StreamReader streamReader = new StreamReader(fileName))
            {
                fileContent = streamReader.ReadToEnd();
                streamReader.Close();
                var systemUserListFromFile = JsonConvert.DeserializeObject<List<Return>>(fileContent);
                if (systemUserListFromFile != null)
                {
                    onlineReturnList = systemUserListFromFile;
                }
            }
        }

        static ReturnDALBase()
        {
            Deserialize();
        }
    }
}


