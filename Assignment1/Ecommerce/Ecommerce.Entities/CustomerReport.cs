using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Ecommerce.Entities
{
    public class CustomerReport
    {
        public Guid CustomerID { get; set; }
        public string CustomerName { get; set; }
        public int CustomerSalesCount { get; set; }
        public double CustomerSalesAmount { get; set; }


        public CustomerReport()
        {
            CustomerID = default(Guid);
            CustomerName = null;
            CustomerSalesCount = 0;
            CustomerSalesAmount = 0;



        }
    }


}

