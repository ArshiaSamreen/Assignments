using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Ecommerce.Entities;
namespace Ecommerce.Ecommerce.Contracts
{
    public interface ICartProductBL : IDisposable
    {
        Task<List<CartProduct>> GetAllCartProductsBL();
        Task<bool> AddCartProductBL(CartProduct newCartProduct);
        Task<CartProduct> GetCartProductByCartIDBL(Guid searchCartID);
        Task<bool> UpdateCartProductBL(CartProduct updateCartProduct);
        Task<bool> DeleteCartProductBL(Guid deleteCartID);

    }
}
