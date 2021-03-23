using MyVocabularyCards.Models;
using MyVocabularyCards.PersistenceDB;
using MyVocabularyCards.Services;
using MyVocabularyCards.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MyVocabularyCards.ViewModels
{
    class QuizViewModel : BaseViewModel
    {
        #region Fields

        private readonly IVocabularyDB _vocabularyConnection;
        private readonly INavigationService _pageNavigation;

        private string buttonText = "Start";
        private string randomWord;
        private ObservableCollection<Vocabulary > listChoice;
        private string correctAnswer;
        private string correctOrFalse = "";
        private Vocabulary selectedAnswer = new Vocabulary();
        private string cardStartBtn = "Start";
        private string previousRandom { get; set; }

        #endregion

        #region Properties

        public string CardStartBtn
        {
            get => cardStartBtn;
            set => SetProperty(ref cardStartBtn, value);
        }

        public string CorrectOrFalse
        {
            get => correctOrFalse ?? string.Empty;
            set
            {
                SetProperty(ref correctOrFalse, value);
            }

        }

        public Vocabulary SelectedAnswer 
        {
            get => selectedAnswer;
            set
            {
                SetProperty(ref selectedAnswer, value);
                SelectedItem(value);
            }
        }

        public string CorrectAnswer
        {
            get => correctAnswer;
            set => SetProperty(ref correctAnswer, value);
        }

        private string getAnswer;
        public string GetAnswer
        {
            get => getAnswer;
            set => SetProperty(ref getAnswer, value);
        }

        public string ButtonText
        {
            get => buttonText;
            set => SetProperty(ref buttonText, value);
        }

        public ObservableCollection<Vocabulary> ListChoice
        {
            get => listChoice;
            set => SetProperty(ref listChoice, value);
        }

        public string RandomWord
        {
            get => randomWord;
            set => SetProperty(ref randomWord, value);
        }

        #endregion

        #region Commands
        public ICommand RandomeWordCommand { get; private set; }
        public ICommand ChangeButtonText { get; private set; }
        public ICommand CheckAnswerCommand { get; private set; }
        public ICommand SelectedCommand { get; private set; }
        public ICommand CardStartCommand { get; private set; }
        public ICommand GetAnswerCommand { get; private set; }
        public ICommand SoundCommand { get; private set; }
        #endregion

        #region Methods

        #region Constructor

        public QuizViewModel(IVocabularyDB vocabularyConnection, INavigationService pageNavigation)
        {
            _vocabularyConnection = vocabularyConnection;
            _pageNavigation = pageNavigation;

           _ = GetRandomWord();
            InitializeCommand();
        }

        #endregion

        private void InitializeCommand()
        {
            RandomeWordCommand = new Command(async () => await GetRandomWord());
            //ChangeButtonText = new Command(async () => await ChangTextAsync());
            SelectedCommand = new Command<Vocabulary>(SelectedItem);
            CheckAnswerCommand = new Command(GetCorrectAnswerQuiz);

            GetAnswerCommand = new Command(GetCorrectAnswerCard);
            SoundCommand = new Command(GetSound);
        }


        private async void GetSound(object obj)
        {
            await TextToSpeech.SpeakAsync(randomWord);
        }

        private void GetCorrectAnswerQuiz()
        {
            if(RandomWord!=""&& correctAnswer!="")
            {
                CorrectOrFalse = RandomWord + " = " + correctAnswer;

            }
            else
            {
                CorrectOrFalse = "";
            }
        }

        private void GetCorrectAnswerCard(object obj)
        {
            GetAnswer = correctAnswer;
        }

        private async Task CheckAnswer()
        {
            string FalseMesseage = RandomWord + " = " + correctAnswer;

            if (CorrectAnswer != null && SelectedAnswer.Meaning != null)
            {
                CorrectOrFalse = (selectedAnswer.Meaning == correctAnswer && correctAnswer != null) ? "Correct!" : FalseMesseage;
            }
            else if(RandomWord == null)
            {
                await GetRandomWord();
                //await ChangTextAsync();
                //CorrectOrFalse = RandomWord + " = " + correctAnswer;
            }
        }

        private async void SelectedItem(Vocabulary vocabulary)
        {
         
            await CheckAnswer();

        }


        //private async Task ChangTextAsync()
        //{
        //    //After click start button text will be changed to "Next"
        //    ButtonText = "Next";
        //    CardStartBtn = "OK";
        //    await GetRandomWord();
        //    //CorrectOrFalse = string.Empty;
        //    //GetAnswer = string.Empty;
        //}

        private async Task GetRandomWord()
        {
            //test
            CorrectOrFalse = string.Empty;
            GetAnswer = string.Empty;
            RandomWord = string.Empty;
            CorrectAnswer = string.Empty;

            var vocab = await _vocabularyConnection.GetVocabularyAsync(string.Empty);
            var listVocab = (List<Vocabulary>)vocab;
            int randomWordIndex = 0;
            string correctMeaning = "";

            if (listVocab.Count != 0)
            {
                if (listVocab.Count > 1)
                {
                    do
                    {
                        randomWordIndex = new Random().Next(0, listVocab.Count);

                        var word = listVocab[randomWordIndex].Word;
                        var answer = listVocab[randomWordIndex].Meaning;
                        correctMeaning = listVocab[randomWordIndex].Meaning;

                        //Here get random word as question
                        RandomWord = word;

                        //Hier is the correct answer
                        CorrectAnswer = answer;

                    } while (RandomWord == previousRandom);
                }
                else
                {
                    randomWordIndex = new Random().Next(0, listVocab.Count);

                    var word = listVocab[randomWordIndex].Word;
                    var answer = listVocab[randomWordIndex].Meaning;
                    correctMeaning = listVocab[randomWordIndex].Meaning;

                    //Here get random word as question
                    RandomWord = word;

                    //Hier is the correct answer
                    CorrectAnswer = answer;
                }


                //var randomWord = new Random().Next(0, listVocab.Count);

                //var word = listVocab[randomWord].Word;
                //var answer = listVocab[randomWord].Meaning;
                //var correctMeaning = listVocab[randomWord].Meaning;

                ////Here get random word as question
                //RandomWord = word;

                ////Hier is the correct answer
                //CorrectAnswer = answer;

                var listChoice = new List<string>();
                var ChoiceIndex = new List<int>();

                ChoiceIndex.Add(randomWordIndex);

                listChoice.Add(correctMeaning);

                var count = listVocab.Count;
                var maxChoice = 0;

                if (count > 3 && count > 0)
                {
                    maxChoice = 4;
                }
                else
                {
                    maxChoice = count;
                }

                for (int i = 0; i < maxChoice - 1; i++)
                {
                    var randomChoiceIndex = new Random().Next(0, listVocab.Count);
                    if (randomChoiceIndex == randomWordIndex || ChoiceIndex.Contains(randomChoiceIndex))
                    {
                        i--;
                    }
                    else
                        ChoiceIndex.Add(randomChoiceIndex);

                }

                //suffer
                var sufferList = new List<int>();

                for (int i = 0; i < ChoiceIndex.Count; i++)
                {
                    var suffer = new Random().Next(0, ChoiceIndex.Count);
                    if (sufferList.Contains(ChoiceIndex[suffer]))
                    {
                        i--;
                    }
                    else
                    {
                        sufferList.Add(ChoiceIndex[suffer]);
                    }

                    ListChoice = new ObservableCollection<Vocabulary>();

                    foreach (var item in sufferList)
                    {
                        ListChoice.Add(listVocab[item]);
                    }

                    previousRandom = RandomWord;
                }

            }
            else
            {
                CorrectOrFalse = string.Empty;

            }

        }

        #endregion
    }
}
