using System;
using System.Collections.Generic;
using System.IO;
using Ecommerce.Ecommerce.Entities;
using Newtonsoft.Json;

namespace Ecommerce.Ecommerce.Contracts.DALContracts
{
    public abstract class CustomerDALBase
    {
        protected static List<Customer> customerList = new List<Customer>() 
        { 
            new Customer() 
            { 
                CustomerID = Guid.NewGuid(), Email = "customer@ecommerce.com", 
                CustomerName = "Customer", Password = "customer", 
                CreationDateTime = DateTime.Now, LastModifiedDateTime = DateTime.Now 
            } 
        };
        
        private static readonly string fileName = "Customers.json";

        public abstract (bool,Guid) AddCustomerDAL(Customer newCustomer);
        public abstract List<Customer> GetAllCustomersDAL();
        public abstract Customer GetCustomerByCustomerIDDAL(Guid searchCustomerID);
        public abstract List<Customer> GetCustomersByNameDAL(string customerName);
        public abstract Customer GetCustomerByEmailDAL(string email);
        public abstract Customer GetCustomerByEmailAndPasswordDAL(string email, string password);
        public abstract bool UpdateCustomerDAL(Customer updateCustomer);
        public abstract bool UpdateCustomerPasswordDAL(Customer updateCustomer);
        public abstract bool DeleteCustomerDAL(Guid deleteCustomerID);

        public static void Serialize()
        {
            string serializedJson = JsonConvert.SerializeObject(customerList);
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
                var customerListFromFile = JsonConvert.DeserializeObject<List<Customer>>(fileContent);
                if (customerListFromFile != null)
                {
                    customerList = customerListFromFile;
                }
            }
        }
        static CustomerDALBase()
        {
            Deserialize();
        }
    }
}
