using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static System.Console;
using Ecommerce.Ecommerce.BusinessLayer;
using Ecommerce.Ecommerce.Entities;
using Ecommerce.Ecommerce.Exceptions;
using Ecommerce.Ecommerce.Contracts.BLContracts;
using Ecommerce.Presentation;
using Ecommerce.Ecommerce.Contracts;
using System.Linq;

namespace Ecommerce.Ecommerce.PresentationLayer
{
    public static class CustomerPresentation
    {
        public static async Task<int> CustomerUserMenu()
        {
            int choice = -2;
            using (ICustomerBL customerBL = new CustomerBL())
            {
                do
                {
                    WriteLine("\n\nWelcome " + ((Customer)UserData.CurrentUser).CustomerName + "!");
                    WriteLine("1.Initiate Order");
                    WriteLine("2.Cancel Order");
                    WriteLine("3.Return Order");
                    WriteLine("4.Update Account");
                    WriteLine("5.Change Password");
                    WriteLine("6.Manage Address");
                    WriteLine("7.Delete Account");
                    WriteLine("0.Logout");
                    WriteLine("-1.Exit");
                    Write("Choice: ");

                    bool isValidChoice = int.TryParse(ReadLine(), out choice);
                    if (isValidChoice)
                    {
                        switch (choice)
                        {
                            case 1: 
                                await InitiateOrder(); 
                                break;
                            case 2: 
                                await CancelCustomerOrder(); 
                                break;
                            case 3: 
                                await ReturnOnlineOrder(); 
                                break;
                            case 4: 
                                await UpdateCustomerAccount(); 
                                break;
                            case 5: 
                                await ChangeCustomerPassword(); 
                                break;
                            case 6: 
                                await ManageAddress(); 
                                break;
                            case 7: 
                                await DeleteCustomerAccount(); 
                                break;
                            case 0: 
                                break;
                            case -1: 
                                break;
                            default: 
                                WriteLine("Invalid Choice"); 
                                break;
                        }
                    }
                    else
                    {
                        choice = -2;
                    }
                } while (choice != 0 && choice != -1);
            }
            return choice;
        }

