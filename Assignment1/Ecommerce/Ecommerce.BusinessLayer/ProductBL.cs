using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ecommerce.Ecommerce.Contracts.BLContracts;
using Ecommerce.Ecommerce.Contracts.DALContracts;
using Ecommerce.Ecommerce.DataAccessLayer;
using Ecommerce.Ecommerce.Entities;
using Ecommerce.Ecommerce.Helpers;

namespace Ecommerce.Ecommerce.BusinessLayer
{
    public class ProductBL : BLBase<Product>, IProductBL, IDisposable
    {
        readonly ProductDALBase productDAL;
        
        public async Task<List<Product>> GetAllProductsBL()
        {
            List<Product> productsList = null;
            try
            {
                await Task.Run(() =>
                {
                    productsList = productDAL.GetAllProductsDAL();

                });
            }
            catch (Exception)
            {
                throw;
            }
            return productsList;
        }

        public async Task<Product> GetProductByProductIDBL(Guid productID)
        {
            Product matchingProduct = null;

            try
            {
                await Task.Run(() =>
                {
                    matchingProduct = productDAL.GetProductByProductIDDAL(productID);
                });
            }

            catch (Exception)
            {
                throw;
            }
            return matchingProduct;
        }

        public async Task<List<Product>> GetProductsByProductCategoryBL(Category givenCategory)
        {
            List<Product> tempProductsList = null;
            try
            {
                await Task.Run(() =>
                {
                    tempProductsList = productDAL.GetProductsByProductCategoryDAL(givenCategory);
                });
            }
            catch (Exception)
            {
                throw;
            }
            return tempProductsList;
        }

        public async Task<List<Product>> GetProductsByProductNameBL(string productName)
        {
            List<Product> tempProductsList = null;
            try
            {
                await Task.Run(() =>
                {
                    tempProductsList = productDAL.GetProductsByProductNameDAL(productName);
                });
            }
            catch (Exception)
            {
                throw;
            }
            return tempProductsList;
        }

        public async Task<bool> UpdateProductDescriptionBL(Product updateProduct)
        {
            bool descriptionUpdated = false;
            try
            {
                if (await GetProductByProductIDBL(updateProduct.ProductID) != null)
                {
                    productDAL.UpdateProductDescriptionDAL(updateProduct);
                    descriptionUpdated = true;
                    Serialize();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return descriptionUpdated;
        }

        public async Task<bool> UpdateProductPriceBL(Product updateProduct)
        {
            bool priceUpdated = false;
            try
            {
                if (await GetProductByProductIDBL(updateProduct.ProductID) != null)
                {
                    this.productDAL.UpdateProductPriceDAL(updateProduct);
                    priceUpdated = true;
                    Serialize();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return priceUpdated;
        }

        public async Task<bool> AddProductBL(Product addProduct)
        {
            bool productAdded = false;
            try
            {
                if (await Validate(addProduct))
                {
                    await Task.Run(() =>
                    {
                        productDAL.AddProductDAL(addProduct);
                        productAdded = true;
                        Serialize();
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
            return productAdded;
        }

        public async Task<bool> DeleteProductBL(Guid deleteProductID)
        {
            bool productDeleted = false;
            try
            {
                await Task.Run(() =>
                {
                    productDeleted = productDAL.DeleteProductDAL(deleteProductID);
                    Serialize();
                });
            }
            catch (Exception)
            {
                throw;
            }
            return productDeleted;
        }

        public static void Serialize()
        {
            try
            {
                ProductDAL.Serialize();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void Deserialize()
        {
            try
            {
                ProductDAL.Deserialize();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ProductBL()
        {
            productDAL = new ProductDAL();
        }
        public void Dispose()
        {
            ((ProductDAL)productDAL).Dispose();
        }
    }
}
