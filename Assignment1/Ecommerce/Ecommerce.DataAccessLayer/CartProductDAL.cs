using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Ecommerce.Contracts;
using Ecommerce.Ecommerce.Entities;
using Ecommerce.Ecommerce.Exceptions;
using Ecommerce.Ecommerce.Helpers;


namespace Ecommerce.Ecommerce.DataAccessLayer
{
    public class CartProductDAL : CartProductDALBase, IDisposable
    {
        public override bool AddCartProductDAL(CartProduct newCartProduct)
        {
            bool cartProductAdded;
            try
            {
                newCartProduct.CartId = Guid.NewGuid();
                cartList.Add(newCartProduct);
                cartProductAdded = true;
            }
            catch (Exception)
            {
                throw;
            }
            return cartProductAdded;
        }

        public override List<CartProduct> GetAllCartProductsDAL()
        {
            return cartList;
        }

        public override CartProduct GetCartProductByCartIDDAL(Guid searchCartID)
        {
            CartProduct matchingCartProduct = null;
            try
            {
                matchingCartProduct = cartList.Find(
                    (item) => { return item.CartId == searchCartID; }
                );
            }
            catch (Exception)
            {
                throw;
            }
            return matchingCartProduct;
        }

        public override bool DeleteCartProductDAL(Guid deleteCartID)
        {
            bool cartProductDeleted = false;
            try
            {
                CartProduct matchingCartProduct = cartList.Find(
                    (item) => { return item.CartId == deleteCartID; }
                );
                if (matchingCartProduct != null)
                {
                    cartList.Remove(matchingCartProduct);
                    cartProductDeleted = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return cartProductDeleted;
        }

        public override bool UpdateCartProductDAL(CartProduct updateCartProduct)
        {
            bool cartProductUpdated = false;
            try
            {
                CartProduct matchingCartProduct = GetCartProductByCartIDDAL(updateCartProduct.CartId);
                if (matchingCartProduct != null)
                {
                    ReflectionHelpers.CopyProperties(updateCartProduct, matchingCartProduct, new List<string>() { "ProductId", "ProductQuantityOrdered", "AddressId", "TotalAmount" });
                    matchingCartProduct.LastModifiedDateTime = DateTime.Now;
                    cartProductUpdated = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return cartProductUpdated;
        }

        public void Dispose()
        {

        }
    }
}
