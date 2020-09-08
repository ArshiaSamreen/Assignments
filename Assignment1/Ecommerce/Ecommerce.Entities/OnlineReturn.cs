using System;
using Ecommerce.Ecommerce.Helpers;
using Ecommerce.Ecommerce.Helpers.ValidationAttributes;

namespace Ecommerce.Ecommerce.Entities
{
    /// <summary>
    /// Interface for OnlineReturn Entity
    /// </summary>
    public interface IOnlineReturn
    {
        Guid OnlineReturnID { get; set; }
        PurposeOfReturn Purpose { get; set; }
        int QuantityOfReturn { get; set; }
        Guid OrderID { get; set; }
        Guid ProductID { get; set; }
        double TotalAmount { get; set; }
        Guid CustomerID { get; set; } //ID of the customer
        double ProductPrice { get; set; }
        DateTime CreationDateTime { get; set; }
        DateTime LastModifiedDateTime { get; set; }
        int OrderNumber { get; set; }
        int ProductNumber { get; set; }
        
    }

    /// <summary>
    /// Represents OnlineReturn
    /// </summary>
    public class Return : IOnlineReturn
    {
        /* Auto-Implemented Properties */
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
          
        /* Constructor */
        public Return()
        {
            OnlineReturnID = default(Guid);
            Purpose = default(PurposeOfReturn);
            OrderID = default(Guid);
            ProductID = default(Guid);
            TotalAmount = 0.0;
            QuantityOfReturn = 0;
            CreationDateTime = default(DateTime);
            LastModifiedDateTime = default(DateTime);
        }
    }
}