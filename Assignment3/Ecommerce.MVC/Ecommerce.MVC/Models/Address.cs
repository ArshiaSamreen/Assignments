using Ecommerce.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.MVC.Model
{
    public class Address
    {
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
    }
}
