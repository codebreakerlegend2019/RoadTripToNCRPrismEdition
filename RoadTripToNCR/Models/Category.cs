using Prism.Mvvm;
using RoadTripToNCR.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace RoadTripToNCR.Models
{
    public class Category:BindableBase
    {
        public string Name { get; set; }
        public string FilterName => Name.ToLower();
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
        public Color SelectedColor => IsSelected ? UIHelper.GetFrameSelectedColor(true) : UIHelper.GetFrameSelectedColor(false);
    }
}
