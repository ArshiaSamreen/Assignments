using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static System.Console;
using Ecommerce.Ecommerce.BusinessLayer;
using Ecommerce.Ecommerce.Helpers;
using Ecommerce.Ecommerce.Entities;
using Ecommerce.Ecommerce.Exceptions;
using Ecommerce.Ecommerce.Contracts.BLContracts;
using Ecommerce.Ecommerce.PresentationLayer;
using Ecommerce.Ecommerce.Contracts;

namespace Ecommerce.Presentation
{
    class CustomerProductPresentation
    {
        public static async Task<int> CustomerProductMenu()
        {
            int choice = -2;
            using (IProductBL productBL = new ProductBL())
            {
                do
                {                  
                    List<Product> products = await productBL.GetAllProductsBL();
                    WriteLine("\n***************Products***********\n");
                    WriteLine("Product:");
                    if (products != null && products?.Count > 0)
                    {
                        WriteLine("#\tProductName\tPrice\tDescription\t");
                        int serialNo = 0;
                        foreach (var product in products)
                        {
                            serialNo++;
                            WriteLine($"{serialNo}\t{product.ProductName}\t\t{product.ProductPrice}\t" +
                                $"{product.ProductColor}{product.ProductSize},{product.ProductMaterial},{product.Category}");
                        }
                    }                   
                    WriteLine("\n1.Add Product to cart");
                    WriteLine("2.Remove Product from Cart");
                    WriteLine("3.Confirm Order");
                    WriteLine("4.Get Product Details");
                    WriteLine("0.Back");
                    WriteLine("-1.Exit");
                    Write("Choice: ");
                    bool isValidChoice = int.TryParse(ReadLine(), out choice);
                    if (isValidChoice)
                    {
                        switch (choice)
                        {
                            case 1:
                                await AddtoCart(); 
                                break;
                            case 2: 
                                await RemoveFromCart(); 
                                break;
                            case 3:
                                await ConfirmOrder(); 
                                break;
                            case 4:
                                await GetProduct();
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
            }
            return choice;
        }

        public static async Task AddtoCart()
        {
            try
            {
                IProductBL productBL = new ProductBL();               
                CartProduct cartProduct = new CartProduct();
                Write("Product #: ");
                bool isNumberValid = int.TryParse(ReadLine(), out int serialNo);
                if (isNumberValid)
                {
                    serialNo--;
                    List<Product> products = await productBL.GetAllProductsBL();
                    if (serialNo <= products.Count - 1)
                    {
                        Product product = products[serialNo];
                        cartProduct.ProductID = product.ProductID;
                        cartProduct.ProductPrice = product.ProductPrice;
                        Write("Enter Quantity : ");
                        cartProduct.ProductQuantityOrdered = int.Parse(ReadLine());
                        using (ICartProductBL cartProductBL = new CartProductBL())
                        {
                            bool isAdded = await cartProductBL.AddCartProductBL(cartProduct);
                            if (isAdded)
                            {
                                WriteLine("Product Added to Cart!");
                            }
                        }
                    }
                    else
                    {
                        WriteLine($"Invalid Product Serial #.\nPlease enter a number between 1 to {products.Count}");
                    }
                }
                else
                {
                    WriteLine($"Invalid number.");
                }

            }
            catch (Exception ex)
            {
                ExceptionLogger.LogException(ex);
                WriteLine(ex.Message);
            }
        }

        public static async Task RemoveFromCart()
        {
            try
            {
                ICartProductBL cartProductBL = new CartProductBL();
                List<CartProduct> cart = await cartProductBL.GetAllCartProductsBL();

                WriteLine("#\tProduct Price\tProduct Quantity \t Total Amount");
                int serial = 0;
                foreach (var cartProduct in cart)
                {
                    serial++;
                    WriteLine($"{serial}\t{cartProduct.ProductPrice}\t{cartProduct.ProductQuantityOrdered}\t{cartProduct.TotalAmount}");
                }
                Write("Product #: ");
                bool isNumberValid = int.TryParse(ReadLine(), out int serial1);
                if (isNumberValid)
                {
                    serial1--;
                    if (serial1 <= cart.Count - 1)
                    {                                              
                       bool isDeleted = await cartProductBL.DeleteCartProductBL(cart[serial1].CartId);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task ConfirmOrder()
        {
            try
            {
                IProductBL productBL = new ProductBL();
                Guid tempOrderID;
                ICartProductBL cartProductBL = new CartProductBL();
                List<CartProduct> cart = await cartProductBL.GetAllCartProductsBL();
                using (IOrderDetailBL orderDetailBL = new OrderDetailBL())
                {
                    CustomerBL customerBL = new CustomerBL();
                    Customer customer = await customerBL.GetCustomerByEmailBL(UserData.CurrentUser.Email);
                    AddressBL addressBL = new AddressBL();
                    List<Address> addresses = await addressBL.GetAddressByCustomerIDBL(customer.CustomerID);
                    double totalamount = 0;
                    int quantity = 0;
                    Guid orderID = Guid.NewGuid();
                    tempOrderID = orderID;
                    
                    foreach (var cartProduct in cart)
                    {
                        OrderDetail orderDetail = new OrderDetail();
                        WriteLine("#\tAddressLine 1\tLandMark\tCity");
                        int serial = 0;
                        foreach (var address in addresses)
                        {
                            serial++;
                            WriteLine($"{serial}\t{address.AddressLine1}\t{address.Landmark}\t{address.City}");
                        }

                        Write("Address #: ");
                        bool isNumberValid = int.TryParse(ReadLine(), out int serial1);
                        if (isNumberValid)
                        {
                            serial1--;

                            if (serial1 <= addresses.Count - 1)
                            {
                                //Read inputs
                                Address address = addresses[serial1];
                                orderDetail.AddressId = address.AddressID;
                            }
                        }
                        orderDetail.OrderId = orderID;
                        orderDetail.ProductID = cartProduct.ProductID;
                        orderDetail.ProductPrice = cartProduct.ProductPrice;
                        orderDetail.ProductQuantityOrdered = cartProduct.ProductQuantityOrdered;
                        orderDetail.TotalAmount = (cartProduct.ProductPrice * cartProduct.ProductQuantityOrdered);
                        totalamount += orderDetail.TotalAmount;
                        quantity += orderDetail.ProductQuantityOrdered;
                        bool isAdded;
                        Guid newguid;
                        (isAdded,newguid)= await orderDetailBL.AddOrderDetailsBL(orderDetail);
                    }

                    using (IOrderBL orderBL = new OrderBL())
                    {
                        Order order = new Order
                        {
                            OrderId = orderID,
                            CustomerID = customer.CustomerID,
                            TotalQuantity = quantity,
                            OrderAmount = totalamount
                        };
                        bool isAdded;
                        Guid newguid;
                        (isAdded,newguid)= await orderBL.AddOrderBL(order);
                        if (isAdded)
                        {
                            WriteLine("Order  Added");
                        }
                    }
                }

                IOrderBL orderBL1 = new OrderBL();
                Order order1 = await orderBL1.GetOrderByOrderIDBL(tempOrderID);
                WriteLine($"Your Order No. {order1.OrderNumber}\t{order1.TotalQuantity}\t{order1.OrderAmount}\t{order1.DateOfOrder}");
                WriteLine("Order Details");
                IOrderDetailBL orderDetailBL1 = new OrderDetailBL();
                List<OrderDetail> orderDetailslist = await orderDetailBL1.GetOrderDetailsByOrderIDBL(tempOrderID);
                foreach (var item in orderDetailslist)
                {
                    Product product = await productBL.GetProductByProductIDBL(item.ProductID);
                    WriteLine($"{product.ProductName}\t{item.ProductPrice}\t{item.ProductQuantityOrdered}\t{item.TotalAmount}");
                }
                cart.Clear();
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogException(ex);
                WriteLine(ex.Message);
            }
        }
        
        public static async Task GetProduct()
        {
            try
            {
                using (IProductBL productBL = new ProductBL())
                {
                    int choice;
                    WriteLine("\n1.Get all products");
                    WriteLine("2.Get product by product name");
                    WriteLine("3.Get product by product category");
                    Write("Enter your choice : ");
                    choice = int.Parse(ReadLine());
                    switch (choice)
                    {
                        case 1:
                            List<Product> allProductList = new List<Product>();
                            allProductList = await productBL.GetAllProductsBL();
                            break;
                        case 2:
                            Write("Product name: ");
                            string productName = ReadLine();
                            List<Product> productNameList = new List<Product>();
                            productNameList = await productBL.GetProductsByProductNameBL(productName);
                            break;
                        case 3:
                            WriteLine("Choose among the following categories:");
                            WriteLine("1.Clothing");
                            WriteLine("2.Footwear");
                            WriteLine("3.Bags");
                            WriteLine("4.Books");
                            WriteLine("5.Furniture");
                            bool isCategory = int.TryParse(ReadLine(), out int choice1);
                            Category givenCategory;
                            if (isCategory)
                            {
                                if (choice1 == 1)
                                {
                                    givenCategory = Category.Clothing;
                                }
                                else if (choice1 == 2)
                                {
                                    givenCategory = Category.Footwear;
                                }
                                else if (choice1 == 3)
                                {
                                    givenCategory = Category.Bags;
                                }
                                else if (choice1 == 4)
                                {
                                    givenCategory = Category.Books;
                                }
                                else if (choice1 == 5)
                                {
                                    givenCategory = Category.Furniture;
                                }
                                else
                                {
                                    givenCategory = Category.Clothing;
                                }
                            }
                            else
                            {
                                givenCategory = Category.Clothing;
                            }
                            List<Product> categoryProductList = new List<Product>();
                            categoryProductList = await productBL.GetProductsByProductCategoryBL(givenCategory);
                            break;
                        default: 
                            WriteLine("Invalid Choice"); break;
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