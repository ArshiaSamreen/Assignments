using Ecommerce.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.MVC.Model
{
    public class Order
    {
        /*Auto-Implemented Properties*/
        [Required("OrderId can't be blank.")]
        public Guid OrderId { get; set; } //ID of the order
        [Required("CustomerID can't be blank.")]
        public Guid CustomerID { get; set; } //ID of the customer
        public DateTime DateOfOrder { get; set; } //Date of placing order
        public int TotalQuantity { get; set; } //Total quantity of the products ordererd
        public DateTime LastModifiedDateTime { get; set; }//date of order modification
        public double OrderAmount { get; set; }//total cost of the order
        public OrderStatus status; //Gives the status of the order
        public double OrderNumber { get; set; } = 0;//order no of the customer
    }
}
