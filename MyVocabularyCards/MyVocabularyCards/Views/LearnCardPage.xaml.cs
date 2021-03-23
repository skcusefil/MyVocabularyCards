using MyVocabularyCards.PersistenceDB;
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
    public partial class LearnCardPage : ContentPage
    {
        VocabularyDB _connection;
        NavigationService _page;

        public LearnCardPage()
        {
            InitializeComponent();
            var connection = new VocabularyDB(DependencyService.Get<ISqliteConnection>());
            var page = new NavigationService();

            _connection = connection;
            _page = page;

            InitializeComponent();
            BindingContext = new QuizViewModel(connection, page);
        }

        protected override void OnDisappearing()
        {
            BindingContext = new QuizViewModel(_connection, _page);

            base.OnDisappearing();
        }

        protected override void OnAppearing()
        {
            BindingContext = new QuizViewModel(_connection, _page);

            base.OnAppearing();
        }
    }
}