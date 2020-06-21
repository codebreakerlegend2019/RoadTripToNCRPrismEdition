using Rg.Plugins.Popup.Services;
using RoadTripToNCR.Helpers.LoadingView;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
    }
}
