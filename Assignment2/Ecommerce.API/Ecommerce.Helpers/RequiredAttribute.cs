using System;

namespace Ecommerce.Helpers
{
    public class RequiredAttribute : Attribute
    {
        public string ErrorMessage { get; set; }

        /* Constructors */
        public RequiredAttribute() : base()
        {
        }

        public RequiredAttribute(string errorMessage) : base()
        {
            ErrorMessage = errorMessage;
        }
    }
}
