using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using demo_simple_ion_cms.Validators;

namespace demo_simple_ion_cms.Models.Payloads
{
    public class CustomerCreatePayload
    {
        [MaxLength(100)]
        public string FirstName { get; set; }
        
        [MaxLength(100)]
        public string LastName { get; set; }
        
        public DateTime? DOB { get; set; }
        
        [IndexesNormalChars]
        public List<string> FavouriteFoodNames { get; set; }
    }
}