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
using System.IO;
using MonkeyCache.SQLite;
using System.Threading.Tasks;

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

        protected override void OnInitialized()
        {
            InitializeComponent();
            Device.SetFlags(new[]
            {
                "SwipeView_Experimental",
                "CarouselView_Experimental",
                "RadioButton_Experimental",
                "IndicatorView_Experimental",
                "Shapes_Experimental",
                "AppTheme_Experimental"
            });
            AppThemeConfiguration();
            JdsClient.BaseAddress = ApiLink;
            MainPage = new MainPage();

        }

        private static void AppThemeConfiguration()
        {
            var isKeyExisted = App.Current.Properties.ContainsKey("CurrentTheme");
            if (!isKeyExisted)
                App.Current.Properties["CurrentTheme"] = OSAppTheme.Light.ToString();
            else
            {
                var cacheTheme = App.Current.Properties["CurrentTheme"].ToString();
                if (cacheTheme == "Light")
                    App.Current.UserAppTheme = OSAppTheme.Light;
                else
                    App.Current.UserAppTheme = OSAppTheme.Dark;
            }
        }

        protected override void OnStart()
        {
            Barrel.Create(OfflineDatabasePath());
            Barrel.ApplicationId = "LocalDb";
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
            containerRegistry.RegisterForNavigation<CustomShellPage,CustomShellPageViewModel>();
            containerRegistry.RegisterForNavigation<PlaceDetailPage, PlaceDetailPageViewModel>();
        }

        private string OfflineDatabasePath()
        {
            if (Device.RuntimePlatform == Device.iOS)
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..", "Library");

            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
        }
    }
}
