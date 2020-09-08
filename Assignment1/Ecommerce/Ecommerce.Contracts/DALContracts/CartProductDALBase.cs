using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Ecommerce.Entities;

namespace Ecommerce.Ecommerce.Contracts
{
    public abstract class CartProductDALBase
    {      
        protected static List<CartProduct> cartList = new List<CartProduct>();
        private static readonly string fileName = "Cart.json";
        public abstract bool AddCartProductDAL(CartProduct newCartProduct);
        public abstract List<CartProduct> GetAllCartProductsDAL();
        public abstract CartProduct GetCartProductByCartIDDAL(Guid searchCartID);
        public abstract bool UpdateCartProductDAL(CartProduct updateCartProduct);
        public abstract bool DeleteCartProductDAL(Guid deleteCartProductID);

        public static void Serialize()
        {
            string serializedJson = JsonConvert.SerializeObject(cartList);
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
                var systemUserListFromFile = JsonConvert.DeserializeObject<List<CartProduct>>(fileContent);
                if (systemUserListFromFile != null)
                {
                    cartList = systemUserListFromFile;
                }
            }
        }
        static CartProductDALBase()
        {
            Deserialize();
        }
    }
}
