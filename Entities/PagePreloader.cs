using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIMock.Views;

namespace UIMock.Entities
{
    public static class PagePreloader
    {
        public static async Task PreloadPagesAsync()
        {
            // Navigate to each page and immediately return to cache the page
            var homePage = new HomePage();
            await App.Current.MainPage.Navigation.PushAsync(homePage, false);
            await App.Current.MainPage.Navigation.PopAsync(false);

            var qrPage = new QRCodePage();
            await App.Current.MainPage.Navigation.PushAsync(qrPage, false);
            await App.Current.MainPage.Navigation.PopAsync(false);

            var profilePage = new ProfilePage();
            await App.Current.MainPage.Navigation.PushAsync(profilePage, false);
            await App.Current.MainPage.Navigation.PopAsync(false);
        }
    }
}
