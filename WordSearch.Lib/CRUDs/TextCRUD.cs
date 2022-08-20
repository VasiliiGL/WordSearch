using System;
using System.Collections.Generic;
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
    }
}
