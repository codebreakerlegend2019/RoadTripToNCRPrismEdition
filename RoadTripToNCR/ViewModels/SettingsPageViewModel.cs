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
            var currentTheme = Application.Current.Properties["Theme"].ToString();
            IsDarkModeOn = (currentTheme == "Dark");
            IsLightModeOn = (currentTheme == "Light");
            this._navigationService = navigationService;
        }
        public DelegateCommand<string> ChangeThemeCommand => new DelegateCommand<string>(ChangeTheme);
        public DelegateCommand PopCommand => new DelegateCommand(Pop);

        private async void Pop()
        {
            await Shell.Current.Navigation.PopModalAsync();
        }

        private async void ChangeTheme(string cmdParams)
        {
            var mergedDictionaries = App.Current.Resources.MergedDictionaries;
            mergedDictionaries.Clear();
            if (cmdParams == "Light")
            {
                mergedDictionaries.Add(new LightTheme());
                Application.Current.Properties["Theme"] = "Light";
                IsDarkModeOn = false;
                IsLightModeOn = true;
            }
            else
            {
                mergedDictionaries.Add(new DarkTheme());
                Application.Current.Properties["Theme"] = "Dark";
                IsDarkModeOn = true;
                IsLightModeOn = false;
            }
            await Application.Current.SavePropertiesAsync();
            RaisePropertyChanged();
        }
       
    }
}
