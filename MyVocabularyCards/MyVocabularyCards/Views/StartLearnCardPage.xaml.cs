using MyVocabularyCards.Services;
using MyVocabularyCards.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyVocabularyCards.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartLearnCardPage : ContentPage
    {
        public StartLearnCardPage()
        {
            var page = new NavigationService();
            BindingContext = new StartLearnCardViewModel(page);
            InitializeComponent();
        }
    }
}