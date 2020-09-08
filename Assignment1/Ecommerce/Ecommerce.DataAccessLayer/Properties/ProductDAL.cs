using System;
using System.Collections.Generic;
using Capgemini.Ecommerce.Entities;
using Capgemini.Ecommerce.Helpers;
using Capgemini.Ecommerce.Contracts.DALContracts;

namespace Capgemini.Ecommerce.DataAccessLayer
{
    /// <summary>
    /// Contains data access layer methods for inserting, updating or deleting products from Products collection.
    /// </summary>
    public class ProductDAL : ProductDALBase, IDisposable
    {
        /// <summary>
        /// Gets all the products from the collection.
        /// </summary>
        /// <returns>Returns the list of all products</returns>
        public override List<Product> GetAllProductsDAL()
        {
            return productList;
        }

        /// <summary>
        /// Gets the product based on the product number
        /// </summary>
        /// <param name="productNumber">Represents the product number to be searched</param>
        /// <returns>Returns the product with the given product ID</returns>
        public override Product GetProductByProductIDDAL(Guid productID)
        {
            Product matchingProduct = null;
            try
            {
                //find product based on productNumber
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

        /// <summary>
        /// Gets all the products of a particular product category
        /// </summary>
        /// <param name="productCategory">Represents the product category whose products are to be fetched</param>
        /// <returns>The list of products of the given product category</returns>
        public override List<Product> GetProductsByProductCategoryDAL(Category givenCategory)
        {
            List<Product> tempProductList = new List<Product>();
            try
            {
                tempProductList = productList.FindAll(
                    (item) => { return item.category == givenCategory; }
                    );

            }
            catch (Exception)
            {
                throw;
            }
            return tempProductList;

        }

        /// <summary>
        /// Gets the list of all products with a particular product name. 
        /// </summary>
        /// <param name="productName">Represents the product name by which products are to be fetched</param>
        /// <returns>The list of products of the given product name</returns>
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

        /// <summary>
        /// Updates the description of the product
        /// </summary>
        /// <param name="updateProduct">Represents only the product description detail of the product</param>
        /// <returns>Returns whether the product description is updated</returns>
        public override bool UpdateProductDescriptionDAL(Product updateProduct)
        {
            bool descriptionUpdated = false;

            try
            {
                //finding product based on product ID
                Product matchingProduct = GetProductByProductIDDAL(updateProduct.ProductID);

                if (matchingProduct != null)
                {
                    //Update product description details
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

        /// <summary>
        /// Adds a new product to the product list collection
        /// </summary>
        /// <param name="addProduct">Contains the product details to be added</param>
        /// <returns>Tells whether the new product is added</returns>
        public override bool AddProductDAL(Product addProduct)
        {
            bool productAdded = false;

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

        /// <summary>
        /// Deletes Product based on ProductID
        /// </summary>
        /// <param name="productID">Represents the productID to be deleted</param>
        /// <returns>Tells whether the product is deleted</returns>
        public override bool DeleteProductDAL(Guid deleteProductID)
        {
            bool productDeleted = false;

            try
            {
                //Find product based on deleteProductID
                Product matchingProduct = GetProductByProductIDDAL(deleteProductID);

                if (matchingProduct != null)
                {
                    //Delete  product from the collection
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

        /// <summary>
        /// Updates the price of the product
        /// </summary>
        /// <param name="updateProduct">Represents only the product price detail of the product</param>
        /// <returns>Returns whether the product price is updated</returns>
        public override bool UpdateProductPriceDAL(Product updateProduct)
        {
            bool priceUpdated = false;

            try
            {
                //finding product based on product ID
                Product matchingProduct = GetProductByProductIDDAL(updateProduct.ProductID);

                if (matchingProduct != null)
                {
                    //Update product description detail
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

        /// <summary>
        /// Clears unmanaged resources suc as db connections or file streams
        /// </summary>
        public void Dispose()
        {
            //No unmanaged resources currently!
        }
    }
}
