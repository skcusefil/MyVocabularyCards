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
    public partial class VocabularyDetailPage : ContentPage
    {

        public VocabularyDetailPage(VocabularyViewModel viewModel)
        {
            var connection = new VocabularyDB(DependencyService.Get<ISqliteConnection>());
            var page = new NavigationService();
            Title = (viewModel.Word == null || viewModel.Meaning == null) ? "Add new word" : "Edit word";
            BindingContext = new VocabDetailViewModel(viewModel ?? new VocabularyViewModel(), connection, page);
            InitializeComponent();
        }
    }
}