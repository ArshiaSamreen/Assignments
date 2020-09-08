﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.MVC.Model
{
    public class Cart
    {
        public Guid CartId { get; set; }
        public Guid AddressID { get; set; }
        public Guid CustomerID { get; set; }

        public int TotalQuantity { get; set; }
        public int TotalAmount { get; set; }
    }
}
