using Ecommerce.Ecommerce.Helpers.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Ecommerce.Entities
{
    /// <summary>
    /// Interface for OrderDetail entity
    /// </summary>
    public interface IOrderDetail
    {
        Guid OrderDetailId { get; set; }
        Guid OrderId { get; set; }
        bool IsCancelled { get; set; }
        Guid ProductID { get; set; }
        int ProductQuantityOrdered { get; set; }
        double ProductPrice { get; set; }
        Guid AddressId { get; set; }
        double TotalAmount { get; set; }
        DateTime LastModifiedDateTime { get; set; }
        DateTime DateOfOrder { get; set; }
    }

    /// <summary>
    /// contains detailed information of each product in the order.
    /// </summary>
    public class OrderDetail : IOrderDetail
    {
        /*Auto-Implemented Properties*/
        [Required("OrderDetailsId can't be blank.")]
        public Guid OrderDetailId { get; set; } //ID of the order details
        [Required("OrderId can't be blank.")]
        public Guid OrderId { get; set; } //ID of the order placed
        public bool IsCancelled { get; set; } = false;//cancellation status
        [Required("ProductID can't be blank.")]
        public Guid ProductID { get; set; } //ID of the product
        public int ProductQuantityOrdered { get; set; }//quantity of the product ordered
        public double ProductPrice { get; set; }//price of the product
        [Required("AddressId can't be blank.")]
        public Guid AddressId { get; set; }//address ID of the address to which the product has to be delivered
        public double TotalAmount { get; set; } //Revenue through product
        public DateTime LastModifiedDateTime { get; set; }//date of modification of cart
        public DateTime DateOfOrder { get; set; }//date of ordering
        public int OrderSerial { get; set; }//order serial is equal to OrderNumber
    }

}
