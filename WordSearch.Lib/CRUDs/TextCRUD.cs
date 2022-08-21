using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WordSearch.Models.CRUDs
{
    public class TextCRUD
    {
        public string DeleteSigns(string text)
        {
            Regex regex = new Regex(@"\W");
            string newText = regex.Replace(text, " ");
            return newText;
        }
        public string ReplaceText(string text, string word)
        {
            string newText;
            string newWord = new string('*', word.Length);
            newText = text.Replace(word, newWord);
            return newText;
        }
        public bool IsSearchWords(string text, ObservableCollection<Word> setWords)
        {
            bool isSearchWords = false;
            foreach (var word in setWords)
            {
                if (text.IndexOf(word.WordSearch) >= 0)
                {
                    isSearchWords = true;
                    break;
                }
            }
            return isSearchWords;
        }

        public string ReadTextOfFile(string _path)
        {
            string path = _path;
            string text;
            try
            {
                StreamReader reader = new StreamReader(path);
                text = reader.ReadToEnd();
                reader.Close();
                return text;
            }
            catch (Exception e)
            {
                return "Exception: " + e.Message;
            }
            finally
            {

            }
        }

        public void SearchDangerWord (FileSearch file, ObservableCollection<Word> ListWords)
        {
            var text = ReadTextOfFile(file.PathFile);
            List<string> listDangerWords = new List<string>();
            List<string> list = new List<string>();
            DeleteSigns(text);
            string[] ListWordsInText = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var word in ListWordsInText)
            {
                for (var i=0; i< ListWords.Count; i++)
                {
                    if (string.Compare(word,ListWords[i].WordSearch)==0)
                    {
                        listDangerWords.Add(word);
                    }
                }
            }
            var grouped = listDangerWords
                           .GroupBy(i => i)
                           .Select(i => new { Word = i.Key, Count = i.Count() }).OrderByDescending(i => i.Count);
            file.ListDangerWords.Clear();
            file.ListWordsReport = "";
            foreach (var i in grouped)
            {
                file.ListDangerWords.Add(i.Word, i.Count);
                list.Add($"{i.Word} / {i.Count}");
            }
            file.ListWordsReport = string.Join(" ", list); 
        }
    }
}
