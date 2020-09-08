using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Ecommerce.Entities;
using Ecommerce.Ecommerce.Helpers;

namespace Ecommerce.Ecommerce.Contracts.BLContracts
{
    public interface IProductBL : IDisposable
    {
        Task<List<Product>> GetAllProductsBL();
        Task<Product> GetProductByProductIDBL(Guid productNumber);
        Task<List<Product>> GetProductsByProductCategoryBL(Category givenCategory);
        Task<List<Product>> GetProductsByProductNameBL(string productName);
        Task<bool> UpdateProductDescriptionBL(Product updateProduct);
        Task<bool> AddProductBL(Product addProduct);
        Task<bool> DeleteProductBL(Guid deleteProductID);
        Task<bool> UpdateProductPriceBL(Product updateProduct);

    }
}
