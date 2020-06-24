using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoadTripToNCR.Models
{
    public class City:BindableBase
    {
        public string Name { get; set; }
        public string FilterName => $"{Name.ToLower()} city";
        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                SetProperty(ref _isSelected, value);
                RaisePropertyChanged(nameof(SelectedColor));
            }
        }
        public string SelectedColor => (IsSelected) ? "#00ACC1" : "#80DEEA";
    }
}
