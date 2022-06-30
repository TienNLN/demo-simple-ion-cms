using System;

namespace demo_simple_ion_cms.Models.Responses.FavouriteFoods
{
    public class FavouriteFoodDTO
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public DateTime CreatedTime { get; set; }
        
        public int CustomerId { get; set; }
    }
}