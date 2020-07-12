using ImTools;
using MonkeyCache.SQLite;
using Newtonsoft.Json;
using Plugin.Toast;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Navigation.Xaml;
using RoadTripToNCR.Interfaces;
using RoadTripToNCR.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using NavigationMode = Xamarin.Essentials.NavigationMode;

namespace RoadTripToNCR.ViewModels
{
    public class PlacePageViewModel : ViewModelBaseWithNavigateToSettings
    {
        private readonly INavigationService _navigationService;
        private readonly IGetAllAsync<Place> _placeRepo;
        private readonly IGetAll<City> _cityRepo;
        private readonly IGetAll<Category> _categoryRepo;
        public bool IsPlacesLoaded { get; set; }
        public bool IsPlaceStillLoading { get; set; }
        public bool IsNoPlaces { get; set; }

        public bool IsRefreshing { get; set; }

        private bool _isClearButtonVisible;
        public bool IsClearButtonVisible
        {
            get => _isClearButtonVisible;
            set
            {
                _isClearButtonVisible = value;
                RaisePropertyChanged();
            }
        }
        private string _keyWord;
        public string KeyWord
        {
            get => _keyWord ?? "";
            set
            {
                _keyWord = value;
                if (value == string.Empty)
                    _isClearButtonVisible = false;
                else
                    _isClearButtonVisible = true;
                filterPlaces(value.ToLower());
                RaisePropertyChanged(nameof(IsClearButtonVisible));
                RaisePropertyChanged();
            }
        }

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
        private Place _selectedPlace;
        public Place SelectedPlace
        {
            get => _selectedPlace;
            set 
            { _selectedPlace = value;
                RaisePropertyChanged();
            }
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
        private OSAppTheme _currentTheme;
        public PlacePageViewModel(INavigationService navigationService,
            IGetAllAsync<Place> placeRepo,
            IGetAll<City> cityRepo,
            IGetAll<Category> categoryRepo)
            : base(navigationService)
        {
            this._navigationService = navigationService;
            this._placeRepo = placeRepo;
            this._cityRepo = cityRepo;
            this._categoryRepo = categoryRepo;
            Title = "Find Your Destination";
            _currentTheme = App.Current.UserAppTheme;
            var cities = _cityRepo.GetAll();
            Cities = cities;
            Categories = _categoryRepo.GetAll();
            LoadPlaceCommand.Execute();

        }

        private async Task LoadItems()
        {
            var cities = _cityRepo.GetAll();
            Cities = cities;
            Categories = _categoryRepo.GetAll();
            await LoadPlaces();
        }

        private void foreachPlace(List<Place> placesToFilter,string searchData,List<Place> filteredPlaces)
        {
            foreach (var place in placesToFilter.Where(x => x.City == _selectedCity.FilterName).ToList())
            {
                var searchMatch = place.Name.ToLower().Contains(searchData)
                   || place.Trivia.ToLower().Contains(searchData);
                if (searchMatch)
                    filteredPlaces.Add(place);
            }
        }
        private void filterPlaces(string searchData)
        {
            if (_places != null)
            {
                var places = new List<Place>();
                if (_selectedCity != null && _selectedCategory==null)
                    foreachPlace(_places.Where(x => x.City == _selectedCity.FilterName).ToList(), searchData,places);
                else if (_selectedCity != null && _selectedCategory != null)
                    foreachPlace(_places.Where(x => x.City == _selectedCity.FilterName && x.Type == _selectedCategory.FilterName).ToList(), searchData, places);
                else
                    foreachPlace(_places, searchData, places);
                Places = places;
                RaisePropertyChanged(nameof(Places));
            }
        }
        public DelegateCommand ReloadCommand => new DelegateCommand(async () =>
         {
             IsRefreshing = true;
             RaisePropertyChanged(nameof(IsRefreshing));
             await LoadItems();
             IsRefreshing = false;
             RaisePropertyChanged(nameof(IsRefreshing));
         });
        public DelegateCommand LoadPlaceCommand => new DelegateCommand(async()=> 
        {
            await LoadPlaces();
        });

        public DelegateCommand ClearSearchCommand => new DelegateCommand(() =>
        {
            _keyWord = string.Empty;
            RaisePropertyChanged(nameof(KeyWord));
        });
        private void UpdateCities()
        {
            if(_selectedCity!=null)
            {
                foreach (var city in _cities)
                {
                    if (city.IsSelected && city.Name != _selectedCity.Name)
                        city.IsSelected = false;
                    else if (city.Name == _selectedCity.Name)
                        city.IsSelected = true;
                    else
                        city.IsSelected = false;
                }
            }
            else
                foreach (var city in _cities)
                    city.IsSelected = false;
        }

        private void LikeUnlike(Place placeSelected)
        {

            foreach (var place in _places)
            {
                if (place.Id == placeSelected.Id)
                {
                    if (!place.IsLiked)
                    {
                        CrossToastPopUp.Current.ShowToastMessage($"{place.Name} is added to Travel List");
                        place.IsLiked = true;
                    }
                    else
                    {
                        CrossToastPopUp.Current.ShowToastMessage($"{place.Name} is removed to Travel List");
                        place.IsLiked = false;
                    }
                    break;
                }
            }
        }


        public DelegateCommand CitySelectionChangedCommand => new DelegateCommand(async () =>
        {
            if (_places != null)
            {
                UpdateCities();
                if (_selectedCategory != null)
                {

                    Places = _places.Where(x => x.City == SelectedCity.FilterName && x.Type == _selectedCategory.FilterName)
                    .OrderBy(x => x.Name)
                    .ToList();
                    RaisePropertyChanged(nameof(Places));
                }
                else
                {
                    Places = _places.Where(x => x.City == SelectedCity.FilterName)
                   .OrderBy(x => x.Name)
                   .ToList();
                    RaisePropertyChanged(nameof(Places));
                }
            }
            else
            {
                SelectedCity = null;
                RaisePropertyChanged(nameof(SelectedCity));
                await App.Current.MainPage.DisplayAlert("Info", "Places are still Loading", "OK");
            }
        });

        public DelegateCommand PlaceSelectionChangeCommand => new DelegateCommand(async () =>
        {
            var jsonContent = JsonConvert.SerializeObject(_selectedPlace);
            await Shell.Current.GoToAsync($"/PlaceDetailPage?Content={jsonContent}");
            _selectedPlace = null;
        });

        private void UpdateCategories()
        {
            if(_selectedCategory !=null)
            {
                foreach (var category in _categories)
                {
                    if (category.IsSelected && category.Name != _selectedCategory.Name)
                        category.IsSelected = false;
                    else if(category.Name == _selectedCategory.Name)
                        category.IsSelected = true;
                    else
                        category.IsSelected = false;
                }
            }
            else
                foreach (var category in _categories)
                    category.IsSelected = false;
        }
        public DelegateCommand CategorySelectionChangeCommand => new DelegateCommand(async () =>
       {
           if (_selectedCity != null)
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
               CrossToastPopUp.Current.ShowToastMessage("Please Select City First");
               SelectedCategory = null;
               RaisePropertyChanged(nameof(SelectedCategory));
           }

       });


