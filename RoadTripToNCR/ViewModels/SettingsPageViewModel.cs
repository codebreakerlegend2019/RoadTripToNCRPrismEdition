using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
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
            var currentTheme = Application.Current.Properties["CurrentTheme"].ToString();
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
            if (cmdParams == "Light")
            {
                App.Current.UserAppTheme = OSAppTheme.Light;
                App.Current.Properties["CurrentTheme"] = OSAppTheme.Light.ToString();
                IsLightModeOn = true;
                IsDarkModeOn = false;
            }
            else
            {
                App.Current.UserAppTheme = OSAppTheme.Dark;
                App.Current.Properties["CurrentTheme"] = OSAppTheme.Dark.ToString();
                IsDarkModeOn = true;
                IsLightModeOn = false;
            }
            await Application.Current.SavePropertiesAsync();
            RaisePropertyChanged();
        }
       
    }
}
