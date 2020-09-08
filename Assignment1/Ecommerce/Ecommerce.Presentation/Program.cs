using System;
using System.Threading.Tasks;
using static System.Console;
using Ecommerce.Ecommerce.BusinessLayer;
using Ecommerce.Ecommerce.Exceptions;
using Ecommerce.Ecommerce.Helpers;
using Ecommerce.Ecommerce.Entities;
using Ecommerce.Ecommerce.Contracts.BLContracts;

namespace Ecommerce.Ecommerce.PresentationLayer
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            try
            {
                int choice;
                do
                {
                    int internalChoice = 0;
                    WriteLine("*****************ECOMMERCE MANAGEMENT SYSTEM********************");
                    WriteLine("1.Existing User\n2.Customer Registration\n3.Exit");
                    WriteLine("Enter your choice");
                    bool isValidChoice = int.TryParse(ReadLine(), out choice);
                    if (isValidChoice)
                    {
                        switch (choice)
                        {
                            case 1:
                                do
                                {
                                    (UserType userType, IUser currentUser) = await ShowLoginScreen();
                                    UserData.CurrentUser = currentUser;
                                    UserData.CurrentUserType = userType;
                                    if (userType == UserType.Admin)
                                    {
                                        internalChoice = await AdminPresentation.AdminUserMenu();
                                    }
                                    else if (userType == UserType.Customer)
                                    {
                                        internalChoice = await CustomerPresentation.CustomerUserMenu();
                                    }
                                    else if (userType == UserType.Anonymous)
                                    {
                                        internalChoice = -2;
                                    }
                                } while (internalChoice==-2);
                                break;
                            case 2:
                                await AddCustomer();
                                break;
                            case 3:
                                break;
                            default:
                                WriteLine("Invalid choice");
                                break;   
                        }
                    }
                    else
                    {
                        WriteLine("Choice should be a number");
                    }
                    WriteLine("Thank you! Visit again!");
                } while (choice!=3);
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogException(ex);
                WriteLine(ex.Message);
            }
            ReadKey();
        }

        static async Task<(UserType, IUser)> ShowLoginScreen()
        {
            string email, password;
            WriteLine("************LOGIN**********");
            Write("Email: ");
            email = ReadLine();
            Write("Password: ");
            password = null;
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                if ((key.Key != ConsoleKey.Backspace) && (key.Key != ConsoleKey.Enter))
                {
                    password += key.KeyChar;
                    Write("*");
                }
                else
                {
                    Write("\b");
                }
            }while (key.Key != ConsoleKey.Enter);
            
            using (IAdminBL adminBL = new AdminBL())
            {
                Admin admin = await adminBL.GetAdminByEmailAndPasswordBL(email, password);
                if (admin != null)
                {
                    return (UserType.Admin, admin);
                }
            }
            using (ICustomerBL CustomerBL = new CustomerBL())
            {
                Customer customer = await CustomerBL.GetCustomerByEmailAndPasswordBL(email, password);
                if (customer != null)
                {
                    return (UserType.Customer, customer);
                }
            }
            WriteLine("Invalid Email or Password. Please try again...");
            return (UserType.Anonymous, null);
        }

        private static async Task AddCustomer()
        {
            try
            {
                Customer newCustomer = new Customer();               
                WriteLine("Enter Customer Name :");
                newCustomer.CustomerID = default;
                newCustomer.CustomerName = Console.ReadLine();
                WriteLine("Enter Phone Number :");
                newCustomer.CustomerMobile = Console.ReadLine();
                WriteLine("Enter Customer's Email");
                newCustomer.Email = Console.ReadLine();
                WriteLine("Enter Customer's Password");
                newCustomer.Password = Console.ReadLine();

                bool customerAdded;
                Guid newCustomerGuid;
                CustomerBL cbl = new CustomerBL();
                (customerAdded,newCustomerGuid)  = await cbl.AddCustomerBL(newCustomer);
                if (customerAdded)
                {
                    Console.WriteLine("Customer Added");
                    Console.WriteLine("Your User id is " + newCustomer.Email);
                }
                else
                    Console.WriteLine("Customer Not Added");
            }
            catch (EcommerceException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}