using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using RoadTripToNCR.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace RoadTripToNCR.ViewModels
{
    public class CustomShellPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _goToSettingsCommand;

        public CustomShellPageViewModel(INavigationService navigationService)
            :base(navigationService)
        {
            this._navigationService = navigationService;
        }
    }
}
