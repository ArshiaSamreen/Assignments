using System;
using System.Collections.Generic;
using Ecommerce.Ecommerce.Entities;
using Ecommerce.Ecommerce.Helpers;
using Ecommerce.Ecommerce.Contracts.DALContracts;

namespace Ecommerce.Ecommerce.DataAccessLayer
{   
    public class ProductDAL : ProductDALBase, IDisposable
    {
        public override List<Product> GetAllProductsDAL()
        {
            return productList;
        }

        public override Product GetProductByProductIDDAL(Guid productID)
        {
            Product matchingProduct = null;
            try
            {
                matchingProduct = productList.Find(
                        (item) => { return item.ProductID == productID; }
                    );
            }
            catch (Exception)
            {
                throw;
            }
            return matchingProduct;
        }

        public override List<Product> GetProductsByProductCategoryDAL(Category givenCategory)
        {
            List<Product> tempProductList = new List<Product>();
            try
            {
                tempProductList = productList.FindAll(
                        (item) => { return item.Category == givenCategory; }
                    );

            }
            catch (Exception)
            {
                throw;
            }
            return tempProductList;
        }

        public override List<Product> GetProductsByProductNameDAL(string productName)
        {
            List<Product> tempProductList = new List<Product>();
            try
            {
                tempProductList = productList.FindAll(
                       (item) => { return item.ProductName == productName; }
                   );

            }
            catch (Exception)
            {
                throw;
            }
            return tempProductList;
        }

        public override bool UpdateProductDescriptionDAL(Product updateProduct)
        {
            bool descriptionUpdated = false;
            try
            {
                Product matchingProduct = GetProductByProductIDDAL(updateProduct.ProductID);
                if (matchingProduct != null)
                {
                    ReflectionHelpers.CopyProperties(updateProduct, matchingProduct, new List<string>() { "ProductDescription" });
                    matchingProduct.LastModifiedDateTime = DateTime.Now;
                    descriptionUpdated = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return descriptionUpdated;
        }

        public override bool AddProductDAL(Product addProduct)
        {
            bool productAdded;
            try
            {
                addProduct.ProductID = Guid.NewGuid();
                addProduct.CreationDateTime = DateTime.Now;
                addProduct.LastModifiedDateTime = DateTime.Now;
                productList.Add(addProduct);
                productAdded = true;
            }
            catch (Exception)
            {
                throw;
            }
            return productAdded;
        }

        public override bool DeleteProductDAL(Guid deleteProductID)
        {
            bool productDeleted = false;
            try
            {
                Product matchingProduct = GetProductByProductIDDAL(deleteProductID);
                if (matchingProduct != null)
                {
                    productList.Remove(matchingProduct);
                    productDeleted = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return productDeleted;
        }

        public override bool UpdateProductPriceDAL(Product updateProduct)
        {
            bool priceUpdated = false;
            try
            {
                Product matchingProduct = GetProductByProductIDDAL(updateProduct.ProductID);
                if (matchingProduct != null)
                {
                    ReflectionHelpers.CopyProperties(updateProduct, matchingProduct, new List<string>() { "ProductPrice" });
                    matchingProduct.LastModifiedDateTime = DateTime.Now;
                    priceUpdated = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return priceUpdated;
        }

        public void Dispose()
        {

        }
    }
}
