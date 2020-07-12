using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using RoadTripToNCR.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;

namespace RoadTripToNCR.ViewModels
{
    [QueryProperty(nameof(Content), nameof(Content))]
    public class PlaceDetailPageViewModel : ViewModelBase
    {
        public Place SelectedPlace { get; set; }

        string content = "";
        public string Content 
        {
            get => content;
            set
            {
                SetProperty(ref content, Uri.UnescapeDataString(value ?? string.Empty));
                RaisePropertyChanged();
                SelectedPlace = JsonConvert.DeserializeObject<Place>(content);
                RaisePropertyChanged(nameof(SelectedPlace));
            }
        }
        
        public PlaceDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {

        }
        public DelegateCommand GoBackCommand =>new DelegateCommand(async()=> 
        {
            await Shell.Current.Navigation.PopAsync();
        });

    }
}
