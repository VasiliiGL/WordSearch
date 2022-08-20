using System;
using System.Collections.Generic;
using System.Text;

namespace WordSearch.Models.CRUDs
{
    public class ListWordsCRUD
    {
        public void AddFromString(DataViewModels data)
        {
            data.ListWords.Clear();
            string words = data.SelectedWord.WordSearch;
            string[] newSetWords = words.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var word in newSetWords)
            {
                var newWord = new Word { WordSearch = word };
                data.ListWords.Add(newWord);
            }

        }
    }
}
