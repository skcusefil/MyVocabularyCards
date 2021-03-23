using MyVocabularyCards.PersistenceDB;
using MyVocabularyCards.Services;
using MyVocabularyCards.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyVocabularyCards.ViewModels
{
    public class StartLearnCardViewModel
    {
        private readonly INavigationService _pageNavigation;

        public ICommand NavigateToLearnCardPage { get; private set; }

        public StartLearnCardViewModel(INavigationService pageNavigation)
        {
            _pageNavigation = pageNavigation;

            NavigateToLearnCardPage = new Command(async () => 
            {
                var learnCard = new LearnCardPage();
                await _pageNavigation.PushAsync(learnCard);
            });
        }
    }
}
