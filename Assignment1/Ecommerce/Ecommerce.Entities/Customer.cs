using System;
using System.Collections.Generic;
using System.Linq;
using Ecommerce.Ecommerce.Helpers.ValidationAttributes;
using Ecommerce.Ecommerce.Entities;

namespace Ecommerce.Ecommerce.Entities
{
    /// <summary>
    /// Interface for Customer Entity
    /// </summary>
    public interface ICustomer
    {
        Guid CustomerID { get; set; }
        string CustomerName { get; set; }
        string Email { get; set; }
        string Password { get; set; }
        string CustomerMobile { get; set; }
        DateTime CreationDateTime { get; set; }
        DateTime LastModifiedDateTime { get; set; }
    }

    /// <summary>
    /// Represents Customer
    /// </summary>
    public class Customer : ICustomer, IUser
    {
        /* Auto-Implemented Properties */
        [Required("Customer ID can't be blank.")]
        public Guid CustomerID { get; set; }

        [Required("Customer Name can't be blank.")]
        [RegExp(@"^(\w{2,40})$", "Customer Name should contain only 2 to 40 characters.")]
        public string CustomerName { get; set; }

        [Required("Email can't be blank.")]
        [RegExp(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", "Email is invalid.")]
        public string Email { get; set; }

        [Required("Password can't be blank.")]
        [RegExp(@"((?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,15})", "Password should be 6 to 15 characters with at least one digit, one uppercase letter, one lower case letter.")]
        public string Password { get; set; }

        [Required("Mobile No. Can not be blank")]
        [RegExp(@"^((\+)?(\d{2}[-]))?(\d{10}){1}?$", "10 digit Mobile no.")]
        public string CustomerMobile { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime LastModifiedDateTime { get; set; }

        /* Constructor */
        public Customer()
        {
            CustomerID = default(Guid);
            CustomerName = null;
            Email = null;
            Password = null;
            CustomerMobile = null;
            CreationDateTime = default(DateTime);
            LastModifiedDateTime = default(DateTime);
        }
    }
}



