using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static System.Console;
using Ecommerce.Ecommerce.BusinessLayer;
using Ecommerce.Ecommerce.Entities;
using Ecommerce.Ecommerce.Exceptions;
using Ecommerce.Ecommerce.Contracts.BLContracts;

namespace Ecommerce.Ecommerce.PresentationLayer
{
    public static class AddressPresentation
    {
        public static async Task<int> AddressPresentationMenu()
        {
            int choice = -2;
            using (IAddressBL addressBL = new AddressBL())
            {
                do
                {
                    CustomerBL customerBL = new CustomerBL();
                    Customer customer = await customerBL.GetCustomerByEmailBL(UserData.CurrentUser.Email);
                    List<Address> addresses = await addressBL.GetAddressByCustomerIDBL(customer.CustomerID);
                    WriteLine("\n***************ADDRESS***********\n");
                    WriteLine("Addresses:");
                    if (addresses != null && addresses.Count > 0)
                    {
                        WriteLine("#\tAddressLine 1\tLandMark\tCity\tState");
                        int count = 0;
                        foreach (var address in addresses)
                        {
                            count++;
                            WriteLine($"{count}\t{address.AddressLine1}\t{address.Landmark}\t{address.City}\t{address.State}");
                        }
                    }

                    WriteLine("\n1.Add Address");
                    WriteLine("2.Update Existing Address");
                    WriteLine("3.Delete Existing Address");
                    WriteLine("0.Back");
                    WriteLine("-1.Exit");
                    Write("Choice: ");

                    bool isValidChoice = int.TryParse(ReadLine(), out choice);
                    if (isValidChoice)
                    {
                        switch (choice)
                        {
                            case 1: 
                                await AddAddress(); 
                                break;
                            case 2: 
                                await UpdateAddress(); 
                                break;
                            case 3: 
                                await DeleteAddress(); 
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

        public static async Task AddAddress()
        {
            try
            {
                Address address = new Address();
                CustomerBL customerBL = new CustomerBL();
                Customer customer = await customerBL.GetCustomerByEmailBL(UserData.CurrentUser.Email);
                address.CustomerID = customer.CustomerID;
                Write("Address Line 1: ");
                address.AddressLine1 = ReadLine();
                Write("Address Line 2: ");
                address.AddressLine2 = ReadLine();
                Write("LandMark: ");
                address.Landmark = ReadLine();
                Write("City: ");
                address.City = ReadLine();
                Write("State: ");
                address.State = ReadLine();
                Write("PinCode: ");
                address.PinCode = ReadLine();
                
                using (AddressBL addressBL = new AddressBL())
                {
                    bool isAdded = await addressBL.AddAddressBL(address);
                    if (isAdded)
                    {
                        WriteLine("Address Added");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogException(ex);
                WriteLine(ex.Message);
            }
        }

        public static async Task UpdateAddress()
        {
            try
            {
                using (IAddressBL addressBL = new AddressBL())
                {                  
                    Write("Address #: ");
                    bool isNumberValid = int.TryParse(ReadLine(), out int number);
                    if (isNumberValid)
                    {
                        number--;
                        CustomerBL customerBL = new CustomerBL();
                        Customer customer = await customerBL.GetCustomerByEmailBL(UserData.CurrentUser.Email);
                        List<Address> addresses = await addressBL.GetAddressByCustomerIDBL(customer.CustomerID);
                        if (number <= addresses.Count - 1)
                        {
                            Address address = addresses[number];
                            Write("Address Line 1: ");
                            address.AddressLine1 = ReadLine();
                            Write("Address Line 2: ");
                            address.AddressLine2 = ReadLine();
                            Write("LandMark: ");
                            address.Landmark = ReadLine();
                            Write("City: ");
                            address.City = ReadLine();
                            Write("State: ");
                            address.State = ReadLine();
                            Write("PinCode: ");
                            address.PinCode = ReadLine();

                            bool isUpdated = await addressBL.UpdateAddressBL(address);
                            if (isUpdated)
                            {
                                WriteLine("Address Updated");
                            }
                        }
                        else
                        {
                            WriteLine($"Invalid Address #.\nPlease enter a number between 1 to {addresses.Count}");
                        }
                    }
                    else
                    {
                        WriteLine($"Invalid number.");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogException(ex);
                WriteLine(ex.Message);
            }

        }
        
        public static async Task DeleteAddress()
        {
            try
            {
                using (IAddressBL addressBL = new AddressBL())
                {
                                        Write("Address #: ");
                    bool isNumberValid = int.TryParse(ReadLine(), out int number);
                    if (isNumberValid)
                    {
                        number--;
                        CustomerBL customerBL = new CustomerBL();
                        Customer customer = await customerBL.GetCustomerByEmailBL(UserData.CurrentUser.Email);
                        List<Address> addresses = await addressBL.GetAddressByCustomerIDBL(customer.CustomerID);

                        Write("Are you sure? (Y/N): ");
                        string confirmation = ReadLine();
                        if (confirmation.Equals("Y", StringComparison.OrdinalIgnoreCase))
                        {
                            if (number <= addresses.Count - 1)
                            {
                                Address address = addresses[number];
                                bool isDeleted = await addressBL.DeleteAddressBL(address.AddressID);
                                if (isDeleted)
                                {
                                    WriteLine("Customer Address Deleted");
                                }
                            }
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
    }
}
