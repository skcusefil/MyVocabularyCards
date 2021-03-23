using MyVocabularyCards.Models;
using MyVocabularyCards.PersistenceDB;
using MyVocabularyCards.Services;
using MyVocabularyCards.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyVocabularyCards.ViewModels
{
    public class VocabPageViewModel : BaseViewModel
    {
        #region Fields

        private VocabularyViewModel _selectedVocab;
        private IVocabularyDB _vocabularyConnection;
        private INavigationService _pageNavigation;
        private ObservableCollection<VocabularyViewModel> _vocabularyList;
        private Color _color;
        #endregion Fields

        #region Constructors

        public Color selectedColor
        {
            get => _color;
            set => SetProperty(ref _color, value);
        }

        public VocabPageViewModel(IVocabularyDB vocabularyConnection, INavigationService pageNavigation)
        {
            this._vocabularyConnection = vocabularyConnection;
            this._pageNavigation = pageNavigation;

            //Commands, Message subscribe
            InitializeCommand();
            MessageSubscribe();

        }

        #endregion Constructors


        //public ObservableCollection<VocabularyViewModel> VocabularyList { get; private set; } =
        //new ObservableCollection<VocabularyViewModel>(); 
        #region Properties

        public ObservableCollection<VocabularyViewModel> VocabularyList
        {
            get => _vocabularyList;
            set
            {
                SetProperty(ref _vocabularyList, value);
            }
        }

        public VocabularyViewModel SelectedVocab
        {
            get => _selectedVocab;
            set
            {
                SetProperty(ref _selectedVocab, value);
                SelectVocab(value);
            }
        }

        #endregion Properties

        #region Commands

        public ICommand LoadDataCommand { get; private set; }
        public ICommand AddVocabCommand { get; private set; }
        public ICommand SelectVocabCommand { get; private set; }
        public ICommand DeleteVocabCommand { get; private set; }
        public ICommand SearchVocabCommand { get; private set; }

        #endregion Commands


        #region Methods

        private void MessageSubscribe()
        {
            MessagingCenter.Subscribe<VocabDetailViewModel, Vocabulary>(this, MessagingMenagment.VocabularyAdded, OnVocabAdded);
            MessagingCenter.Subscribe<VocabDetailViewModel, Vocabulary>(this, MessagingMenagment.VocabularyUpdated, OnVocabUpdated);
        }

        private void InitializeCommand()
        {
            LoadDataCommand = new Command(async w => await LoadData(string.Empty));
            AddVocabCommand = new Command(async () => await AddVocab());
            SelectVocabCommand = new Command<VocabularyViewModel>(async s => await SelectVocab(s));
            DeleteVocabCommand = new Command<VocabularyViewModel>(async d => await DeleteVocab(d));
            SearchVocabCommand = new Command<string>(async w => await LoadData(w));
        }


        private void OnVocabUpdated(VocabDetailViewModel source, Vocabulary vocabulary)
        {
            var vocabInList = VocabularyList.Single(c => c.ID == vocabulary.ID);

            vocabInList.ID = vocabulary.ID;
            vocabInList.Word = vocabulary.Word;
            vocabInList.Meaning = vocabulary.Meaning;
        }

        private void OnVocabAdded(VocabDetailViewModel source, Vocabulary vocabulary)
        {
            VocabularyList.Add(new VocabularyViewModel(vocabulary));
        }

        private async Task DeleteVocab(VocabularyViewModel c)
        {
            VocabularyList.Remove(c);

            var vocab = await _vocabularyConnection.SearchVocabulary(c.ID);
            await _vocabularyConnection.DeleteVocabulary(vocab);
        }

        private async Task SelectVocab(VocabularyViewModel c)
        {
            if (c == null)
                return;

            //SelectedVocab = null;
            await _pageNavigation.PushAsync(new VocabularyDetailPage(c));

        }

        private async Task AddVocab()
        {
            await _pageNavigation.PushAsync(new VocabularyDetailPage(new VocabularyViewModel()));
        }

        private async Task LoadData(string keyword)
        {
            var vocabList = await _vocabularyConnection.GetVocabularyAsync(keyword);
            VocabularyList = new ObservableCollection<VocabularyViewModel>();
            foreach (var vocab in vocabList)
            {
                VocabularyList.Add(new VocabularyViewModel(vocab));
            }
        }

        #endregion Methods


    }
}
