﻿using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using RoadTripToNCR.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;

namespace RoadTripToNCR.ViewModels
{
    public class ViewModelBaseWithNavigateToSettings : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _goToSettingsCommand;
        public ViewModelBaseWithNavigateToSettings(INavigationService navigationService):base(navigationService)
        {
            this._navigationService = navigationService;
        }
        public DelegateCommand GoToSettingsCommand =>
       _goToSettingsCommand ?? (_goToSettingsCommand = new DelegateCommand(GoToSettings));

        private async void GoToSettings()
        {
            await Shell.Current.Navigation.PushAsync(new SettingsPage());
        }
    }
}
