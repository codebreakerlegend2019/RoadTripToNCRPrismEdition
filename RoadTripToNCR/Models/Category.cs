using System;
using System.Collections.Generic;
using System.Text;

namespace RoadTripToNCR.Models
{
    public class Category
    {
        public string Name { get; set; }
        public string FilterName => Name.ToLower();
    }
}
