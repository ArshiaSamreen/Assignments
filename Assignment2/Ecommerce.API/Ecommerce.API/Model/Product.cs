using Ecommerce.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.API.Model
{
    public class Product
    {
        [Required("Product ID can't be blank!")]
        public Guid ProductID { get; set; }

        [Required("Product Name can't be blank!")]
        public string ProductName { get; set; }

        [Required("Product color can't be blank!")]
        public string ProductColor { get; set; }

        [Required("Product size can't be blank!")]
        public string ProductSize { get; set; }

        [Required("Product material can't be blank!")]
        public string ProductMaterial { get; set; }

        [Required("Product Category can't be blank!")]

        public Category Category { get; set; }

        [Required("Product price can't be blank!")]
        public double ProductPrice { get; set; }

        public DateTime CreationDateTime { get; set; }

        public DateTime LastModifiedDateTime { get; set; }
    }
}
