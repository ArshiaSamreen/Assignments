using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static System.Console;
using Ecommerce.Ecommerce.BusinessLayer;
using Ecommerce.Ecommerce.Entities;
using Ecommerce.Ecommerce.Exceptions;
using Ecommerce.Ecommerce.Contracts.BLContracts;
using Ecommerce.Ecommerce.Contracts;

namespace Ecommerce.Ecommerce.PresentationLayer
{
    public static class ReturnPresentation
    {
        public static async Task<int> ReturnMenu()
        {
            int choice;
            do
            {
                WriteLine("\n***************Online Return Menu ***********");
                WriteLine("\t1.View Online Returns");
                WriteLine("\t2.Add Online Return");
                WriteLine("\t3.Update Online Return");
                WriteLine("\t4.Delete Online Return");
                WriteLine("0. Logout");
                WriteLine("-1. Exit");
                WriteLine("***********************************************");
                Write("Choice: ");
                bool isValidChoice = int.TryParse(ReadLine(), out choice);
                if (isValidChoice)
                {
                    switch (choice)
                    {
                        case 1: 
                            await ViewReturns(); 
                            break;
                        case 2: 
                            await AddReturn(); 
                            break;
                        case 3: 
                            await UpdateOnlineReturn(); 
                            break;
                        case 4: 
                            await DeleteReturn(); 
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
            } while (choice != 0 || choice != -1);
            return choice;
        }

        public static async Task ViewReturns()
        {
            try
            {
                using (IReturnBL ReturnBL = new ReturnBL())
                {
                    List<Return> onlineReturns = await ReturnBL.GetAllReturnsBL();
                    WriteLine("Returns:");
                    if (onlineReturns != null && onlineReturns.Count > 0)
                    {
                        WriteLine("#\tPurpose\tQuantityOfReturn\\tCreated\tModified");
                        int serialNo = 0;
                        foreach (var onlineReturn in onlineReturns)
                        {
                            serialNo++;
                            WriteLine($"{serialNo}\t{onlineReturn.Purpose}\t{onlineReturn.QuantityOfReturn}\t{onlineReturn.CreationDateTime}\t{onlineReturn.LastModifiedDateTime}");
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

        public static async Task AddReturn()
        {
            Return onlineReturn = new Return();
            List<OrderDetail> matchingOrder = new List<OrderDetail>();
            WriteLine("Enter the order number which you want to return");
            double orderToBeReturned = double.Parse(Console.ReadLine());
            WriteLine("Entered Number is: " + orderToBeReturned);
            try
            {
                using (IOrderBL orderBL = new OrderBL())
                {
                    Order order = await orderBL.GetOrderByOrderNumberBL(orderToBeReturned);
                    onlineReturn.OrderID = order.OrderId;
                    using (IOrderDetailBL orderDetailBL = new OrderDetailBL())
                    {
                        matchingOrder = await orderDetailBL.GetOrderDetailsByOrderIDBL(order.OrderId);
                    }
                }

                if (matchingOrder.Count != 0)
                {
                    int orderNo = 0;
                    WriteLine("Products in the order are ");
                    foreach (var orderDetail in matchingOrder)
                    {
                        orderNo++;
                        WriteLine("#\tProductID\t ProductQuantityOrdered\t ProductPrice\t TotalAmount");
                        WriteLine($"{orderNo}\t{orderDetail.ProductID}\t{orderDetail.ProductQuantityOrdered}\t{orderDetail.ProductPrice}\t{orderDetail.TotalAmount}");
                    }
                    WriteLine("Enter The Product Number to be Returned");
                    int ProductToBeReturned = int.Parse(ReadLine());
                    if (ProductToBeReturned <= matchingOrder.Count && ProductToBeReturned > 0)
                    {
                        WriteLine("Enter The Product Serial No. to be Returned");
                        Write("Product  #: ");
                        bool isNumberValid = int.TryParse(ReadLine(), out int serialNo);
                        if (isNumberValid)
                        {
                            serialNo--;
                            if (serialNo <= matchingOrder.Count - 1)
                            {
                                OrderDetail orderDetail = matchingOrder[serialNo];
                                onlineReturn.ProductID = orderDetail.ProductID;
                            }
                        }
                        Console.WriteLine("Enter The Quantity to be Returned");
                        int QuantityOfReturn = int.Parse(Console.ReadLine());
                        IOrderDetail orderDetail1 = new OrderDetail();
                        onlineReturn.QuantityOfReturn = QuantityOfReturn;

                        foreach (var orderDetail in matchingOrder)
                        {
                            orderDetail1 = matchingOrder.Find(
                           (item) => { return item.ProductQuantityOrdered == orderDetail.ProductQuantityOrdered; });
                        }

                        if (QuantityOfReturn <= orderDetail1.ProductQuantityOrdered)
                        {
                            WriteLine("Purpose of Return:\n1.  UnsatiSfactoryProduct\n2. WrongProductShipped\n3.  WrongProductOrdered\n4. DefectiveProduct  ");
                            bool isPurposeValid = int.TryParse(ReadLine(), out int purpose);
                            if (isPurposeValid)
                            {
                                if (purpose == 1)
                                {
                                    onlineReturn.Purpose = Helpers.PurposeOfReturn.UnsatiSfactoryProduct;
                                }
                                else if (purpose == 2)
                                {
                                    onlineReturn.Purpose = Helpers.PurposeOfReturn.WrongProductShipped;
                                }
                                else if (purpose == 3)
                                {
                                    onlineReturn.Purpose = Helpers.PurposeOfReturn.WrongProductOrdered;
                                }
                                else if (purpose == 4)
                                {
                                    onlineReturn.Purpose = Helpers.PurposeOfReturn.DefectiveProduct;
                                }
                                else
                                {
                                    WriteLine("Invalid Option Selected ");
                                }

                                Write("Are you sure? (Y/N): ");
                                string confirmation = ReadLine();
                                if (confirmation.Equals("Y", StringComparison.OrdinalIgnoreCase))
                                {
                                    using (IReturnBL onlineReturnBL = new ReturnBL())
                                    {
                                        bool confirmed = await onlineReturnBL.AddReturnBL(onlineReturn);

                                        if (confirmed)
                                        {
                                            matchingOrder[ProductToBeReturned - 1].ProductQuantityOrdered -= QuantityOfReturn;
                                            matchingOrder[ProductToBeReturned - 1].TotalAmount -= matchingOrder[ProductToBeReturned - 1].ProductPrice *QuantityOfReturn;
                                            WriteLine("Total Return Amount: " + (matchingOrder[ProductToBeReturned - 1].ProductPrice * QuantityOfReturn));                                          
                                            WriteLine("OnlineReturn Confirmed");
                                            WriteLine("OnlineReturnID is" + "  " + onlineReturn.OnlineReturnID);
                                        }
                                        else
                                        {
                                            WriteLine("Data Not Serialized");
                                        }
                                    }
                                }
                                else
                                {
                                    WriteLine(" Not Confirmed");
                                }
                            }
                            else
                            {
                                WriteLine(" Purpose of return not valid");
                            }
                        }
                        else
                        {
                            WriteLine("Invalid QuantityOfReturn");
                        }
                    }
                    else
                    {
                        WriteLine("Wrong Input");
                    }
                }
                else
                {
                    Console.WriteLine("OrderID not Found");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task UpdateOnlineReturn()
        {
            try
            {
                using (IReturnBL onlineReturnsBL = new ReturnBL())
                {
                    Write("OnlineReturn #: ");
                    bool isNumberValid = int.TryParse(ReadLine(), out int serial);
                    if (isNumberValid)
                    {
                        serial--;
                        List<Return> onlineReturns = await onlineReturnsBL.GetAllReturnsBL();
                        if (serial <= onlineReturns.Count - 1)
                        {
                            Return onlineReturn = onlineReturns[serial];
                            WriteLine("Purpose of Return:\n1.  UnsatiSfactoryProduct\n2. WrongProductShipped\n3.  WrongProductOrdered\n4. DefectiveProduct  ");
                            bool isPurposeValid = int.TryParse(ReadLine(), out int purpose);
                            Write("QuantityOfReturn: ");
                            onlineReturn.QuantityOfReturn = Convert.ToInt32(ReadLine());

                            bool isUpdated = await onlineReturnsBL.UpdateReturnBL(onlineReturn);
                            if (isUpdated)
                            {
                                WriteLine("OnlineReturn Updated");
                            }
                        }
                        else
                        {
                            WriteLine($"Invalid OnlineReturn #.\nPlease enter a number between 1 to {onlineReturns.Count}");
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

        public static async Task DeleteReturn()
        {
            try
            {
                using (IReturnBL ReturnBL = new ReturnBL())
                {
                    Write("OnlineReturn #: ");
                    bool isNumberValid = int.TryParse(ReadLine(), out int serialNo);
                    if (isNumberValid)
                    {
                        serialNo--;
                        List<Return> onlineReturns = await ReturnBL.GetAllReturnsBL();
                        if (serialNo <= onlineReturns.Count - 1)
                        {
                            Return onlineReturn = onlineReturns[serialNo];
                            Write("Are you sure? (Y/N): ");
                            string confirmation = ReadLine();

                            if (confirmation.Equals("Y", StringComparison.OrdinalIgnoreCase))
                            {
                                bool isDeleted = await ReturnBL.DeleteOnlineReturnBL(onlineReturn.OnlineReturnID);
                                if (isDeleted)
                                {
                                    WriteLine("OnlineReturn Deleted");
                                }
                            }
                        }
                        else
                        {
                            WriteLine($"Invalid OnlineReturn #.\nPlease enter a number between 1 to {onlineReturns.Count}");
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
    }
}
