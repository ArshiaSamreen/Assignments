using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecommerce.API.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    AddressID = table.Column<Guid>(nullable: false),
                    AddressLine1 = table.Column<string>(nullable: true),
                    AddressLine2 = table.Column<string>(nullable: true),
                    Landmark = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    PinCode = table.Column<string>(nullable: true),
                    CustomerID = table.Column<Guid>(nullable: false),
                    CreationDateTime = table.Column<DateTime>(nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.AddressID);
                });

            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    AdminID = table.Column<Guid>(nullable: false),
                    AdminName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    CreationDateTime = table.Column<DateTime>(nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.AdminID);
                });

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    CartId = table.Column<Guid>(nullable: false),
                    AddressID = table.Column<Guid>(nullable: false),
                    CustomerID = table.Column<Guid>(nullable: false),
                    TotalQuantity = table.Column<int>(nullable: false),
                    TotalAmount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.CartId);
                });

            migrationBuilder.CreateTable(
                name: "CartProduct",
                columns: table => new
                {
                    CartProductID = table.Column<Guid>(nullable: false),
                    CartId = table.Column<Guid>(nullable: false),
                    ProductID = table.Column<Guid>(nullable: false),
                    ProductQuantityOrdered = table.Column<int>(nullable: false),
                    ProductPrice = table.Column<double>(nullable: false),
                    AddressId = table.Column<Guid>(nullable: false),
                    TotalAmount = table.Column<double>(nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartProduct", x => x.CartProductID);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerID = table.Column<Guid>(nullable: false),
                    CustomerName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    CustomerMobile = table.Column<string>(nullable: true),
                    CreationDateTime = table.Column<DateTime>(nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(nullable: false),
                    CustomerID = table.Column<Guid>(nullable: false),
                    DateOfOrder = table.Column<DateTime>(nullable: false),
                    TotalQuantity = table.Column<int>(nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(nullable: false),
                    OrderAmount = table.Column<double>(nullable: false),
                    OrderNumber = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderId);
                });

            migrationBuilder.CreateTable(
                name: "OrderCancel",
                columns: table => new
                {
                    OrderCancelID = table.Column<Guid>(nullable: false),
                    CartProductID = table.Column<Guid>(nullable: false),
                    QuantityToBeCancelled = table.Column<int>(nullable: false),
                    RefundAmount = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderCancel", x => x.OrderCancelID);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    OrderDetailId = table.Column<Guid>(nullable: false),
                    OrderId = table.Column<Guid>(nullable: false),
                    IsCancelled = table.Column<bool>(nullable: false),
                    ProductID = table.Column<Guid>(nullable: false),
                    ProductQuantityOrdered = table.Column<int>(nullable: false),
                    ProductPrice = table.Column<double>(nullable: false),
                    AddressId = table.Column<Guid>(nullable: false),
                    TotalAmount = table.Column<double>(nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(nullable: false),
                    DateOfOrder = table.Column<DateTime>(nullable: false),
                    OrderSerial = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetail", x => x.OrderDetailId);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductID = table.Column<Guid>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    ProductColor = table.Column<string>(nullable: true),
                    ProductSize = table.Column<string>(nullable: true),
                    ProductMaterial = table.Column<string>(nullable: true),
                    Category = table.Column<int>(nullable: false),
                    ProductPrice = table.Column<double>(nullable: false),
                    CreationDateTime = table.Column<DateTime>(nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductID);
                });

            migrationBuilder.CreateTable(
                name: "Return",
                columns: table => new
                {
                    OnlineReturnID = table.Column<Guid>(nullable: false),
                    QuantityOfReturn = table.Column<int>(nullable: false),
                    ProductID = table.Column<Guid>(nullable: false),
                    OrderID = table.Column<Guid>(nullable: false),
                    TotalAmount = table.Column<double>(nullable: false),
                    ProductPrice = table.Column<double>(nullable: false),
                    Purpose = table.Column<int>(nullable: false),
                    CustomerID = table.Column<Guid>(nullable: false),
                    CreationDateTime = table.Column<DateTime>(nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(nullable: false),
                    OrderNumber = table.Column<int>(nullable: false),
                    ProductNumber = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Return", x => x.OnlineReturnID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "CartProduct");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "OrderCancel");

            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Return");
        }
    }
}
