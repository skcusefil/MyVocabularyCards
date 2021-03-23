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
    public partial class QuizPage : ContentPage
    {
        VocabularyDB _connection;
        NavigationService _page;
        public QuizPage()
        {
            var connection = new VocabularyDB(DependencyService.Get<ISqliteConnection>());
            var page = new NavigationService();

            _connection = connection;
            _page = page;

            InitializeComponent();
            ViewModel = new QuizViewModel(connection,page);
        }

        QuizViewModel ViewModel
        {
            get { return BindingContext as QuizViewModel; }
            set { BindingContext = value; }
        }

        private void CollectionList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.SelectedCommand.Execute(e.CurrentSelection);
        }

        protected override void OnDisappearing()
        {
            ViewModel = new QuizViewModel(_connection,_page);

            base.OnDisappearing();
        }

        protected override void OnAppearing()
        {
            ViewModel = new QuizViewModel(_connection, _page);

            base.OnAppearing();
        }
    }
}