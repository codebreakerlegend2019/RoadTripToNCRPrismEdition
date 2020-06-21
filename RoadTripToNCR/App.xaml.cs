using Prism;
using Prism.Ioc;
using RoadTripToNCR.ViewModels;
using RoadTripToNCR.Views;
using Xamarin.Essentials.Interfaces;
using Xamarin.Essentials.Implementation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RoadTripToNCR.Helpers;
using RoadTripToNCR.Helpers.JdsClientTool;
using Prism.Mvvm;
using System;
using ImTools;
using Prism.Navigation.Xaml;
using Prism.Navigation;
using System.Diagnostics;
using RoadTripToNCR.Themes;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace RoadTripToNCR
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */

        public static readonly string ApiLink = "https://jdscoding.000webhostapp.com/";
        public static readonly string ApiLinkImages = "https://jdscoding.000webhostapp.com/images/";
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            Device.SetFlags(new[]
          {
                "SwipeView_Experimental",
                "CarouselView_Experimental",
                "RadioButton_Experimental",
                "IndicatorView_Experimental",
            });
            if (App.Current.Resources.Source.OriginalString == "Themes/DarkTheme.xaml")
                App.Current.Resources.MergedDictionaries.Add(new DarkTheme());
            else
                App.Current.Resources.MergedDictionaries.Add(new LightTheme());
            JdsClient.BaseAddress = ApiLink;
            var result = await NavigationService.NavigateAsync("NavigationPage/PlacePage");
            if (!result.Success)
                Debug.WriteLine(result.Exception.StackTrace);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<PlacePage, PlacePageViewModel>();
            containerRegistry.AutoRegisterByInterFaceName("IGetAll");
            containerRegistry.AutoRegisterByInterFaceName("IGetAllAsync");
            containerRegistry.RegisterForNavigation<SettingsPage, SettingsPageViewModel>();
            containerRegistry.RegisterForNavigation<CustomShellPage>();
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();
            ViewModelLocationProvider.Register<CustomShellPage, CustomShellPageViewModel>();
        }
    }
}
