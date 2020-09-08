using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.MVC.Model
{
    public class CustomerReport
    {
        public Guid CustomerID { get; set; }
        public string CustomerName { get; set; }
        public int CustomerSalesCount { get; set; }
        public double CustomerSalesAmount { get; set; }
    }
}
