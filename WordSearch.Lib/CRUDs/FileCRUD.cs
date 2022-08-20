using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using Microsoft.Win32;

namespace WordSearch.Models.CRUDs
{
    public class FileCRUD
    {
        public TextCRUD TextCrud { get; set; }
        public FileCRUD()
        {
            TextCrud = new TextCRUD();
        }
        public void SearchDangerFiles(ObservableCollection<File> listFiles, ObservableCollection<Word> listWords, ObservableCollection<File> ListDanderFiles)
        {
            if (listFiles != null)
            {
                ListDanderFiles.Clear();
                string wordForSearch;
                string textForSearch;
                for (var f = 0; f < listFiles.Count; f++)
                {
                    for (var w = 0; w < listWords.Count; w++)
                    {
                        wordForSearch = listWords[w].WordSearch;
                        textForSearch = TextCrud.ReadTextOfFile(listFiles[f].PathFile);
                        // textForSearch = TextCrud.DeleteSigns(textForSearch);
                        if (TextCrud.IsSearchWords(textForSearch, listWords))
                        {
                            ListDanderFiles.Add(listFiles[f]);
                            break;
                        }
                    }
                }
            }
            
        }
        public void CopyFiles(ObservableCollection<File> listFiles, ObservableCollection<Word> setwords)
        {
            if (listFiles != null)
            {
                foreach (var file in listFiles)
                {
                    CopyFile(file.PathFile, file.NameFile);
                    CreatCopyFile(file, setwords);
                }
            }
        }
        public void CopyFile(string path, string name)
        {
            string pathDirectory = @"D:\VASILII\Контрольная работа WordSearch\NewDirectory";
            string newpathFile = pathDirectory + @"\" + name;
            FileInfo fileInfo = new FileInfo(path);
            if (fileInfo.Exists)
            {
                CreatDirectory();
                fileInfo.CopyTo(newpathFile, true);
            }
        }
        public void CreatDirectory()
        {
            string path = @"D:\VASILII\Контрольная работа WordSearch\NewDirectory";
            DirectoryInfo directory = new DirectoryInfo(path);
            if (!directory.Exists)
            {
                directory.Create();
            }
        }
        public void CreatCopyFile(File file, ObservableCollection<Word> setwords)
        {
            string newNameFile = "New" + "_" + file.NameFile;
            string pathDirectory = @"D:\VASILII\Контрольная работа WordSearch\NewDirectory";
            string newpathFileCopy = pathDirectory + @"\" + newNameFile;
            string newText;
            StreamWriter streamWriter;
            try
            {
                FileInfo fileInfo = new FileInfo(newpathFileCopy);
                streamWriter = fileInfo.CreateText();
                newText = ReplaceTextInFile(file, setwords);
                streamWriter.WriteLine(newText);
                streamWriter.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        public string ReplaceTextInFile(File file, ObservableCollection<Word> SetWords)
        {
            var pathFile = file.PathFile;
            string textFile = TextCrud.ReadTextOfFile(pathFile);
            string newTextFile = "";
            //for (var i = 0; i < SetWords.Count; i++)
            //{
            //    newTextFile= TextCrud.ReplaceText(textFile, SetWords[i].WordSearch);
            //}

            foreach (var word in SetWords)
            {
                newTextFile = TextCrud.ReplaceText(textFile, word.WordSearch);
            }
            return newTextFile;
        }

    }
}
