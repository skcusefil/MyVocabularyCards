using MyVocabularyCards.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyVocabularyCards.PersistenceDB
{
    public interface IVocabularyDB
    {
        Task<List<Vocabulary>> GetVocabularyAsync(string keyword);
        Task<Vocabulary> SearchVocabulary(int id);
        Task AddOrVocabulary(Vocabulary vocabulary);
        Task UpdateVocabulary(Vocabulary vocabulary);
        Task<int> DeleteVocabulary(Vocabulary vocabulary);
    }
}
