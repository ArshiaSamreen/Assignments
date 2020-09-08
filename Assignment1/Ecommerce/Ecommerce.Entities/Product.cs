using System;
using Ecommerce.Ecommerce.Helpers.ValidationAttributes;
using Ecommerce.Ecommerce.Helpers;

namespace Ecommerce.Ecommerce.Entities
{
    public interface IProduct
    {
        Guid ProductID { get; set; }
        string ProductName { get; set; }
        Category Category { get; set; }
        string ProductColor { get; set; }
        string ProductSize { get; set; }
        string ProductMaterial { get; set; }
        double ProductPrice { get; set; }
        DateTime CreationDateTime { get; set; }
        DateTime LastModifiedDateTime { get; set; }
    }
    public class Product : IProduct
    {
        /* Auto-Implemented Properties*/
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

        /* Constructor */
        public Product()
        {
            ProductID = default;
            ProductName = null;
            Category = default;
            ProductColor = null;
            ProductSize = null;
            ProductMaterial = null;
            ProductPrice = default;
            CreationDateTime = default;
            LastModifiedDateTime = default;
        }
    }
}

