using MyVocabularyCards.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyVocabularyCards.ViewModels
{
    public class VocabularyViewModel : BaseViewModel
    {
        public int ID { get; set; }

        private string _word;
        public string Word
        {
            get => _word;
            set
            {
                SetProperty(ref _word, value);
                OnPropertyChanged(nameof(WordMeaning));
            }
        }

        private string _meaning;
        public string Meaning
        {
            get => _meaning;
            set
            {
                SetProperty(ref _meaning, value);
                OnPropertyChanged(nameof(WordMeaning));
            }
        }

        public string WordMeaning
        {
            get => $"{Word} - {Meaning}";
        }

        public VocabularyViewModel() { }

        public VocabularyViewModel(Vocabulary vocabulary)
        {
            ID = vocabulary.ID;
            Word = vocabulary.Word;
            Meaning = vocabulary.Meaning;
        }
    }
}
