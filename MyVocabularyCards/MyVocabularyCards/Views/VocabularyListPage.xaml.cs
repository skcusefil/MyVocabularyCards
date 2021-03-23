using MyVocabularyCards.PersistenceDB;
using MyVocabularyCards.Services;
using MyVocabularyCards.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyVocabularyCards.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VocabularyListPage : ContentPage
    {
        public VocabularyListPage()
        {
            var connection = new VocabularyDB(DependencyService.Get<ISqliteConnection>());
            var page = new NavigationService();
            ViewModel = new VocabPageViewModel(connection, page);
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            ViewModel.LoadDataCommand.Execute(null);
            base.OnAppearing();
        }

        public VocabPageViewModel ViewModel
        {
            get { return BindingContext as VocabPageViewModel; }
            set { BindingContext = value; }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
        }
    }
}