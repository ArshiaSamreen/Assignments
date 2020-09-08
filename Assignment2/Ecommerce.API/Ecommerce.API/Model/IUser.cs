using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.API.Model
{
    public interface IUser
    {
        string Email { get; set; }
        string Password { get; set; }
    }
}
