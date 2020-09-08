using Ecommerce.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.MVC.Model
{
    public class Return
    {
        [System.ComponentModel.DataAnnotations.Key]
        [Required("OnlineReturnID can't be blank.")]
        public Guid OnlineReturnID { get; set; }
        [Required("Quantity can't be blank.")]
        [RegExp("^[0-9]*[0-9][0-9]*$", "Quantity cannot be less than 0.")]
        public int QuantityOfReturn { get; set; }
        [Required("ProductID can't be blank.")]
        public Guid ProductID { get; set; }
        [Required("OrderID can't be blank.")]
        public Guid OrderID { get; set; }
        [Required("ReturnAmount can't be blank.")]
        public double TotalAmount { get; set; }
        [Required("Product Price can't be blank.")]
        public double ProductPrice { set; get; }
        [Required("PurposeOfReturn can't be blank.")]
        public PurposeOfReturn Purpose { get; set; }
        public Guid CustomerID { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime LastModifiedDateTime { get; set; }
        [RegExp("^[0-9]*[0-9][0-9]*$", "Quantity cannot be less than 0.")]
        public int OrderNumber { get; set; }
        [RegExp("^[0-9]*[0-9][0-9]*$", "Quantity cannot be less than 0.")]
        public int ProductNumber { get; set; }
    }
}
