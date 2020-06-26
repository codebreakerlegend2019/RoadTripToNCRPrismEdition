using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoadTripToNCR.Models
{
    public class Place:BindableBase
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
        private bool _isLike;
        public bool IsLiked
        {
            get => _isLike;
            set
            {
                SetProperty(ref _isLike, value);
                RaisePropertyChanged(nameof(LikedColor));
            }
        }
        public string LikedColor => IsLiked ? "Red" : "Gray";
    }
}
