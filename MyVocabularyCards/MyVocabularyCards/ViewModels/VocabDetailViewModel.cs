using MyVocabularyCards.Models;
using MyVocabularyCards.PersistenceDB;
using MyVocabularyCards.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyVocabularyCards.ViewModels
{
    public class VocabDetailViewModel
    {
        private readonly IVocabularyDB _vocabularyConnection;
        private readonly INavigationService _pageNavigation;

        public Vocabulary Vocabulary { get; private set; }

        public ICommand SaveCommand { get; private set; }

        public VocabDetailViewModel(VocabularyViewModel viewModel, IVocabularyDB vocabularyConnection, INavigationService pageNavigation)
        {
            if (viewModel == null)
                throw new AggregateException(nameof(viewModel));
            this._vocabularyConnection = vocabularyConnection;
            this._pageNavigation = pageNavigation;

            SaveCommand = new Command(async () => await Save());

            Vocabulary = new Vocabulary
            {
                ID = viewModel.ID,
                Word = viewModel.Word,
                Meaning = viewModel.Meaning
            };

        }

        private async Task Save()
        {
            if (string.IsNullOrWhiteSpace(Vocabulary.Word) && string.IsNullOrWhiteSpace(Vocabulary.Meaning))
            {
                await _pageNavigation.DisplayAlert("Oops!", "You must write a word!", "OK");
                return;
            }

            if (Vocabulary.ID == 0)
            {
                await this._vocabularyConnection.AddOrVocabulary(Vocabulary);
                MessagingCenter.Send(this, MessagingMenagment.VocabularyAdded, Vocabulary);
            }
            else
            {
                await this._vocabularyConnection.UpdateVocabulary(Vocabulary);
                MessagingCenter.Send(this, MessagingMenagment.VocabularyUpdated, Vocabulary);
            }

            await _pageNavigation.PopAsync();
        }
    }
}
