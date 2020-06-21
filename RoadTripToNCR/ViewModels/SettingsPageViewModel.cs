using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using RoadTripToNCR.Themes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;

namespace RoadTripToNCR.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public new string Title { get; set; }
        public bool IsDarkModeOn { get; set; }

        public bool IsLightModeOn { get; set; }
        public SettingsPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Settings";
            var currentTheme = (App.Current.Resources.MergedDictionaries.ToList()).FirstOrDefault().ToString();
            IsDarkModeOn = (currentTheme == "RoadTripToNCR.Themes.DarkTheme");
            IsLightModeOn = (currentTheme == "RoadTripToNCR.Themes.LightTheme");
            this._navigationService = navigationService;
        }
        public DelegateCommand<string> ChangeThemeCommand => new DelegateCommand<string>(ChangeTheme);

      
        private void ChangeTheme(string cmdParams)
        {
            var mergedDictionaries = App.Current.Resources.MergedDictionaries;
            mergedDictionaries.Clear();
            if (cmdParams == "Light")
            {
                mergedDictionaries.Add(new LightTheme());
                IsDarkModeOn = false;
                IsLightModeOn = true;
            }
            else
            {
                mergedDictionaries.Add(new DarkTheme());
                IsDarkModeOn = true;
                IsLightModeOn = false;
            }
            RaisePropertyChanged();
        }
       
    }
}
