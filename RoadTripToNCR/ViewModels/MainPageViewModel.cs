﻿using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using RoadTripToNCR.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace RoadTripToNCR.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
        }
    }
}
