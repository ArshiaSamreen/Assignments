using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Ecommerce.Ecommerce.Exceptions;
using Ecommerce.Ecommerce.Helpers.ValidationAttributes;

namespace Ecommerce.Ecommerce.BusinessLayer
{
    public abstract class BLBase<T>
    {
        protected async virtual Task<bool> Validate(T entityObject)
        {           
            StringBuilder sb = new StringBuilder();
            bool valid = true;
            await Task.Run(() =>
            {
                Type type = typeof(T);
                PropertyInfo[] properties = type.GetProperties();
                foreach (var prop in properties)
                {
                    var attr = prop.GetCustomAttribute<RequiredAttribute>();
                    if (attr != null)
                    {
                        object currentValue = prop.GetValue(entityObject);
                        if (string.IsNullOrEmpty(Convert.ToString(currentValue)))
                        {
                            valid = false;
                            sb.Append(Environment.NewLine + attr.ErrorMessage);
                        }
                    }
                }

                //Check the regular expression of properties using reflection and attributes
                foreach (var prop in properties)
                {
                    var attr = prop.GetCustomAttribute<RegExpAttribute>();
                    if (attr != null)
                    {
                        string currentValue = Convert.ToString(prop.GetValue(entityObject));
                        Regex regex = new Regex(attr.RegularExpressionToCheck);
                        if (!regex.IsMatch(currentValue))
                        {
                            valid = false;
                            sb.Append(Environment.NewLine + attr.ErrorMessage);
                        }
                    }
                }
            });

            if (valid == false)
                throw new EcommerceException(sb.ToString());
            return valid;
        }
    }
}