        public DelegateCommand<Place> LikeUnlikeCommand => new DelegateCommand<Place>((Place place) =>
        {
            LikeUnlike(place);
        });


        private async Task LoadPlaces()
        {
            try
            {
                IsPlaceStillLoading = true;
                var places = await _placeRepo.GetAllAsync();
                var likedPlaces = Barrel.Current.Get<List<Place>>("TravelList");
                foreach (var place in places)
                {
                    if (likedPlaces == null)
                        break;
                    var likedPlaceMatch = likedPlaces.FirstOrDefault(x => x.Id == place.Id);
                    if (likedPlaceMatch != null)
                        place.IsLiked = true;
                }
                _places = places;
                Places = _places;
                IsPlaceStillLoading = false;
                if (_places.Count > 0)
                    IsPlacesLoaded = true;
                else
                    IsNoPlaces = true;
                RaisePropertyChanged(nameof(IsPlacesLoaded));
                RaisePropertyChanged(nameof(IsNoPlaces));
                RaisePropertyChanged(nameof(IsPlaceStillLoading));
                RaisePropertyChanged(nameof(Places));
            }
            catch (Exception ex)
            {

            }
        }

        public DelegateCommand<Place> NavigateDestinationCommand => new DelegateCommand<Place>(async (Place place) =>
        {
            await Map.OpenAsync(place.Latitude, place.Longitude, new MapLaunchOptions()
            {
                Name = place.Name,
                NavigationMode = NavigationMode.Driving
            });
        });

        public DelegateCommand PageAppearingCommand => new DelegateCommand(()=>
        {
            var appTheme = App.Current.UserAppTheme;
            if (appTheme != _currentTheme)
            {
                _currentTheme = appTheme;
                UpdateCities();
                UpdateCategories();
            }
        });

    }
}
