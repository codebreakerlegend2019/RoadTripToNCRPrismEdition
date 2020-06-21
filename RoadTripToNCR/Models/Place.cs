using System;
using System.Collections.Generic;
using System.Text;

namespace RoadTripToNCR.Models
{
    public class Place
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

        public string Trivia { get; set; }
        public string City { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string FileName { get; set; }

        public string ImageUrl => $"{App.ApiLinkImages}{FileName}";
        public bool IsLiked { get; set; }
    }
}
