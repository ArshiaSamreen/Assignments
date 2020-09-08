using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capgemini.Ecommerce.Contracts;
using Capgemini.Ecommerce.Entities;
using Capgemini.Ecommerce.Exceptions;
using Capgemini.Ecommerce.Helpers;


namespace Capgemini.Ecommerce.DataAccessLayer
{
    public class CartProductDAL : CartProductDALBase, IDisposable
    {
        /// <summary>
        /// Adds new cartProduct to CartProduct collection.
        /// </summary>
        /// <param name="newCartProduct">Contains the CartProduct details to be added.</param>
        /// <returns>Determinates whether the new cartProduct is added.</returns>
        public override bool AddCartProductDAL(CartProduct newCartProduct)
        {
            bool cartProductAdded = false;
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

        /// <summary>
        /// Gets all cartProducts from the collection.
        /// </summary>
        /// <returns>Returns list of all cartProduct.</returns>
        public override List<CartProduct> GetAllCartProductsDAL()
        {
            return cartList;
        }

        /// <summary>
        /// Gets systemUser based on SystemUserID.
        /// </summary>
        /// <param name="searchSystemUserID">Represents SystemUserID to search.</param>
        /// <returns>Returns SystemUser object.</returns>
        public override CartProduct GetCartProductByCartIDDAL(Guid searchCartID)
        {
            CartProduct matchingCartProduct = null;
            try
            {
                //Find CartProduct based on cartID
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

        /// <summary>
        /// Deletes CartProduct based on cartID.
        /// </summary>
        /// <param name="deleteCartProductID">Represents cartID to delete.</param>
        /// <returns>Determinates whether the existing CartProduct is updated.</returns>
        public override bool DeleteCartProductDAL(Guid deleteCartID)
        {
            bool cartProductDeleted = false;
            try
            {
                //Find CartProduct based on searchSystemUserID
                CartProduct matchingCartProduct = cartList.Find(
                    (item) => { return item.CartId == deleteCartID; }
                );

                if (matchingCartProduct != null)
                {
                    //Delete SystemUser from the collection
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

        /// <summary>
        /// Updates CartProduct based on CartID.
        /// </summary>
        /// <param name="updateCartProduct">Represents CartProduct details like CartID.</param>
        /// <returns>Determinates whether the existing CartProduct is updated.</returns>
        public override bool UpdateCartProductDAL(CartProduct updateCartProduct)
        {
            bool cartProductUpdated = false;
            try
            {
                //Find SystemUser based on SystemUserID
                CartProduct matchingCartProduct = GetCartProductByCartIDDAL(updateCartProduct.CartId);

                if (matchingCartProduct != null)
                {
                    //Update systemUser details
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

        /// <summary>
        /// Clears unmanaged resources such as db connections or file streams.
        /// </summary>
        public void Dispose()
        {
            //No unmanaged resources currently
        }
    }
}
