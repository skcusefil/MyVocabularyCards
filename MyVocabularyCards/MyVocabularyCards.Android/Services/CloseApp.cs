using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MyVocabularyCards.Droid.Services;
using MyVocabularyCards.Services;

[assembly: Xamarin.Forms.Dependency(typeof(CloseApp))]

namespace MyVocabularyCards.Droid.Services
{
    public class CloseApp : IExitService
    {
        public void ExitApp()
        {
            Process.KillProcess(Android.OS.Process.MyPid());
        }
    }
}