using MyVocabularyCards.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyVocabularyCards.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExitPage : ContentPage
    {
        public ExitPage()
        {
            InitializeComponent();
            ExitAppCommand = new Command(()=> {
                DependencyService.Get<IExitService>().ExitApp();

            });
        }

        public ICommand ExitAppCommand { get; private set; }
    }
}