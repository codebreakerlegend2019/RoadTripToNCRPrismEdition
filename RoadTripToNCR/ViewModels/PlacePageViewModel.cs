using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Navigation.Xaml;
using RoadTripToNCR.Interfaces;
using RoadTripToNCR.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RoadTripToNCR.ViewModels
{
    public class PlacePageViewModel : ViewModelBaseWithNavigateToSettings
    {
        private readonly INavigationService _navigationService;
        private readonly IGetAllAsync<Place> _placeRepo;
        private readonly IGetAll<City> _cityRepo;
        private readonly IGetAll<Category> _categoryRepo;
        public List<City> Cities { get; set; }
        public List<Category> Categories { get; set; }
        public List<Place> Places { get; set; }

        public PlacePageViewModel(INavigationService navigationService, 
            IGetAllAsync<Place> placeRepo, 
            IGetAll<City> cityRepo,
            IGetAll<Category> categoryRepo)
            :base(navigationService)
        {
            this._navigationService = navigationService;
            this._placeRepo = placeRepo;
            this._cityRepo = cityRepo;
            this._categoryRepo = categoryRepo;
            Title = "Places";
            Cities = _cityRepo.GetAll();
            Categories = _categoryRepo.GetAll();
            LoadPlaceCommand.Execute();
            
        }
        public DelegateCommand LoadPlaceCommand => new DelegateCommand(LoadPlaces);
        
        private async void LoadPlaces()
        {
            var places = await _placeRepo.GetAllAsync();
            Places = places;
            RaisePropertyChanged(nameof(Places));
        }
    
    }
}
