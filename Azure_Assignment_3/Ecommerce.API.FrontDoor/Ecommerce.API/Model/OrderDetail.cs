using Ecommerce.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.API.Model
{
    public class OrderDetail
    {
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
