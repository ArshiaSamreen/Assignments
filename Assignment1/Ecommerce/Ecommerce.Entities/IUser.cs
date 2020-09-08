using System;
using System.Collections.Generic;

namespace Ecommerce.Ecommerce.Entities
{
    public interface IUser
    {
        string Email { get; set; }
        string Password { get; set; }
    }
}
