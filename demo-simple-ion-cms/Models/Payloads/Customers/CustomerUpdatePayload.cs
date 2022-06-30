using System;
using System.ComponentModel.DataAnnotations;

namespace demo_simple_ion_cms.Models.Payloads.Customers
{
    public class CustomerUpdatePayload
    {
        [MaxLength(100)]
        public string FirstName { get; set; }
        
        [MaxLength(100)]
        public string LastName { get; set; }
        
        public DateTime? DOB { get; set; }
    }
}