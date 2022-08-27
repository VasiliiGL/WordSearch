using System;
using System.Collections.Generic;
using System.Text;

namespace WordSearch.Models.Lib
{
    public class Initial
    {
        public string DirectoryForWords { get; set; }
        public string DirectoryForCopyFile { get; set; }
        public Initial()
        {
            DirectoryForWords = @"D:\VASILII\Запрещенные слова";
            DirectoryForCopyFile = @"D:\VASILII\Контрольная работа WordSearch\NewDirectory";
        }
        public Initial(string directoryForWords, string directoryForCopyFile)
        {
            DirectoryForWords = directoryForWords;
            DirectoryForCopyFile = directoryForCopyFile;
        }
        public Initial(Initial initialData)
        {
            DirectoryForWords = initialData.DirectoryForWords;
            DirectoryForCopyFile = initialData.DirectoryForCopyFile;
        }
    }
}