        public static async Task InitiateOrder()
        {
            try
            {
                using (IProductBL product = new ProductBL())
                {
                    await CustomerProductPresentation.CustomerProductMenu();
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogException(ex);
                WriteLine(ex.Message);
            }
        }

        public static async Task ReturnOnlineOrder()
        {
            try
            {
                using (IProductBL product = new ProductBL())
                {
                    await ReturnPresentation.ReturnMenu();
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogException(ex);
                WriteLine(ex.Message);
            }
        }

        public static async Task ManageAddress()
        {
            try
            {
                int internalvalue = await AddressPresentation.AddressPresentationMenu();
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogException(ex);
                WriteLine(ex.Message);
            }
        }

        public static async Task UpdateCustomerAccount()
        {
            try
            {
                using (ICustomerBL customerBL = new CustomerBL())
                {
                    Customer customer = await customerBL.GetCustomerByEmailBL(UserData.CurrentUser.Email);
                    Write("Name: ");
                    customer.CustomerName = ReadLine();
                    Write("Email: ");
                    customer.Email = ReadLine();
                    bool isUpdated = await customerBL.UpdateCustomerBL(customer);
                    if (isUpdated)
                    {
                        WriteLine(" Account Updated");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogException(ex);
                WriteLine(ex.Message);
            }
        }

        public static async Task DeleteCustomerAccount()
        {
            try
            {
                using (ICustomerBL customerBL = new CustomerBL())
                {
                    Write("Current Password: ");
                    string currentPassword = ReadLine();
                    Write("Are you sure? (Y/N): ");
                    string confirmation = ReadLine();
                    Customer customer = await customerBL.GetCustomerByEmailAndPasswordBL(UserData.CurrentUser.Email, currentPassword);
                    if (confirmation.Equals("Y", StringComparison.OrdinalIgnoreCase))
                    {
                        bool isDeleted = await customerBL.DeleteCustomerBL(customer.CustomerID);
                        if (isDeleted)
                        {
                            WriteLine("Customer Account Deleted");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogException(ex);
                WriteLine(ex.Message);
            }
        }

        public static async Task ChangeCustomerPassword()
        {
            try
            {
                using (ICustomerBL customerBL = new CustomerBL())
                {
                    Write("Current Password: ");
                    string currentPassword = ReadLine();

                    Customer existingCustomer = await customerBL.GetCustomerByEmailAndPasswordBL(UserData.CurrentUser.Email, currentPassword);
                    if (existingCustomer != null)
                    {
                        Write("New Password: ");
                        string newPassword = ReadLine();
                        Write("Confirm Password: ");
                        string confirmPassword = ReadLine();

                        if (newPassword.Equals(confirmPassword))
                        {
                            existingCustomer.Password = newPassword;
                            bool isUpdated = await customerBL.UpdateCustomerPasswordBL(existingCustomer);
                            if (isUpdated)
                            {
                                WriteLine("Customer Password Updated");
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

        public static async Task CancelCustomerOrder()
        {
            IProductBL productBL = new ProductBL();
            List<OrderDetail> matchingOrder = new List<OrderDetail>();
            WriteLine("Enter the order number which you want to cancel");
            double orderToBeCancelled = double.Parse(ReadLine());
            WriteLine("Entered Number is: " + orderToBeCancelled);
            try
            {
                using (IOrderBL orderBL = new OrderBL())
                {
                    try
                    {
                        Order order = await orderBL.GetOrderByOrderNumberBL(orderToBeCancelled);
                        using (IOrderDetailBL orderDetailBL = new OrderDetailBL())
                        {
                            matchingOrder = await orderDetailBL.GetOrderDetailsByOrderIDBL(order.OrderId);
                            int a = matchingOrder.Count();
                            WriteLine("No. of Products ordered: " + a);
                        }
                    }
                    catch (Exception)
                    {
                        WriteLine("Invalid OrderId");
                    }
                }
                if (matchingOrder.Count != 0)
                {
                    OrderDetailBL orderDetailBL = new OrderDetailBL();
                    if ((await orderDetailBL.UpdateOrderDeliveredStatusBL(matchingOrder[0].OrderId)))
                    {
                        int serialNo = 0;
                        WriteLine("Products in the order are ");
                        WriteLine("#\t\t ProductID\t\t ProductQuantityOrdered\t\t UnitProductPrice");
                        foreach (OrderDetail orderDetail in matchingOrder)
                        {
                            Product product = await productBL.GetProductByProductIDBL(orderDetail.ProductID);
                            serialNo++;
                            //Console.WriteLine(product.ProductName);
                            WriteLine($"{ serialNo}\t{ orderDetail.ProductID}\t\t{ orderDetail.ProductQuantityOrdered}\t\t{orderDetail.ProductPrice}");
                        }
                        Console.WriteLine("Enter The Product to be Cancelled");
                        int ProductToBeCancelled = int.Parse(Console.ReadLine());
                        if (ProductToBeCancelled <= matchingOrder.Count && ProductToBeCancelled > 0)
                        {
                            Console.WriteLine("Enter The Product Quantity to be Cancelled");
                            int quantityToBeCancelled = int.Parse(Console.ReadLine());
                            if (matchingOrder[ProductToBeCancelled - 1].ProductQuantityOrdered >= quantityToBeCancelled)
                            {
                                matchingOrder[ProductToBeCancelled - 1].ProductQuantityOrdered -= quantityToBeCancelled;
                                matchingOrder[ProductToBeCancelled - 1].TotalAmount -= matchingOrder[ProductToBeCancelled - 1].ProductPrice * quantityToBeCancelled;
                                Console.WriteLine("Total Refund Amount: " + (matchingOrder[ProductToBeCancelled - 1].ProductPrice * quantityToBeCancelled));
                                OrderDetailBL orderDetailBL1 = new OrderDetailBL();
                                await orderDetailBL1.UpdateOrderDetailsBL(matchingOrder[ProductToBeCancelled - 1]);
                                Console.WriteLine("Product Cancelled Succesfully");
                            }
                            else
                            {
                                Console.WriteLine("PRODUCT QUANTITY EXCEEDED");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Wrong Input");
                        }

                    }
                    else
                    {
                        Console.WriteLine("Order Can't be cancelled as it is delivered");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}