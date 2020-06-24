using ImTools;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Navigation.Xaml;
using RoadTripToNCR.Interfaces;
using RoadTripToNCR.Models;
using RoadTripToNCR.Themes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
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

        private List<City> _cities;
        public List<City> Cities
        {
            get => _cities;
            set 
            {
                _cities = value;
                RaisePropertyChanged(nameof(Cities));
            }
        }
        private List<Category> _categories;
        public List<Category> Categories 
        {
            get => _categories;
            set { _categories = value; }
        }
        public List<Place> Places { get; set; }
        private List<Place> _places { get; set; }
        private City _selectedCity;
        public City SelectedCity
        {
            get => _selectedCity;
            set { _selectedCity = value; }
        }

        private Category _selectedCategory;
        public Category SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
            }
        }
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
            Title = "Find Your Destination";
            var cities= _cityRepo.GetAll();
            Cities =  cities;
            Categories = _categoryRepo.GetAll();
            LoadPlaceCommand.Execute();
            
        }

      
        public DelegateCommand LoadPlaceCommand => new DelegateCommand(LoadPlaces);

        private void UpdateCities()
        {
            _selectedCity.IsSelected = true;

            RaisePropertyChanged(nameof(Cities));
            foreach (var city in _cities)
            {
                if (city.IsSelected && city.Name != _selectedCity.Name)
                    city.IsSelected = false; 
                RaisePropertyChanged(nameof(Cities));
            }
            RaisePropertyChanged(nameof(SelectedCity));
        }

   
        public DelegateCommand CitySelectionChangedCommand => new DelegateCommand(async() =>
        {
            if  (Places.Count > 0)
            {
                UpdateCities();
                Places = _places.Where(x => x.City == SelectedCity.FilterName)
                .OrderBy(x => x.Name)
                .ToList();
                RaisePropertyChanged(nameof(Places));
            }
            else
            {
                SelectedCity = null;
                RaisePropertyChanged(nameof(SelectedCity));
                await App.Current.MainPage.DisplayAlert("Info", "Places are still Loading", "OK");
            }
        });

        private void UpdateCategories()
        {
            _selectedCategory.IsSelected = true;

            foreach (var category in _categories)
            {
                if (category.IsSelected && category.Name != _selectedCategory.Name)
                    category.IsSelected = false;
            }
            RaisePropertyChanged(nameof(Categories));
            RaisePropertyChanged(nameof(SelectedCategory));
        }
        public DelegateCommand CategorySelectionChangeCommand => new DelegateCommand( async() =>
        {
            if(SelectedCity!=null && Places.Count>0)
            {
                UpdateCategories();
                Places = _places
                .Where(x => x.City == SelectedCity.FilterName && x.Type == SelectedCategory.FilterName)
                .OrderBy(x => x.Name)
                .ToList();
                RaisePropertyChanged(nameof(Places));
            }
            else
            {
                SelectedCategory = null;
                RaisePropertyChanged(nameof(SelectedCategory));
                await App.Current.MainPage.DisplayAlert("Info", "Please Select City First", "OK");
            }

        });


        private async void LoadPlaces()
        {
            var places = await _placeRepo.GetAllAsync();
            _places = places;
            Places = _places;
            RaisePropertyChanged(nameof(Places));
        }
    
    }
}
