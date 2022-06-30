using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using demo_simple_ion_cms.Models.Responses.FavouriteFoods;

namespace demo_simple_ion_cms.Models.Responses
{
    public class CustomerDTO
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string FirstName { get; set; }
        
        [MaxLength(100)]
        public string LastName { get; set; }
        
        public DateTime? DOB { get; set; }
        
        public DateTime CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        
        public List<FavouriteFoodDTO> FavouriteFoods { get; set; }
    }
}