using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AzureFinalAssignment.Models
{
    public class PromotionMessage
    {
        [Required]
        [MinLength(3)]
        public string UserName { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}
