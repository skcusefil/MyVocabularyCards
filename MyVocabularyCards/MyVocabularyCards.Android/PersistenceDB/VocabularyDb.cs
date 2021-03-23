using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xamarin.Forms;
using SQLite;
using MyVocabularyCards.PersistenceDB;
using MyVocabularyCards.Droid.PersistenceDB;

[assembly: Dependency(typeof(VocabularyDb))]

namespace MyVocabularyCards.Droid.PersistenceDB
{
    public class VocabularyDb : ISqliteConnection
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentsPath, "Vocab.db3");

            return new SQLiteAsyncConnection(path);
        }
    }
}