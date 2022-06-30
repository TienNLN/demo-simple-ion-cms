using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using demo_simple_ion_cms.Validators;

namespace demo_simple_ion_cms.Entities
{
    [Table(nameof(Customer))]
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100), Required]
        public string FirstName { get; set; }
        
        [MaxLength(100), Required]
        public string LastName { get; set; }
        
        public DateTime? DOB { get; set; }
        
        public DateTime CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        
        
        public List<FavouriteFood> FavouriteFoods { get; set; }
    }
}