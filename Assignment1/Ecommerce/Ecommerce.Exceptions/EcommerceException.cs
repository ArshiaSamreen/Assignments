using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Ecommerce.Exceptions
{
    /// <summary>
    /// Represents project-level user-defined exception.
    /// </summary>
    public class EcommerceException : ApplicationException
    {
        ///constructors
        public EcommerceException() : base()
        {
        }

        public EcommerceException(string message) : base(message)
        {
        }

        public EcommerceException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}


