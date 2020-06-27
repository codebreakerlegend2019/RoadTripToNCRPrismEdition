using ImTools;
using Rg.Plugins.Popup.Services;
using RoadTripToNCR.Helpers.LoadingView;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RoadTripToNCR.Helpers
{
    public static class UIHelper
    {
        [Obsolete]
        public static async Task Load(Task task, string loadingCaption)
        {
            var loadingPage = new PopUpLoadingPage("defaultLoading.json", loadingCaption);
            await PopupNavigation.PushAsync(loadingPage);
            await task;
            await PopupNavigation.PopAllAsync();
        }
        [Obsolete]
        public static async Task Load(Task task, string lottieAnimationFileName, string loadingCaption)
        {
            var loadingPage = new PopUpLoadingPage(lottieAnimationFileName, loadingCaption);
            await PopupNavigation.PushAsync(loadingPage);
            await task;
            await PopupNavigation.PopAllAsync();
        }

        public static Color GetFrameSelectedColor(bool isSelected)
        {
            if (App.Current.UserAppTheme == OSAppTheme.Dark)
                if (isSelected)
                    return (Color)App.Current.Resources["Dark_FrameSelectedColor"];
                else
                    return (Color)App.Current.Resources["Dark_FrameBackgroundColor"];
            else
                if (isSelected)
                    return (Color)App.Current.Resources["Light_FrameSelectedColor"];
                else
                    return (Color)App.Current.Resources["Light_FrameBackgroundColor"];
        }

        public static Color GetLikeUnlikeColor(bool isLiked)
        {
            if (App.Current.UserAppTheme == OSAppTheme.Dark)
                if (isLiked)
                    return Color.Red;
                else
                    return Color.White;
            else
                if (isLiked)
                    return Color.Red;
                else
                    return Color.White;
        }
    }
}
