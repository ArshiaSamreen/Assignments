using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static System.Console;
using Ecommerce.Ecommerce.BusinessLayer;
using Ecommerce.Ecommerce.Entities;
using Ecommerce.Ecommerce.Exceptions;
using Ecommerce.Ecommerce.Contracts.BLContracts;
using Ecommerce.Ecommerce.Helpers;

namespace Ecommerce.Ecommerce.PresentationLayer
{
    public static class AdminPresentation
    {
        public static async Task<int> AdminUserMenu()
        {
            int choice = -2;
            using (IAdminBL AdminBL = new AdminBL())
            {
                do
                {
                    WriteLine("\nWelcome Admin " + ((Admin)UserData.CurrentUser).AdminName + "!");
                    WriteLine("");
                    WriteLine("1.View Customer Profile");
                    WriteLine("2.View Customer Reports");
                    WriteLine("3.Update Admin Profile ");
                    WriteLine("4.Change Password");
                    WriteLine("5.Logout");
                    WriteLine("6.Add New Product");
                    WriteLine("-1.Exit");
                    Write("Choice: ");

                    bool isValidChoice = int.TryParse(ReadLine(), out choice);
                    if (isValidChoice)
                    {
                        switch (choice)
                        {
                            case 1: 
                                await ViewCustomersProfile(); 
                                break;
                            case 2: 
                                await ViewCustomerReports(); 
                                break;
                            case 3: 
                                await UpdateAdmin(); 
                                break;
                            case 4: 
                                await ChangeAdminPassword(); 
                                break;
                            case 5: 
                                break;
                            case 6: 
                                await AddProduct(); 
                                break;
                            case -1: 
                                break;
                            default: 
                                WriteLine("Invalid Choice"); break;
                        }
                    }
                    else
                    {
                        choice = -2;
                    }
                } while (choice != 10 && choice != -1);
            }
            return choice;
        }

        public static async Task ChangeAdminPassword()
        {
            try
            {
                using (IAdminBL adminBL = new AdminBL())
                {
                    Write("Current Password: ");
                    string currentPassword = ReadLine();

                    Admin existingAdmin = await adminBL.GetAdminByEmailAndPasswordBL(UserData.CurrentUser.Email, currentPassword);
                    if (existingAdmin != null)
                    {
                        Write("New Password: ");
                        string newPassword = ReadLine();
                        Write("Confirm Password: ");
                        string confirmPassword = ReadLine();

                        if (newPassword.Equals(confirmPassword))
                        {
                            existingAdmin.Password = newPassword;                         
                            bool isUpdated = await adminBL.UpdateAdminPasswordBL(existingAdmin);
                            if (isUpdated)
                            {
                                WriteLine("Admin Password Updated");
                            }
                        }
                        else
                        {
                            WriteLine($"New Password and Confirm Password doesn't match");
                        }
                    }
                    else
                    {
                        WriteLine($"Current Password doesn't match.");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogException(ex);
                WriteLine(ex.Message);
            }
        }

        public static async Task UpdateAdmin()
        {
            try
            {
                using (IAdminBL adminBL = new AdminBL())
                {
                    Admin admin = await adminBL.GetAdminByAdminEmailBL(UserData.CurrentUser.Email);
                    Write("Name: ");
                    admin.AdminName = ReadLine();
                    Write("Email: ");
                    admin.Email = ReadLine();
                    bool isUpdated = await adminBL.UpdateAdminBL(admin);
                    if (isUpdated)
                    {
                        WriteLine(" Account Updated");
                    }
                }
            }
            catch (EcommerceException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
 
        public static async Task ViewCustomerReports()
        {
            CustomerBL customerBL = new CustomerBL();
            List<Customer> customers = await customerBL.GetAllCustomersBL();
            List<CustomerReport> customerreports = new List<CustomerReport>();
            CustomerReport item;
            foreach (var customer in customers)
            {
                item = await customerBL.GetCustomerReportByRetailIDBL(customer.CustomerID);
                customerreports.Add(item);
            }
            WriteLine("#\tCustomer Name\t no.of orders placed\t Total amount spent on orders");
            int serialNo = 0;
            foreach (var report in customerreports)
            {
                serialNo++;
                WriteLine($"{serialNo}\t{report.CustomerName}\t{report.CustomerSalesCount}\t{report.CustomerSalesAmount}");
            }
        }
 
        public static async Task AddProduct()
        {
            try
            {
                Product product = new Product();
                Write("Product Name: ");
                product.ProductName = ReadLine();
                Write("Product Category: ");
                product.Category = (Category)int.Parse(ReadLine());
                Write("Product Price: ");
                product.ProductPrice = double.Parse(ReadLine());
                Write("Product Color: ");
                product.ProductColor = ReadLine();
                Write("Product Size: ");
                product.ProductSize = ReadLine();
                Write("Product Material: ");
                product.ProductMaterial = ReadLine();

                using (IProductBL productBL = new ProductBL())
                {
                    bool isAdded = await productBL.AddProductBL(product);
                    if (isAdded)
                    {
                        WriteLine("Product Added!");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogException(ex);
                WriteLine(ex.Message);
            }
        }

        public static async Task ViewCustomersProfile()
        {
            CustomerBL customerBL = new CustomerBL();
            List<Customer> customers = await customerBL.GetAllCustomersBL();
            WriteLine("#\tCustomer Name\t Retialer Email\tCustomer Phone no.");
            int serialNo = 0;
            foreach (var customer in customers)
            {
                serialNo++;
                WriteLine($"{serialNo}\t{customer.CustomerName}\t{customer.Email}\t{customer.CustomerMobile}");
            }
        }
    }
}
