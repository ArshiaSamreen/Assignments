using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ecommerce.Ecommerce.Entities;
using Ecommerce.Ecommerce.Helpers;

namespace Ecommerce.Ecommerce.PresentationLayer
{
    public static class UserData
    {
        public static IUser CurrentUser { get; set; }
        public static UserType CurrentUserType { get; set; }
    }
}