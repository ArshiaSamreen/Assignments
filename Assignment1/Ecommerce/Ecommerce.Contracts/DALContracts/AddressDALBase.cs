using System;
using System.Collections.Generic;
using System.IO;
using Ecommerce.Ecommerce.Entities;
using Newtonsoft.Json;

namespace Ecommerce.Ecommerce.Contracts.DALContracts
{
    public abstract class AddressDALBase
    {
        protected static List<Address> addressList = new List<Address>();
        private static readonly string fileName = "Addresss.json";
        public abstract bool AddAddressDAL(Address newAddress);
        public abstract List<Address> GetAllAddresssDAL();
        public abstract Address GetAddressByAddressIDDAL(Guid searchAddressID);
        public abstract List<Address> GetAddressByCustomerIDDAL(Guid customerID);
        public abstract bool UpdateAddressDAL(Address updateAddress);
        public abstract bool DeleteAddressDAL(Guid deleteAddressID);

        public static void Serialize()
        {
            string serializedJson = JsonConvert.SerializeObject(addressList);
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
                var addressListFromFile = JsonConvert.DeserializeObject<List<Address>>(fileContent);
                if (addressListFromFile != null)
                {
                    addressList = addressListFromFile;
                }
            }
        }
        
        static AddressDALBase()
        {
            Deserialize();
        }
    }
}
