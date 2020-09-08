using System;
using System.Collections.Generic;
using System.Linq;
using Ecommerce.Ecommerce.Helpers.ValidationAttributes;
using Ecommerce.Ecommerce.Entities;

namespace Ecommerce.Ecommerce.Entities
{
    /// <summary>
    /// Interface for Address Entity
    /// </summary>
    public interface IAddress
    {
        Guid AddressID { get; set; }
        string AddressLine1 { get; set; }
        string AddressLine2 { get; set; }
        string Landmark { get; set; }
        string City { get; set; }
        string State { get; set; }
        string PinCode { get; set; }
        Guid CustomerID { get; set; }
        DateTime CreationDateTime { get; set; }
        DateTime LastModifiedDateTime { get; set; }
    }

    /// <summary>
    /// Represents Address
    /// </summary>
    public class Address : IAddress
    {
        /* Auto-Implemented Properties */
        [Required("Address ID can't be blank.")]
        public Guid AddressID { get; set; }

        [Required("Address Line 1 can't be blank.")]
        [RegExp(@"^(\w{2,40})$", "Address Line 2 should contain House No. and Flat Number.")]
        public string AddressLine1 { get; set; }

        [Required("Address Line 2 can't be blank.")]
        [RegExp(@"^(\w{2,40})$", "Address Line 2 should contain Society, Village")]
        public string AddressLine2 { get; set; }

        [RegExp(@"^(\w{2,40})$", "Landmark Should Contain Nearest known Place.")]
        public string Landmark { get; set; }

        [Required("City Name can't be blank.")]
        [RegExp(@"^(\w{2,40})$", "City Name.")]
        public string City { get; set; }

        [Required("State Name can't be blank.")]
        [RegExp(@"^(\w{2,40})$", "StateName.")]
        public string State { get; set; }

        [Required("PinCode cannot be blank.")]
        public string PinCode { get; set; }

        public Guid CustomerID { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime LastModifiedDateTime { get; set; }

        /* Constructor */
        public Address()
        {
            AddressID = default(Guid);
            AddressLine1 = null;
            AddressLine2 = null;
            Landmark = null;
            City = null;
            State = null;
            PinCode = null;
            CreationDateTime = default(DateTime);
            LastModifiedDateTime = default(DateTime);
        }
    }
}



