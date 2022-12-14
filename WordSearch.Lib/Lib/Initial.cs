using System;
using System.Collections.Generic;
using System.Text;

namespace WordSearch.Models.Lib
{
    public class Initial
    {
        public string DirectoryForWords { get; set; }
        public string DirectoryForCopyFile { get; set; }
        public string DirectorySearchFile { get; set; }
        
        public Initial()
        {
            //this.DirectoryForWords = @"D:\VASILII\Запрещенные слова";
            //this.DirectoryForCopyFile = @"D:\VASILII\Контрольная работа WordSearch\NewDirectory";
            //this.DirectorySearchFile = @"D:\VASILII\Запрещенные слова Тектсы";
        }
        public Initial(string directoryForWords, string directoryForCopyFile, string directorySearchFile)
        {
            DirectoryForWords = directoryForWords;
            DirectoryForCopyFile = directoryForCopyFile;
            DirectorySearchFile = directorySearchFile;
        }
        public Initial(Initial initialData)
        {
            DirectoryForWords = initialData.DirectoryForWords;
            DirectoryForCopyFile = initialData.DirectoryForCopyFile;
            DirectorySearchFile = initialData.DirectorySearchFile;
        }
    }
}
