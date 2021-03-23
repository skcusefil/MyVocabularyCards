using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyVocabularyCards.Services
{
    public class NavigationService : INavigationService
    {
        //DisplayAlert
        public async Task<bool> DisplayAlert(string title, string message, string ok, string cancel)
        {
            return await MainPage.DisplayAlert(title, message, ok, cancel);
        }

        //DisplayAlert
        public async Task DisplayAlert(string title, string message, string ok)
        {
            await MainPage.DisplayAlert(title, message, ok);
        }

        //Navigate to page
        public async Task<Page> PopAsync()
        {
            return await MainPage.Navigation.PopAsync();
        }

        //Navigate back
        public async Task PushAsync(Page page)
        {
            await MainPage.Navigation.PushAsync(page);
        }

        //Property for getting main page
        #region Property
        private Page MainPage
        {
            get { return Application.Current.MainPage; }
        }
        #endregion Property
    }
}
