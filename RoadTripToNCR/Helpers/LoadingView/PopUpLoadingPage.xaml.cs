using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RoadTripToNCR.Helpers.LoadingView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopUpLoadingPage : PopupPage
    {
        public PopUpLoadingPage(string lottieAnimName, string caption)
        {
            InitializeComponent();
            if (caption.Trim() == string.Empty)
                Caption.IsVisible = false;
            else
                Caption.Text = caption;
            AnimView.Animation = lottieAnimName;
        }
    }
}