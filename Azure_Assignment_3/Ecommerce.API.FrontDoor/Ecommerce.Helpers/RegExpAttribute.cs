using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Helpers
{
    public class RegExpAttribute : Attribute
    {
        public string RegularExpressionToCheck { get; set; }
        public string ErrorMessage { get; set; }

        public RegExpAttribute(string regularExpression) : base()
        {
            RegularExpressionToCheck = regularExpression;
        }

        public RegExpAttribute(string regularExpression, string errorMessage) : base()
        {
            RegularExpressionToCheck = regularExpression;
            ErrorMessage = errorMessage;
        }
    }
}
