using MyVocabularyCards.Services;
using MyVocabularyCards.Views;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyVocabularyCards
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();

            //Setting first page
            tabBar.CurrentItem = vocabListPage;

            BindingContext = this;

        }

        public ICommand ExitAppCommand { get; private set; }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {
            // Quit application when click exit
            DependencyService.Get<IExitService>().ExitApp();
        }
    }
}
