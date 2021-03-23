using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyVocabularyCards.PersistenceDB
{
    public interface ISqliteConnection
    {
        SQLiteAsyncConnection GetConnection();
    }
}
