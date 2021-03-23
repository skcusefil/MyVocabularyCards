using MyVocabularyCards.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyVocabularyCards.PersistenceDB
{
    public class VocabularyDB : IVocabularyDB
    {
        //Create Connection
        private SQLiteAsyncConnection _connection;

        public VocabularyDB(ISqliteConnection database)
        {
            _connection = database.GetConnection();
            _connection.CreateTableAsync<Vocabulary>();
        }


        //Add or update
        public async Task AddOrVocabulary(Vocabulary vocabulary)
        {
            await _connection.InsertAsync(vocabulary);

        }

        //Delete 
        public async Task<int> DeleteVocabulary(Vocabulary vocabulary)
        {
            return await _connection.DeleteAsync(vocabulary);
        }

        //Search **Edit later
        public async Task<Vocabulary> SearchVocabulary(int id)
        {
            return await _connection.FindAsync<Vocabulary>(id);
        }

        //Update
        public async Task UpdateVocabulary(Vocabulary vocabulary)
        {
            await _connection.UpdateAsync(vocabulary);
        }

        //Get list from table in sqlite
        public async Task<List<Vocabulary>> GetVocabularyAsync(string keyword)
        {
            if (keyword == string.Empty)
            {
                return await _connection.Table<Vocabulary>().OrderBy(x=>x.Word).ToListAsync();

            }
            else
            {
                return await _connection.Table<Vocabulary>().Where(letter => letter.Word.Contains(keyword)).OrderBy(x=>x.Word).ToListAsync();
            }
        }
    }
}
