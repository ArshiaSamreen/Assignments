using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace Ecommerce.MVC.Helper
{
    public class Helper
    {
        public HttpClient Initial()
        {
            var Client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:4388")
            };
            return Client;
        }       
    }
}
