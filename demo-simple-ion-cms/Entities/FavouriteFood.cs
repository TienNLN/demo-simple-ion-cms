using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace demo_simple_ion_cms.Entities
{
    [Table(nameof(FavouriteFood))]
    [Index(nameof(Name), IsUnique = true)]
    public class FavouriteFood
    {
        [Key]
        public int Id { get; set; }
        
        [MaxLength(100), Required]
        public string Name { get; set; }
        
        public DateTime CreatedTime { get; set; }
        
        public int CustomerId { get; set; }
        
        
        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; }
    }
}