using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Ecommerce.Contracts.BLContracts;
using Ecommerce.Ecommerce.Contracts.DALContracts;
using Ecommerce.Ecommerce.DataAccessLayer;
using Ecommerce.Ecommerce.Entities;
using Ecommerce.Ecommerce.Exceptions;

namespace Ecommerce.Ecommerce.BusinessLayer
{
    public class CustomerBL : BLBase<Customer>, ICustomerBL, IDisposable
    {
        readonly CustomerDALBase customerDAL;
        
        protected async override Task<bool> Validate(Customer entityObject)
        {            
            StringBuilder sb = new StringBuilder();
            bool valid = await base.Validate(entityObject);
            var existingObject = await GetCustomerByEmailBL(entityObject.Email);
            if (existingObject != null && existingObject?.CustomerID != entityObject.CustomerID)
            {
                valid = false;
                sb.Append(Environment.NewLine + $"Email {entityObject.Email} already exists");
            }

            if (valid == false)
                throw new EcommerceException(sb.ToString());
            return valid;
        }

        public async Task<(bool, Guid)> AddCustomerBL(Customer newCustomer)
        {
            bool customerAdded = false;
            Guid CustomerGuid = default;

            try
            {
                if (await Validate(newCustomer))
                {
                    await Task.Run(() =>
                    {
                        (customerAdded, CustomerGuid) = customerDAL.AddCustomerDAL(newCustomer);                       
                        Serialize();
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
            return (customerAdded, CustomerGuid);
        }

        public async Task<List<Customer>> GetAllCustomersBL()
        {
            List<Customer> customersList = null;
            try
            {
                await Task.Run(() =>
                {
                    customersList = customerDAL.GetAllCustomersDAL();
                });
            }
            catch (Exception)
            {
                throw;
            }
            return customersList;
        }
                
        public async Task<Customer> GetCustomerByCustomerIDBL(Guid searchCustomerID)
        {
            Customer matchingCustomer = null;
            try
            {
                await Task.Run(() =>
                {
                    matchingCustomer = customerDAL.GetCustomerByCustomerIDDAL(searchCustomerID);
                });
            }
            catch (Exception)
            {
                throw;
            }
            return matchingCustomer;
        }

        public async Task<List<Customer>> GetCustomersByNameBL(string customerName)
        {
            List<Customer> matchingCustomers = new List<Customer>();
            try
            {
                await Task.Run(() =>
                {
                    matchingCustomers = customerDAL.GetCustomersByNameDAL(customerName);
                });
            }
            catch (Exception)
            {
                throw;
            }
            return matchingCustomers;
        }

        public async Task<Customer> GetCustomerByEmailBL(string email)
        {
            Customer matchingCustomer = null;
            try
            {
                await Task.Run(() =>
                {
                    matchingCustomer = customerDAL.GetCustomerByEmailDAL(email);
                });
            }
            catch (Exception)
            {
                throw;
            }
            return matchingCustomer;
        }

        public async Task<Customer> GetCustomerByEmailAndPasswordBL(string email, string password)
        {
            Customer matchingCustomer = null;
            try
            {
                await Task.Run(() =>
                {
                    matchingCustomer = customerDAL.GetCustomerByEmailAndPasswordDAL(email, password);
                });
            }
            catch (Exception)
            {
                throw;
            }
            return matchingCustomer;
        }

        public async Task<bool> UpdateCustomerBL(Customer updateCustomer)
        {
            bool customerUpdated = false;
            try
            {
                if ((await Validate(updateCustomer)) && (await GetCustomerByCustomerIDBL(updateCustomer.CustomerID)) != null)
                {
                    customerDAL.UpdateCustomerDAL(updateCustomer);
                    customerUpdated = true;
                    Serialize();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return customerUpdated;
        }

        public async Task<bool> DeleteCustomerBL(Guid deleteCustomerID)
        {
            bool customerDeleted = false;
            try
            {
                await Task.Run(() =>
                {
                    customerDeleted = customerDAL.DeleteCustomerDAL(deleteCustomerID);
                    Serialize();
                });
            }
            catch (Exception)
            {
                throw;
            }
            return customerDeleted;
        }

        public async Task<bool> UpdateCustomerPasswordBL(Customer updateCustomer)
        {
            bool passwordUpdated = false;
            try
            {
                if ((await Validate(updateCustomer)) && (await GetCustomerByCustomerIDBL(updateCustomer.CustomerID)) != null)
                {
                    customerDAL.UpdateCustomerPasswordDAL(updateCustomer);
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

        public async Task<CustomerReport> GetCustomerReportByRetailIDBL(Guid CustomerID)
        {
            CustomerReport customerReport = new CustomerReport
            {
                CustomerID = CustomerID
            };            
            CustomerBL customerBL = new CustomerBL();           
            Customer customer = await customerBL.GetCustomerByCustomerIDBL(customerReport.CustomerID);
            customerReport.CustomerName = customer.CustomerName;             
            OrderBL order = new OrderBL();
            List<Order> orderList = await order.GetOrdersByCustomerIDBL(CustomerID);
            foreach (Order item in orderList)
            {
                customerReport.CustomerSalesCount++;
                customerReport.CustomerSalesAmount += item.OrderAmount;
            }
            return customerReport;
        }

        public CustomerBL()
        {
            customerDAL = new CustomerDAL();
        }
        public void Dispose()
        {
            ((CustomerDAL)customerDAL).Dispose();
        }
        public void Serialize()
        {
            try
            {
                CustomerDAL.Serialize();
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
                CustomerDAL.Deserialize();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
