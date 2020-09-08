using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Ecommerce.Entities
{
    //interface should be added

    public class OrderCancel
    {
        public Guid OrderCancelID { get; set; }
        public Guid CartProductID { set; get; }//Order Id of the order to be cancelled 
        
        public int QuantityToBeCancelled { set; get; }//Quantity of the product to be cancelled
        public double RefundAmount { set; get; }//Amount to be refunded to the customer
    }
}
