using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Ecommerce.Contracts;
using Ecommerce.Ecommerce.DataAccessLayer;
using Ecommerce.Ecommerce.Entities;
using Ecommerce.Ecommerce.Exceptions;

namespace Ecommerce.Ecommerce.BusinessLayer
{   
    public class CartProductBL : BLBase<CartProduct>, ICartProductBL, IDisposable
    {
        readonly CartProductDALBase cartProductDAL;
       
        protected async override Task<bool> Validate(CartProduct entityObject)
        {
            StringBuilder sb = new StringBuilder();
            bool valid = await base.Validate(entityObject);
            var existingObject = await GetCartProductByCartIDBL(entityObject.CartId);
            if (existingObject != null && existingObject?.ProductID != entityObject.ProductID)
            {
                valid = false;
                sb.Append(Environment.NewLine + $"CartID {entityObject.CartId} already exists");
            }
            if (valid == false)
                throw new EcommerceException(sb.ToString());
            return valid;
        }

        public async Task<bool> AddCartProductBL(CartProduct newCartProduct)
        {
            bool cartProductAdded = false;
            try
            {
                if (await Validate(newCartProduct))
                {
                    await Task.Run(() =>
                    {
                        cartProductDAL.AddCartProductDAL(newCartProduct);
                        cartProductAdded = true;
                        Serialize();
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
            return cartProductAdded;
        }

        public async Task<CartProduct> GetCartProductByCartIDBL(Guid cartId)
        {
            CartProduct matchingCartProduct = null;
            try
            {
                await Task.Run(() =>
                {
                    matchingCartProduct = cartProductDAL.GetCartProductByCartIDDAL(cartId);
                });
            }
            catch (Exception)
            {
                throw;
            }
            return matchingCartProduct;
        }

        public async Task<bool> UpdateCartProductBL(CartProduct updateCartProduct)
        {
            bool cartProductUpdated = false;
            try
            {
                if ((await Validate(updateCartProduct)) && (await GetCartProductByCartIDBL(updateCartProduct.CartId)) != null)
                {
                    cartProductDAL.UpdateCartProductDAL(updateCartProduct);
                    cartProductUpdated = true;
                    Serialize();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return cartProductUpdated;
        }

        public async Task<bool> DeleteCartProductBL(Guid deleteCartID)
        {
            bool cartProductDeleted = false;
            try
            {
                await Task.Run(() =>
                {
                    cartProductDeleted = cartProductDAL.DeleteCartProductDAL(deleteCartID);
                    Serialize();
                });
            }
            catch (Exception)
            {
                throw;
            }
            return cartProductDeleted;
        }

        public CartProductBL()
        {
            cartProductDAL = new CartProductDAL();
        }
        public void Dispose()
        {
            ((CartProductDAL)cartProductDAL).Dispose();
        }

        public void Serialize()
        {
            try
            {
                CartProductDAL.Serialize();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Deserialize()
        {
            try
            {
                CartProductDAL.Deserialize();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<CartProduct>> GetAllCartProductsBL()
        {
            List<CartProduct> cartProductsList = null;
            try
            {
                await Task.Run(() =>
                {
                    cartProductsList =cartProductDAL.GetAllCartProductsDAL();

                });
            }
            catch (Exception)
            {
                throw;
            }
            return cartProductsList;
        }
    }
}
