using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MyVocabularyCards.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        private readonly string address = "jira.applications@gmail.com";

        [Obsolete]
        public AboutViewModel()
        {
            //Title = "About";
            OpenWebCommand = new Command(() =>
            {
                Device.OpenUri(new Uri($"mailto:{address}"));
            });
        }

        public ICommand OpenWebCommand { get; }
    }
}