using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.MVC.Model
{
    public class OrderCancel
    {
        public Guid OrderCancelID { get; set; }
        public Guid CartProductID { set; get; }

        public int QuantityToBeCancelled { set; get; }
        public double RefundAmount { set; get; }
    }
}
