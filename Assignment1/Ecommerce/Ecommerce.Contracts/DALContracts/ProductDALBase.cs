using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Ecommerce.Helpers;
using Newtonsoft.Json;
using Ecommerce.Ecommerce.Entities;
using System.IO;

namespace Ecommerce.Ecommerce.Contracts.DALContracts
{
    public abstract class ProductDALBase
    {
        protected static List<Product> productList = new List<Product>()
        {
            new Product(){ProductID=Guid.NewGuid(),ProductName="Salwar",Category= Category.Clothing,ProductPrice=500.00,ProductColor="Green", ProductMaterial="Cotton"},
            new Product(){ProductID=Guid.NewGuid(),ProductName="Shirt",Category=Category.Clothing,ProductPrice=500.00,ProductColor="Red", ProductMaterial="Nylon"},
            new Product(){ProductID=Guid.NewGuid(),ProductName="Sandals",Category=Category.Footwear,ProductPrice=300.00,ProductColor="Black", ProductMaterial="Leather"},
            new Product(){ProductID=Guid.NewGuid(),ProductName="HandBag",Category=Category.Bags,ProductPrice=500.00,ProductColor="Blue", ProductMaterial="Leather"},
            new Product(){ProductID=Guid.NewGuid(),ProductName="Cloud",Category=Category.Books,ProductPrice=400.00,ProductColor="White", ProductMaterial="Paper"},
            new Product(){ProductID=Guid.NewGuid(),ProductName="Chair",Category=Category.Furniture,ProductPrice=500.00,ProductColor="Black", ProductMaterial="Wooden"},
            new Product(){ProductID=Guid.NewGuid(),ProductName="Sofa",Category=Category.Furniture,ProductPrice=1000.00,ProductColor="Black", ProductMaterial="Polyester"}
        };
        private static readonly string fileName = "Products.json";
        public abstract List<Product> GetAllProductsDAL();
        public abstract Product GetProductByProductIDDAL(Guid productID);
        public abstract List<Product> GetProductsByProductCategoryDAL(Category givenCategory);
        public abstract List<Product> GetProductsByProductNameDAL(string productName);
        public abstract bool UpdateProductDescriptionDAL(Product updateProduct);
        public abstract bool AddProductDAL(Product addProduct);
        public abstract bool DeleteProductDAL(Guid deleteProductID);
        public abstract bool UpdateProductPriceDAL(Product updateProduct);
        public static void Serialize()
        {
            string serializedJson = JsonConvert.SerializeObject(productList);
            using (StreamWriter streamWriter = new StreamWriter(fileName))
            {
                streamWriter.Write(serializedJson);
                streamWriter.Close();
            }
        }
        public static void Deserialize()
        {
            if (!File.Exists(fileName))
                File.Create(fileName).Close();
            using (StreamReader streamReader = new StreamReader(fileName))
            {
                string fileContent = streamReader.ReadToEnd();
                streamReader.Close();
                var productListFromFile = JsonConvert.DeserializeObject<List<Product>>(fileContent);
                if (productListFromFile != null)
                {
                    productList = productListFromFile;
                }
            }
        }
        static ProductDALBase()
        {
            Deserialize();
        }
    }
}
