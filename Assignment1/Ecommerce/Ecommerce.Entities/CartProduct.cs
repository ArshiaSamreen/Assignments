using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Ecommerce.Entities
{
    public class CartProduct
    {
        public Guid CartProductID { get; set; }
        public Guid CartId { get; set; }
        
        public Guid ProductID { get; set; }
        public int ProductQuantityOrdered { get; set; }
        public double ProductPrice { get; set; }
        public Guid AddressId { get; set; }
        public double TotalAmount { get; set; } 
        public DateTime LastModifiedDateTime { get; set; }
    }
}
