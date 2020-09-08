using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecommerce.Ecommerce.Contracts.DALContracts;
using Ecommerce.Ecommerce.Entities;
using Ecommerce.Ecommerce.Exceptions;
using Ecommerce.Ecommerce.Helpers;

namespace Ecommerce.Ecommerce.DataAccessLayer
{
    public class CustomerDAL : CustomerDALBase, IDisposable
    {
        public override (bool, Guid) AddCustomerDAL(Customer newCustomer)
        {
            Guid CustomerGuid;
            bool customerAdded;
            try
            {
                CustomerGuid = Guid.NewGuid();
                newCustomer.CustomerID = CustomerGuid;
                newCustomer.CreationDateTime = DateTime.Now;
                newCustomer.LastModifiedDateTime = DateTime.Now;
                customerList.Add(newCustomer);
                customerAdded = true;
            }
            catch (Exception)
            {
                throw;
            }
            return (customerAdded, CustomerGuid);
        }

        public override List<Customer> GetAllCustomersDAL()
        {
            return customerList;
        }

        public override Customer GetCustomerByCustomerIDDAL(Guid searchCustomerID)
        {
            Customer matchingCustomer = null;
            try
            {              
                matchingCustomer = customerList.Find(
                    (item) => { return item.CustomerID == searchCustomerID; }
                );
            }
            catch (Exception)
            {
                throw;
            }
            return matchingCustomer;
        }

        public override List<Customer> GetCustomersByNameDAL(string customerName)
        {
            List<Customer> matchingCustomers = new List<Customer>();
            try
            {              
                matchingCustomers = customerList.FindAll(
                    (item) => { return item.CustomerName.Equals(customerName, StringComparison.OrdinalIgnoreCase); }
                );
            }
            catch (Exception)
            {
                throw;
            }
            return matchingCustomers;
        }

        public override Customer GetCustomerByEmailDAL(string email)
        {
            Customer matchingCustomer = null;
            try
            {               
                matchingCustomer = customerList.Find(
                    (item) => { return item.Email.Equals(email); }
                );
            }
            catch (Exception)
            {
                throw;
            }
            return matchingCustomer;
        }

        public override Customer GetCustomerByEmailAndPasswordDAL(string email, string password)
        {
            Customer matchingCustomer = null;
            try
            {
                matchingCustomer = customerList.Find(
                    (item) => { return item.Email.Equals(email) && item.Password.Equals(password); }
                );
            }
            catch (Exception)
            {
                throw;
            }
            return matchingCustomer;
        }

        public override bool UpdateCustomerDAL(Customer updateCustomer)
        {
            bool customerUpdated = false;
            try
            {
                Customer matchingCustomer = GetCustomerByCustomerIDDAL(updateCustomer.CustomerID);

                if (matchingCustomer != null)
                {                   
                    ReflectionHelpers.CopyProperties(updateCustomer, matchingCustomer, new List<string>() { "CustomerName", "Email" });
                    matchingCustomer.LastModifiedDateTime = DateTime.Now;
                    customerUpdated = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return customerUpdated;
        }

        public override bool DeleteCustomerDAL(Guid deleteCustomerID)
        {
            bool customerDeleted = false;
            try
            {
                Customer matchingCustomer = customerList.Find(
                    (item) => { return item.CustomerID == deleteCustomerID; }
                );
                if (matchingCustomer != null)
                {
                    customerList.Remove(matchingCustomer);
                    customerDeleted = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return customerDeleted;
        }

        public override bool UpdateCustomerPasswordDAL(Customer updateCustomer)
        {
            bool passwordUpdated = false;
            try
            {               
                Customer matchingCustomer = GetCustomerByCustomerIDDAL(updateCustomer.CustomerID);
                if (matchingCustomer != null)
                {
                    ReflectionHelpers.CopyProperties(updateCustomer, matchingCustomer, new List<string>() { "Password" });
                    matchingCustomer.LastModifiedDateTime = DateTime.Now;
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
