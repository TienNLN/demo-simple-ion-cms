using System.Collections.Generic;

namespace demo_simple_ion_cms.Models.Datas.FavouriteFoods
{
    public class FavouriteFoodCreateRangeDataModel
    {
        public int CustomerId { get; set; }
        
        public List<string> FavouriteFoodNames { get; set; }
    }
}