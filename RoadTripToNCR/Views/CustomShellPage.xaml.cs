using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RoadTripToNCR.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]


    public partial class CustomShellPage : ContentPage
    {
        public static readonly BindableProperty TextProperty =
           BindableProperty.Create(nameof(Title), typeof(string), typeof(CustomShellPage), null);

        public static readonly BindableProperty IsSettingsButtonVisibleProperty =
           BindableProperty.Create(nameof(IsSettingsButtonVisible), typeof(bool), typeof(CustomShellPage), null);

        public bool IsSettingsButtonVisible
        {
            get { return (bool)GetValue(IsSettingsButtonVisibleProperty); }
            set { SetValue(IsSettingsButtonVisibleProperty, value); }
        }

        public CustomShellPage()
        {
            InitializeComponent();
            TitleLabel.SetBinding(Label.TextProperty, new Binding(nameof(this.Title), source: this));
            SettingButton.SetBinding(Button.IsVisibleProperty, new Binding(nameof(IsSettingsButtonVisible), source: this));
        }

    }
}