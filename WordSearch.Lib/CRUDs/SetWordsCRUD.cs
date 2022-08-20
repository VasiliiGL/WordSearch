using System;
using System.Collections.Generic;
using System.Text;

namespace WordSearch.Models.CRUDs
{
    public class SetWordsCRUD
    {
        public void AddFromString(DataViewModels data)
        {
            string words = data.SelectedWord.WordSearch;
            string[] newSetWords = words.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var word in newSetWords)
            {
                var newWord = new Word { WordSearch = word };
                data.SetWords.Add(newWord);
            }

        }
    }
}
