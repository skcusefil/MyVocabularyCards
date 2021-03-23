using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyVocabularyCards.Models
{
    public class Vocabulary
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Word { get; set; }
        public string Meaning { get; set; }
    }
}
